using System;
using System.Collections.Generic;
using System.Diagnostics.PerformanceData;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using LibraryManagement.Models;

namespace LibraryManagement.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class issueController : ApiController
    {
        ProjectEntities1 db = new ProjectEntities1();
        [HttpGet]
        public IEnumerable<CustomIssue> Get()
        {
            List<CustomIssue> custom = new List<CustomIssue>();
            var res = db.bookissues.ToList();
            foreach (var i in res)
            {
                CustomIssue c = new CustomIssue();
                c.iis = i.iis;
                c.bookname = i.book.bname;
                c.username = i.user.uname;
                c.issuedate = i.issuedate;
                c.noofdays = i.noofdays;
                c.expreturndate = i.expreturndate;
                c.actualreturndate = i.actualreturndate;
                c.fine = i.fine;
                custom.Add(c);
            }
            return custom;
        }
        [HttpGet]
        public CustomIssue Get(int id)
        {
            CustomIssue c = new CustomIssue();
            var res = (from i in db.bookissues
                       join b in db.books
                       on i.bid equals b.bid
                       join u in db.users
                       on i.uid equals u.uid
                       where i.iis == id
                       select new { IIS = i.iis,
                           BNAME = b.bname,
                           UNAME = u.uname,
                           ISSUEDATE = i.issuedate,
                           NOFDAYS = i.noofdays,
                           EXRETURNDATE = i.expreturndate,
                           ACTUALDATE = i.actualreturndate,
                           FINE = i.fine }).SingleOrDefault();

            c.iis = res.IIS;
            c.bookname = res.BNAME;
            c.username = res.UNAME;
            c.issuedate = res.ISSUEDATE;
            c.noofdays = res.NOFDAYS;
            c.expreturndate = res.EXRETURNDATE;
            c.actualreturndate = res.ACTUALDATE;
            c.fine = res.FINE;

            return c;


        }

        [HttpPost]
        public string Post([FromBody] bookissue p)
        {
            p.expreturndate = (Convert.ToDateTime(p.issuedate).AddDays(p.noofdays)).ToString();
            db.bookissues.Add(p);
            var res = db.SaveChanges();
            if (res > 0)
                return "New Book issue";
            else
                return "Error In Issue";

        }

        [HttpPut]
        public string Update(int id, [FromBody] bookissue i)
        {
            var c = (from t in db.bookissues
                           where t.iis == id
                           select t).SingleOrDefault();
            if (c == null)
                throw new Exception("Issued Id Invalid");
            else
            {
                string actual = i.actualreturndate.ToString();
                string expected = c.expreturndate.ToString();
                c.actualreturndate = i.actualreturndate;
                TimeSpan ts = Convert.ToDateTime(actual).Subtract(Convert.ToDateTime(expected));
                int d = ts.Days;

                if (d > 0)
                 {
                     c.fine =(10*d);
                 }
                else
                {
                    c.fine = 0;
                }
                  var result = db.SaveChanges();
                if (result > 0)
                    return "Data Uploaded";
            }
            return "Error Updating Data";
        }
    }
}
