using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebCrawlerAPI.Controllers;

namespace WebCrawlerAPI
{
    public static class CrawlerJobs
    {
        public static void RemoveAll()
        {
            GameModelsController controller = new GameModelsController();
            controller.DeleteAllGameModels();
        }
        public static void GetDiscounts()
        {
            WebsiteCrawler.GetAllDiscounts();
        }
    }
}