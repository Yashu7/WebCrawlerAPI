using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebCrawlerAPI.Models;

namespace WebCrawlerAPI.Controllers
{
    public class FireFunctionController : ApiController
    {
        GameModelsController controller = new GameModelsController();


        [HttpGet]
        public void RemoveDiscounts()
        {

            controller.DeleteAllGameModels();


        }
    }
}
