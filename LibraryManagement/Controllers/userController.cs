using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using LibraryManagement.Models;

namespace LibraryManagement.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class userController : ApiController
    {
        ProjectEntities1 db = new ProjectEntities1();
        [HttpGet]
        public IEnumerable<CustomUser> Get()
        {
            List<CustomUser> custom = new List<CustomUser>();
            var res = db.users.ToList();
            foreach(var i in res)
            {
                CustomUser u = new CustomUser();
                u.uid = i.uid;
                u.uname = i.uname;
                u.ucontact = i.ucontact;
                u.uaddress = i.uaddress;
                custom.Add(u);
            }
            return custom;
        }

        [HttpGet]
        public CustomUser Get(int id)
        {
            CustomUser u = new CustomUser();
            var i = (from c in db.users
                     where c.uid == id
                     select new { 
                     UID = c.uid,
                     UNAME = c.uname,
                     UCONTACT = c.ucontact,
                     UADDRESS = c.uaddress,
                     }).SingleOrDefault();
            u.uid = i.UID;
            u.uname = i.UNAME;
            u.ucontact = i.UCONTACT;
            u.uaddress = i.UADDRESS;

            return u;
        }

        [HttpPost]
        public string Post([FromBody] user p)
        {
            db.users.Add(p);
            var res = db.SaveChanges();
            if (res > 0)
                return "New User Inserted";
            else
                return "Error Inserting User";

        }

        [HttpPut]
        public string Update(int id, [FromBody] user i)
        {
            var u = (from t in db.users
                     where t.uid == id
                     select t).SingleOrDefault();
            if (u == null)
                return "Book Id Invalid";
            else
            {
                u.uname = i.uname;
                u.ucontact = i.ucontact;
                u.uaddress = i.uaddress;

                var res = db.SaveChanges();
                if (res > 0)
                    return "Data Uploaded";
            }
            return "Error Updating Data";
        }

        [HttpDelete]
        public int Delete(int id)
        {

            var data = db.users.Where(x => x.uid == id).SingleOrDefault();
            try
            {
                if (data == null)
                    return 400;
                else
                {
                    db.users.Remove(data);
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
