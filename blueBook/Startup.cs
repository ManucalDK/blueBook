using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using blueBook.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace blueBook
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
            //methods from extensions package
            services.ConfigureCors();
            services.ConfigureIISIntegration();

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //this options are middlewares, so its really important the correct order in the call of the methods
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }else{
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            
            app.UseStaticFiles();//this enable the static files support, the default path its the wwwroot folder

            app.UseCors("CorsPolicy"); //name of the cors configuration on the extensions package

            //this option enable the forward proxy headers, this can help during application deployment
            app.UseForwardedHeaders(new ForwardedHeadersOptions{
                ForwardedHeaders = Microsoft.AspNetCore.HttpOverrides.ForwardedHeaders.All
            });

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
