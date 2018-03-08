using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.Collections.Generic;
using System.Text;

namespace Writeless.Extensions.Swagger
{
    public static class SwaggerExtensions
    {
        public static void AddSwaggerGen(this IServiceCollection services, string apiTitle, params string[] apiVersions)
        {
            services.AddSwaggerGen(e =>
            {
                foreach (var version in apiVersions)
                {
                    e.SwaggerDoc(version, new Info { Title = apiTitle, Version = version });
                }
            });
            services.ConfigureSwaggerGen(options => options.CustomSchemaIds(e => e.FullName));
        }

        public static void UseSwaggerUI(this IApplicationBuilder app, string apiTitle, params string[] apiVersions)
        {
            app.UseSwagger();
            app.UseSwaggerUI(e =>
            {
                foreach (var version in apiVersions)
                {
                    e.SwaggerEndpoint($"/swagger/{version}/swagger.json", $"{apiTitle} {version}");
                }
            });
        }
    }
}
