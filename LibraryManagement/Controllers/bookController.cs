using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Web.Http;
using System.Web.Http.Cors;
using LibraryManagement.Models;

namespace LibraryManagement.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class bookController : ApiController
    {
        ProjectEntities1 db = new ProjectEntities1();
        [HttpGet]
        public IEnumerable<CustomBook> Get()
        {
            List<CustomBook> custom = new List<CustomBook>();
            var res = db.books.ToList();
            foreach(var i in res)
            {
                CustomBook b = new CustomBook();
                b.bid = i.bid;
                b.bname = i.bname;
                b.bgener = i.bgener;
                b.bprice = i.bprice;
                custom.Add(b);
            }
            return custom;
        }
        [HttpGet]
        public CustomBook Get(int id)
        {
            CustomBook b = new CustomBook();
            var i = (from c in db.books
                     where c.bid == id
                     select new { 
                     BID = c.bid,
                     BNAME = c.bname,
                     BGENRE = c.bgener,
                     BPRICE = c.bprice,
                     }).SingleOrDefault();
            b.bid = i.BID;
            b.bname = i.BNAME;
            b.bgener = i.BGENRE;
            b.bprice = i.BPRICE;
            return b;
        }

        [HttpPost]
        public string Post([FromBody] book p)
        {
            db.books.Add(p);
            var res = db.SaveChanges();
            if (res > 0)
                return "New Book Inserted";
            else
                return "Error Inserting Book";

        }

        [HttpPut]
        public string Update(int id, [FromBody] book i)
        {
            var b = (from t in db.books
                           where t.bid == id
                           select t).SingleOrDefault();
            if (b == null)
                throw new Exception("Book Id Invalid");
            else
            {
                b.bname = i.bname;
                b.bgener = i.bgener;
                b.bprice = i.bprice;

                var res = db.SaveChanges();
                if (res > 0)
                    return "Data Uploaded";
            }
            return "Error Updating Data";
        }

        [HttpDelete]
        public int Delete(int id)
        {

            var data = db.books.Where(x => x.bid == id).SingleOrDefault();
            try
            {
                if (data == null)
                    return 400;
                else
                {
                    db.books.Remove(data);
                    var res = db.SaveChanges();
                    if (res > 0)
                        return 200;
                }
            }
            catch
            {
                return 401;
            }



            return 300;

        }
    }
}
