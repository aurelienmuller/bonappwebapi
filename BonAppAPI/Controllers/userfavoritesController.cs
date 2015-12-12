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
using BonAppAPI.Models;

namespace BonAppAPI.Controllers
{
    public class userfavoritesController : ApiController
    {
        private bonappdbEntities db = new bonappdbEntities();

        // GET: api/userfavorites
        public IEnumerable<UserfavoriteDTO> Getuserfavorites()
        {
            var userfavorites = db.userfavorites.Select(userfav =>
                                new UserfavoriteDTO
                                {
                                    recipeid_fav = userfav.recipeid_fav,
                                    title = userfav.recipe.title,
                                    source_url = userfav.recipe.source_url
                                });
            return userfavorites.ToList();
            //return db.userfavorites.ToList();

        }
        /*public IEnumerable<UserfavoriteDTO> Getuserfavorites(int userid = 2)
        {
            var userfavorites = from u in db.userfavorites
                                select new UserfavoriteDTO()
                                {
                                    recipeid_fav = u.recipe.recipe_id,
                                    title = u.recipe.title,
                                    source_url = u.recipe.source_url
                                };
            /*var userfavorites = db.userfavorites.Where(s => s.user.userid.Equals(userid))
                .Select(userfavorite => new UserfavoriteDTO
                {
                    recipeid_fav = userfavorite.recipeid_fav,
                    source_url = userfavorite.recipe.source_url,
                    title = userfavorite.recipe.title
                });*/
        // return userfavorites.ToList();
        //}

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

            if (id != userfavorite.userfavorite_id)
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
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = userfavorite.userfavorite_id }, userfavorite);
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
            return db.userfavorites.Count(e => e.userfavorite_id == id) > 0;
        }
    }
}