using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApplication4.Models;
namespace WebApplication4.Controllers
{
    public class ValuesController : ApiController
    {
        LeavesDBEntities lm = new LeavesDBEntities();
        // GET api/values
        public IHttpActionResult getemp()
        {

            var results = lm.Employees.ToList();
            return Ok(results);
        }
        // GET api/values/5
        public IHttpActionResult Getempid(int id)
        {
            Employee empdetails = null;
            empdetails = lm.Employees.Find(id);

            if (empdetails == null)
            {
                return NotFound();

            }
            return Ok(empdetails);
        }
  

        // POST api/values
        public IHttpActionResult empinsert(Employee empinsert)
        {
            lm.Employees.Add(empinsert);
            lm.SaveChanges();
            return Ok(lm);

        }

        // PUT api/values/5
        public IHttpActionResult Put(int id, Employee emp)
        {
            if (id == emp.EmployeeId)
            {
                lm.Entry(emp).State = System.Data.Entity.EntityState.Modified;
                lm.SaveChanges();
            }
            return Ok(lm);
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
