using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using projekt1.net.Models;
using projekt1.net.EntityFramework;
using Swashbuckle.AspNetCore.Swagger;
using System.IO;

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.AzureADB2C.UI;

using System.Reflection;

using Microsoft.OpenApi.Models;


namespace projekt1.net
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
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
            });

            services.AddAuthentication(AzureADB2CDefaults.AuthenticationScheme)
                .AddAzureADB2C(options => Configuration.Bind("AzureAdB2C", options));
            //services.AddSwaggerGen(c =>
            //{
            //    c.SwaggerDoc("v1", new OpenApiInfo()
            //    {
            //        Title = "projekt.net",
            //        Version = "v1",
            //        Description = "Swagger example",
            //        //Contact = new Contact
            //        //{
            //        //    Name = "Anna Buchman",
            //        //    Email = "anna.m.buchman@gmail.com"
            //        //}
            //    });
            //    //  //  Set the comments path for the Swagger JSON and UI.

            //    //   var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            //    //    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            //    //    c.IncludeXmlComments(xmlPath);
            //});

            var connection = Configuration["DatabaseConnectionString"];
			services.AddDbContext<DataContext>(options => options.UseSqlServer(connection));

            services.AddSingleton<BlobStorageService>();

            services.Configure<MyConfig>(Configuration.GetSection("ConnectionStrings"));
            services.AddApplicationInsightsTelemetry();
            //services.AddDbContext<projekt1netContext>(options =>
            //        options.UseSqlServer(Configuration.GetConnectionString("projekt1netContext")));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseSwagger();
            //// Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), 
            //// specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("../swagger/v1/swagger.json",  "projekt.net API v1");
            });
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }
            app.UseAuthentication();
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
