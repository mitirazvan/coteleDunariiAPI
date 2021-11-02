using CoteleDunarii.Data;
using CoteleDunarii.Helpers;
using CoteleDunarii.Repository;
using CoteleDunarii.Repository.Interfaces;
using CoteleDunarii.Services;
using CoteleDunarii.Services.Extensions;
using CoteleDunarii.Services.Interfaces;
using CoteleDunarii.WebServices.WebScrapper;
using Hangfire;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

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

            // Register Repositories
            services.AddTransient<ICityRepostirory, CityRepository>();
            services.AddHangfireService(Configuration);

            // Register Services
            services.AddTransient<ICityService, CityService>();
            services.AddScoped<IScrapper, AdfjScrapper>();

            services.AddCoteleDunariiMapping()
                .UseCoteleDunariiMappingProfile();

            services.AddSwaggerService();
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
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Cotele Dunarii Romania API V1");
            });

            app.ConfigureHangfire();
            StartupHelper.RegisterHangfireJobs();

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHangfireDashboard();
            });
        }
    }
}
