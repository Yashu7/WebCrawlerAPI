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
using WebCrawlerAPI.Models;

namespace WebCrawlerAPI.Controllers
{
    public class GameModelsController : ApiController
    {
        private GameDBContext db = new GameDBContext();

        // GET: api/GameModels
        public IQueryable<GameModel> GetGames()
        {
            
                return db.Games;
            
            
        }

        // GET: api/GameModels/5
        [ResponseType(typeof(GameModel))]
        public IHttpActionResult GetGameModel(int id)
        {
            try
            {
                GameModel gameModel = db.Games.Find(id);
                if (gameModel == null)
                {
                    return NotFound();
                }

                return Ok(gameModel);
            }
            catch (Exception ex)
            {
                return Ok(ex);
            }
           
           
           
        }

        // PUT: api/GameModels/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutGameModel(int id, GameModel gameModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != gameModel.Id)
            {
                return BadRequest();
            }

            db.Entry(gameModel).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GameModelExists(id))
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

        // POST: api/GameModels
        [ResponseType(typeof(GameModel))]
        public IHttpActionResult PostGameModel(GameModel gameModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Games.Add(gameModel);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = gameModel.Id }, gameModel);
        }

        // DELETE: api/GameModels/5
        [ResponseType(typeof(GameModel))]
        public IHttpActionResult DeleteGameModel(int id)
        {
            GameModel gameModel = db.Games.Find(id);
            if (gameModel == null)
            {
                return NotFound();
            }

            db.Games.Remove(gameModel);
            db.SaveChanges();

            return Ok(gameModel);
        }
        // DELETE: api/DeleteAll
        public void DeleteAllGameModels()
        {
          
            
            db.Database.ExecuteSqlCommand("DELETE FROM GameModels");
            db.SaveChanges();
            

        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool GameModelExists(int id)
        {
            return db.Games.Count(e => e.Id == id) > 0;
        }
    }
}