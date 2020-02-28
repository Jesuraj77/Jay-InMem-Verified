using EMartV2.BuisnessLayer.Interfaces;
using EMartV2.BuisnessLayer.Services;
using EMartV2.DataLayer;
using EMartV2.DataLayer.Database;
using EMartV2.DataLayer.Interfaces;
using EMartV2.DataLayer.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EMartV2.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        { 

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            //services.AddDbContext<EMartContext>(options => options.UseMySQL(Configuration.GetConnectionString("DefaultConnection")), ServiceLifetime.Transient);

            services.AddSingleton<IDbConnectionFactory>(x =>
                new SqLiteConnectionFactory(Configuration.GetValue<string>("Database:DbLocation")));

            services.AddSingleton<IProductRepository, ProductRepository>();
            services.AddSingleton<IProductService, ProductService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();
            app.UseCookiePolicy();

            //UpdateDatabase(app);

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }

        //private static void UpdateDatabase(IApplicationBuilder app)
        //{
        //    using (var serviceScope = app.ApplicationServices
        //        .GetRequiredService<IServiceScopeFactory>()
        //        .CreateScope())
        //    {
        //        using (var context = serviceScope.ServiceProvider.GetService<EMartContext>())
        //        {
        //            context.Database.Migrate();
        //        }
        //    }
        //}
    }
}
