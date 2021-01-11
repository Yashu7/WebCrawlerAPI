
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Hangfire;
using Microsoft.Owin;
using Owin;
using Hangfire.Dashboard;
using Microsoft.Owin.Host.SystemWeb;
using WebCrawlerAPI.HangFire;

[assembly: OwinStartup(typeof(WebCrawlerAPI.Startup))]
namespace WebCrawlerAPI
{
   
    public partial class Startup
    {
        public void  Configuration(IAppBuilder app)
        {
            app.UseHangfireDashboard("/fireboard", new DashboardOptions
            {
                Authorization = new [] { new AuthFilter() },
               // IsReadOnlyFunc = (DashboardContext context) => true
            });
        }
    }
}