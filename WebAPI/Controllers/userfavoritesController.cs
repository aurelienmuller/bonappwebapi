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
using WebAPI.Models;

namespace WebAPI.Controllers
{
    public class userfavoritesController : ApiController
    {
        private BonAppDBEntities db = new BonAppDBEntities();

        // GET: api/userfavorites
        public IEnumerable<userfavorite> Getuserfavorites()
        {
            return db.userfavorites.ToList();
        }

        // GET: api/userfavorites/5
        [ResponseType(typeof(userfavorite))]
        public IHttpActionResult Getuserfavorite(int id)
        {
            userfavorite userfavorite = db.userfavorites.Find(id);
            if (userfavorite == null)
            {
                return NotFound();
            }

            return Ok(userfavorite);
        }

        // PUT: api/userfavorites/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putuserfavorite(int id, userfavorite userfavorite)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != userfavorite.userid_fav)
            {
                return BadRequest();
            }

            db.Entry(userfavorite).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!userfavoriteExists(id))
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

        // POST: api/userfavorites
        [ResponseType(typeof(userfavorite))]
        public IHttpActionResult Postuserfavorite(userfavorite userfavorite)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.userfavorites.Add(userfavorite);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (userfavoriteExists(userfavorite.userid_fav))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = userfavorite.userid_fav }, userfavorite);
        }

        // DELETE: api/userfavorites/5
        [ResponseType(typeof(userfavorite))]
        public IHttpActionResult Deleteuserfavorite(int id)
        {
            userfavorite userfavorite = db.userfavorites.Find(id);
            if (userfavorite == null)
            {
                return NotFound();
            }

            db.userfavorites.Remove(userfavorite);
            db.SaveChanges();

            return Ok(userfavorite);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool userfavoriteExists(int id)
        {
            return db.userfavorites.Count(e => e.userid_fav == id) > 0;
        }
    }
}