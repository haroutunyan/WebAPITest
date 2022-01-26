using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DIConfigure
{
    public static class SwaggerConfiguration
    {
        public static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Monada API",
                    Description = "ASP.NET Core Web API",
                    TermsOfService = new Uri("https://example.com/terms"),
                    Contact = new OpenApiContact
                    {
                        Name = "Shayne Boyer",
                        Email = string.Empty,
                        Url = new Uri("https://twitter.com/spboyer")
                    },
                    License = new OpenApiLicense
                    {
                        Url = new Uri("https://example.com/license")
                    }
                });


                var scheme = new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Scheme = "Bearer",
                    BearerFormat = "Bearer",
                    Type = SecuritySchemeType.ApiKey,
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization header using the Bearer scheme."
                };
                c.MapType<DateOnly>(() => new OpenApiSchema { Type = "string", Format = "date" });
                c.SchemaFilter<SwaggerFilter>();
                c.DocumentFilter<SwaggerFilter>();
                c.OperationFilter<SwaggerFilter>();
                c.AddSecurityDefinition("Bearer", scheme);
                scheme.Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" };
                c.AddSecurityRequirement(new OpenApiSecurityRequirement { { scheme, new List<string>() } });
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
            services.AddSwaggerGenNewtonsoftSupport();

            return services;
        }


        public static IApplicationBuilder UseOpenApi(this IApplicationBuilder app)
        {
            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.EnableTryItOutByDefault();
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "API V1");
                c.InjectStylesheet("/swagger-ui/SwaggerDark.css");
            });
            return app;
        }
    }

    public class SwaggerFilter : ISchemaFilter, IDocumentFilter, IOperationFilter
    {
        static readonly string deviceId = Guid.NewGuid().ToString();
        public void Apply(OpenApiSchema schema, SchemaFilterContext context)
        {
            if (context.Type.FullName.StartsWith("NetTopologySuite.Geometries"))
                schema.Deprecated = true;
        }

        public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
        {
            swaggerDoc.Servers = swaggerDoc.Servers.Select(s =>
            {
                s.Url = s.Url.Replace("http:", "https:");
                return s;
            }).ToList();
            var included = context.SchemaRepository.Schemas.Where(x => !x.Value.Deprecated).ToList();

            context.SchemaRepository.Schemas.Clear();
            foreach (var (key, value) in included)
                context.SchemaRepository.Schemas.Add(key, value);
        }

        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            
        }
    }
}
