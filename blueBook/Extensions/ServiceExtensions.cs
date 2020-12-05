using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace blueBook.Extensions
{
    public static class ServiceExtensions
    {
        /// <summary>
        /// method to configure cors permissions
        /// </summary>
        /// <param name="services">param associate to dependency injection</param>
        public static void ConfigureCors(this IServiceCollection services ) =>
            services.AddCors(options => 
            {
                options.AddPolicy("CorsPolicy", builder => 
                builder.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader()
                );
            });

        public static void ConfigureIISIntegration(this IServiceCollection services) =>
            services.Configure<IISOptions>(options =>
                {

                });
    }
}