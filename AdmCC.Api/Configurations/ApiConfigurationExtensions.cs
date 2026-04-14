using System.Text.Json.Serialization;
using Scalar.AspNetCore;

namespace AdmCC.Api.Configurations
{
    public static class ApiConfigurationExtensions
    {
        public static IServiceCollection AddApiPresentation(this IServiceCollection services)
        {
            services
                .AddControllers()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                });

            services.AddOpenApi("v1");

            return services;
        }

        public static WebApplication UseApiDocumentation(this WebApplication app)
        {
            app.MapOpenApi("/openapi/{documentName}.json");
            app.MapScalarApiReference(options =>
            {
                options.WithTitle("AdmCC API");
                options.WithOpenApiRoutePattern("/openapi/{documentName}.json");
            });

            return app;
        }
    }
}
