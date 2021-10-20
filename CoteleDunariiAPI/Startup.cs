using CoteleDunarii.Data;
using CoteleDunarii.Repository;
using CoteleDunarii.Repository.Interfaces;
using CoteleDunarii.Services;
using CoteleDunarii.Services.Extensions;
using CoteleDunarii.Services.Interfaces;
using CoteleDunarii.WebServices.WebScrapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace CoteleDunarii
{
    public class Startup
    {
        private IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddDbContext<CoteleDunariiContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("DbConnection")));

            services.AddAutoMapper(typeof(Startup));

            services.AddScoped<IScrapper, AdfjScrapper>();

            // Register Repositories
            services.AddTransient<ICityRepostirory, CityRepository>();

            // Register Services
            services.AddTransient<ICityService, CityService>();

            services.AddCoteleDunariiMapping()
                .UseCoteleDunariiMappingProfile();

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

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Cotele Dunarii Romania API V1");
                //c.RoutePrefix = string.Empty;
            });

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
