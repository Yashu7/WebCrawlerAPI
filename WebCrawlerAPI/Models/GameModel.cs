using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebCrawlerAPI.Interfaces;
using System.Data.Entity;


namespace WebCrawlerAPI.Models
{
    public class GameModel : IGameModel
    {
        /// <summary>
        /// Game's id in the database
        /// </summary>
        
        public int Id { get; set; }
        /// <summary>
        /// Game's Title
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// Price before sale
        /// </summary>
        public string OriginalPrice { get; set; }
        /// <summary>
        /// Price during sale
        /// </summary>
        public string DiscountPrice { get; set; }
    }
    public class GameDBContext : DbContext
    {
        public DbSet<GameModel> Games { get; set; }
    }
}