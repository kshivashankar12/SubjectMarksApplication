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
using SubjectMarksApplication;

namespace SubjectMarksApplication.Controllers
{
    public class SubjectMarksController : ApiController
    {
        private StudentDataEntities db = new StudentDataEntities();

        // GET api/SubjectMarks
        public IHttpActionResult GetSubjectMarks()
        {
            var subjectMarks = db.SubjectMarks.ToList();
            return Ok(subjectMarks);
        }

        // GET api/SubjectMarks/5
        public IHttpActionResult GetSubjectMark(int id)
        {
            var subjectMark = db.SubjectMarks.Find(id);
            if (subjectMark == null)
            {
                return NotFound();
            }

            return Ok(subjectMark);
        }

        // POST api/SubjectMarks
        public IHttpActionResult PostSubjectMark(SubjectMark subjectMark)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.SubjectMarks.Add(subjectMark);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = subjectMark.Id }, subjectMark);
        }

        // PUT api/SubjectMarks/5
        public IHttpActionResult PutSubjectMark(int id, SubjectMark subjectMark)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != subjectMark.Id)
            {
                return BadRequest();
            }

            db.Entry(subjectMark).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SubjectMarkExists(id))
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

        // DELETE api/SubjectMarks/5
        public IHttpActionResult DeleteSubjectMark(int id)
        {
            var subjectMark = db.SubjectMarks.Find(id);
            if (subjectMark == null)
            {
                return NotFound();
            }

            db.SubjectMarks.Remove(subjectMark);
            db.SaveChanges();

            return Ok(subjectMark);
        }

        private bool SubjectMarkExists(int id)
        {
            return db.SubjectMarks.Any(e => e.Id == id);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}