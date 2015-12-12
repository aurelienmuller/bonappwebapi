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
    public class recipesController : ApiController
    {
        private bonappdbEntities db = new bonappdbEntities();

        // GET: api/recipes
        public IEnumerable<recipe> Getrecipes()
        {
            return db.recipes.ToList();
        }

        // GET: api/recipes/5
        [ResponseType(typeof(recipe))]
        public IHttpActionResult Getrecipe(string id)
        {
            recipe recipe = db.recipes.Find(id);
            if (recipe == null)
            {
                return NotFound();
            }

            return Ok(recipe);
        }

        // PUT: api/recipes/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putrecipe(string id, recipe recipe)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != recipe.recipe_id)
            {
                return BadRequest();
            }

            db.Entry(recipe).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!recipeExists(id))
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

        // POST: api/recipes
        [ResponseType(typeof(recipe))]
        public IHttpActionResult Postrecipe(recipe recipe)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.recipes.Add(recipe);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (recipeExists(recipe.recipe_id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = recipe.recipe_id }, recipe);
        }

        // DELETE: api/recipes/5
        [ResponseType(typeof(recipe))]
        public IHttpActionResult Deleterecipe(string id)
        {
            recipe recipe = db.recipes.Find(id);
            if (recipe == null)
            {
                return NotFound();
            }

            db.recipes.Remove(recipe);
            db.SaveChanges();

            return Ok(recipe);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool recipeExists(string id)
        {
            return db.recipes.Count(e => e.recipe_id == id) > 0;
        }
    }
}