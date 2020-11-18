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
        public int Id { get; set; }
        public string Title { get; set; }
        public string OriginalPrice { get; set; }
        public string DiscountPrice { get; set; }
    }
    public class GameDBContext : DbContext
    {
        public DbSet<GameModel> Games { get; set; }
    }
}