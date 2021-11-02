using CoteleDunarii.Filters;
using CoteleDunarii.WebServices.WebScrapper;
using Hangfire;
using Hangfire.SqlServer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;

namespace CoteleDunarii.Helpers
{
    public static class StartupHelper
    {


        /// <summary>
        /// Register services for Swagger documentation
        /// </summary>
        /// <param name="services"></param>
        public static void AddSwaggerService(this IServiceCollection services)
        {
            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Cotele Dunarii Romania API",
                    Description = "This API will scrape the ADFJ website to retrieve Danube water level information and present them via the API enpoints.",
                    //TermsOfService = new Uri("https://example.com/terms"),
                    Contact = new OpenApiContact
                    {
                        Name = "Razvan Mititelu",
                        Email = "miti_razvan@yahoo.com",
                        Url = new System.Uri("https://me.valeafetii.ro/")
                    }
                });
            });
        }


        /// <summary>
        /// Register services for Swagger documentation
        /// </summary>
        /// <param name="services"></param>
        public static void AddHangfireService(this IServiceCollection services, IConfiguration Configuration)
        {
            string hangfireConnectionString = Configuration.GetConnectionString("DbConnection");

            // Add Hangfire services.
            services.AddHangfire(configuration => configuration
                .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                .UseSimpleAssemblyNameTypeSerializer()
                .UseRecommendedSerializerSettings()
                .UseSqlServerStorage(hangfireConnectionString, new SqlServerStorageOptions
                {
                    CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
                    SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
                    QueuePollInterval = TimeSpan.Zero,
                    UseRecommendedIsolationLevel = true,
                    DisableGlobalLocks = true
                }));

            // Add the processing server as IHostedService
            services.AddHangfireServer();
        }

        public static void ConfigureHangfire(this IApplicationBuilder app)
        {
            var options = new DashboardOptions
            {
                Authorization = new[] {
                    new HangfireDashboardAuthorizationFilter(new[]
                    {
                        new HangfireUserCredentials
                        {
                            Username = "coteleDunarii",
                            Password = "KENYHFfyV4Cdzj#Zy7N$"
                        }
                    })
                }
            };

            app.UseHangfireDashboard("/hangfire", options);
        }

        /// <summary>
        /// Register Hangfire jobs. 
        /// </summary>
        public static void RegisterHangfireJobs()
        {
            // Sync job -> Occurs every day at 14:00 UTC.
            RecurringJob.AddOrUpdate<IScrapper>(x => x.RetrieveData(), $"0 14 * * *");
        }

    }
}
