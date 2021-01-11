using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebCrawlerAPI.Controllers;
using WebCrawlerAPI.Models;

namespace WebCrawlerAPI
{
    public static class CrawlerJobs
    {
      
        public static void GetDiscounts()
        {
            GameModelsController controller = new GameModelsController();
            controller.DeleteAllGameModels();
            WebsiteCrawler.GetAllDiscounts();
        }
    }
}