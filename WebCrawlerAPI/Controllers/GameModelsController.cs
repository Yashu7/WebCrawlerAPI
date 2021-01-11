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
        /// <summary>
        /// Get all games on sale from the API
        /// </summary>
        /// <returns></returns>
        [Route("api/allgames/")]
        public IQueryable<GameModel> GetGames()
        {
                return db.Games;
        }
        [ApiExplorerSettings(IgnoreApi = true)]
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
        [ApiExplorerSettings(IgnoreApi = true)]
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