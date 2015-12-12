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
using System.Threading.Tasks;

namespace BonAppAPI.Controllers
{
    public class usersController : ApiController
    {
        private bonappdbEntities db = new bonappdbEntities();

        // GET: api/users
        public IEnumerable<user> Getusers()
        {
            return db.users.ToList();
        }

        // GET: api/users/5
        [ResponseType(typeof(user))]
        public IHttpActionResult Getuser(int id)
        {
            user user = db.users.Find(id);
            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }
        /*[ResponseType(typeof(UserDTO))]
        public IHttpActionResult Getuser(int id)
        {
            var userfavorite = db.userfavorites.Where(uf => uf.userid_fav.Equals(id)).Select(userf =>
                               new UserfavoriteDTO
                               {
                                   recipeid_fav = userf.recipeid_fav,
                                   title = userf.recipe.title,
                                   source_url = userf.recipe.source_url
                               }).ToList();
            var users = db.users.Include(u => u.userfavorites).Select(u =>
                        new UserDTO()
                        {
                            userfav = userfavorite
                            

                        });

            if(users == null)
            {
                return NotFound();
            }
            return Ok(users);
        }*/


        // PUT: api/users/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putuser(int id, user user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != user.userid)
            {
                return BadRequest();
            }

            db.Entry(user).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!userExists(id))
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

        // POST: api/users
        [ResponseType(typeof(user))]
        public IHttpActionResult Postuser(user user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.users.Add(user);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = user.userid }, user);
        }

        // DELETE: api/users/5
        [ResponseType(typeof(user))]
        public IHttpActionResult Deleteuser(int id)
        {
            user user = db.users.Find(id);
            if (user == null)
            {
                return NotFound();
            }

            db.users.Remove(user);
            db.SaveChanges();

            return Ok(user);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool userExists(int id)
        {
            return db.users.Count(e => e.userid == id) > 0;
        }
    }
}