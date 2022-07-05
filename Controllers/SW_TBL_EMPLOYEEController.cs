using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using WebAppEmployeOperation.Models;

namespace WebAppEmployeOperation.Controllers
{
    public class SW_TBL_EMPLOYEEController : ApiController
    {
        private EmployeeDBEntities db = new EmployeeDBEntities();

        // GET: api/SW_TBL_EMPLOYEE
        
        public IQueryable<SW_TBL_EMPLOYEE> GetSW_TBL_EMPLOYEE()
        {
            return db.SW_TBL_EMPLOYEE;
        }

        // GET: api/SW_TBL_EMPLOYEE/5
        
        [ResponseType(typeof(SW_TBL_EMPLOYEE))]
        public IHttpActionResult GetSW_TBL_EMPLOYEE(int id)
        {
            SW_TBL_EMPLOYEE sW_TBL_EMPLOYEE = db.SW_TBL_EMPLOYEE.Find(id);
            if (sW_TBL_EMPLOYEE == null)
            {
                return NotFound();
            }

            return Ok(sW_TBL_EMPLOYEE);
        }

        // PUT: api/SW_TBL_EMPLOYEE/5
        [ResponseType(typeof(void))]
        [HttpPut]

        public IHttpActionResult PutSW_TBL_EMPLOYEE(int id, SW_TBL_EMPLOYEE sW_TBL_EMPLOYEE)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != sW_TBL_EMPLOYEE.Employee_ID)
            {
                return BadRequest();
            }


            var emp = db.sp_UpdateEmployee(sW_TBL_EMPLOYEE.Employee_Name, sW_TBL_EMPLOYEE.Salary, id);
            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SW_TBL_EMPLOYEEExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/SW_TBL_EMPLOYEE
        
        [ResponseType(typeof(SW_TBL_EMPLOYEE))]
        [HttpPost]
        public IHttpActionResult PostSW_TBL_EMPLOYEE(SW_TBL_EMPLOYEE sW_TBL_EMPLOYEE)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //db.SW_TBL_EMPLOYEE.Add(sW_TBL_EMPLOYEE);
            //db.SaveChanges();

            var empid = db.sp_AddEmployee(sW_TBL_EMPLOYEE.Employee_Name, sW_TBL_EMPLOYEE.Salary);
            return CreatedAtRoute("DefaultApi", new { id = sW_TBL_EMPLOYEE.Employee_ID }, sW_TBL_EMPLOYEE);
        }

        // DELETE: api/SW_TBL_EMPLOYEE/5
       
        [ResponseType(typeof(SW_TBL_EMPLOYEE))]
        [HttpDelete]
        public IHttpActionResult DeleteSW_TBL_EMPLOYEE(int id)
        {
            SW_TBL_EMPLOYEE sW_TBL_EMPLOYEE = db.SW_TBL_EMPLOYEE.Find(id);
            if (sW_TBL_EMPLOYEE == null)
            {
                return NotFound();
            }

            var emp = db.sp_DeleteEmployee(id);

            //db.SW_TBL_EMPLOYEE.Remove(sW_TBL_EMPLOYEE);
            //db.SaveChanges();

            return Ok(sW_TBL_EMPLOYEE);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool SW_TBL_EMPLOYEEExists(int id)
        {
            return db.SW_TBL_EMPLOYEE.Count(e => e.Employee_ID == id) > 0;
        }
    }
}