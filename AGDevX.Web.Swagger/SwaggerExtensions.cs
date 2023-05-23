using System;
using System.IO;
using System.Linq;
using AGDevX.Strings;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.DependencyInjection;

namespace AGDevX.Web.Swagger;

public static class SwaggerExtensions
{
    public static void AddSwaggerToApi(this IServiceCollection services, SwaggerConfig swaggerConfig)
    {
        if (swaggerConfig.Enabled)
        {
            services.AddSingleton(swaggerConfig);

            var isApiVersioningAlreadyAdded = services.Any(s => s.ServiceType == typeof(IApiVersionReader));

            if (!isApiVersioningAlreadyAdded)
            {
                services.AddApiVersioning(options =>
                {
                    options.DefaultApiVersion = new ApiVersion(1, 0);
                    options.AssumeDefaultVersionWhenUnspecified = true;
                    options.ReportApiVersions = true;
                    options.ApiVersionReader = ApiVersionReader.Combine(new UrlSegmentApiVersionReader(),
                                                                        new HeaderApiVersionReader("x-api-version"),
                                                                        new MediaTypeApiVersionReader("x-api-version"));
                });
            }

            var isVersionedApiExplorerAlreadyAdded = services.Any(s => s.ImplementationType == typeof(DefaultApiVersionDescriptionProvider));

            if (!isVersionedApiExplorerAlreadyAdded)
            {
                services.AddVersionedApiExplorer(options =>
                {
                    options.GroupNameFormat = "'v'VVV";
                    options.SubstituteApiVersionInUrl = true;
                });
            }

            services.AddSwaggerGen(options =>
            {
                if (!swaggerConfig.ApiXmlDocumentationFilename.IsNullOrWhiteSpace())
                {
                    var xmlPath = Path.Combine(AppContext.BaseDirectory, swaggerConfig.ApiXmlDocumentationFilename!);
                    options.IncludeXmlComments(xmlPath, includeControllerXmlComments: true);
                }
            });

            services.ConfigureOptions<ConfigureDefault>();
            services.ConfigureOptions<ConfigureOAuth2Pkce>();
        }
    }

    public static void UseSwaggerForApi(this WebApplication webApp)
    {
        var apiVersionDescriptionProvider = webApp.Services.GetRequiredService<IApiVersionDescriptionProvider>();

        var swaggerConfig = webApp.Services.GetRequiredService<SwaggerConfig>();

        if (swaggerConfig.Enabled)
        {
            webApp.UseSwagger();
            webApp.UseSwaggerUI(options =>
            {
                foreach (var description in apiVersionDescriptionProvider.ApiVersionDescriptions.Reverse())
                {
                    options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", description.GroupName.ToUpperInvariant());
                }

                if (swaggerConfig.IsConfiguredForOAuth2())
                {
                    options.OAuthAppName(swaggerConfig.Title);
                    options.OAuthClientId(swaggerConfig.ClientId);
                    options.OAuthClientSecret(swaggerConfig.ClientSecret);
                    options.OAuthScopeSeparator(" ");
                    options.OAuthUsePkce();
                }
            });
        }
    }
}