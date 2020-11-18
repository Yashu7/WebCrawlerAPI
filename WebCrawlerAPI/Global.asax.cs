﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Hangfire;
using Hangfire.SqlServer;
using Hangfire.Storage;
using Microsoft.Owin.Host.SystemWeb;
using WebCrawlerAPI.Controllers;

namespace WebCrawlerAPI
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        private IEnumerable<IDisposable> GetHangfireServers()
        {
            Hangfire.GlobalConfiguration.Configuration
                .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                .UseSimpleAssemblyNameTypeSerializer()
                .UseRecommendedSerializerSettings()
                .UseSqlServerStorage("Data Source=MYSERVER;Initial Catalog=2094_aspnet-WebCrawlerAPI;User Id=MYID; Password=MYPASSWORD;", new SqlServerStorageOptions
                {
                    CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
                    SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
                    QueuePollInterval = TimeSpan.Zero,
                    UseRecommendedIsolationLevel = true,
                    DisableGlobalLocks = true
                });

            yield return new BackgroundJobServer();
        }
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            System.Web.Http.GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            HangfireAspNet.Use(GetHangfireServers);
            BackgroundJob.Enqueue(() => CrawlerJobs.RemoveAll());
            BackgroundJob.Enqueue(() => CrawlerJobs.GetDiscounts());
            
            using (var connection = JobStorage.Current.GetConnection())
            {
                foreach (var recurringJob in StorageConnectionExtensions.GetRecurringJobs(connection))
                {
                    RecurringJob.RemoveIfExists(recurringJob.Id);
                }
            }
            RecurringJob.AddOrUpdate(() => CrawlerJobs.RemoveAll(), Cron.Daily);
            RecurringJob.AddOrUpdate(() => CrawlerJobs.GetDiscounts(), Cron.Daily);

          
        }
        
    }
}
