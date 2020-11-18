using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace WebCrawlerAPI.Interfaces
{
    public interface IGameModel
    {
        int Id { get; set; }
        string Title { get; set; }
        string OriginalPrice { get; set; }
        string DiscountPrice { get; set; }
    }
}