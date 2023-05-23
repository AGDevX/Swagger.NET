using System;
using AGDevX.Strings;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace AGDevX.Web.Swagger;

public sealed class ConfigureDefault : IConfigureNamedOptions<SwaggerGenOptions>
{
    private readonly IApiVersionDescriptionProvider _apiVersionDescriptionProvider;
    private readonly SwaggerConfig _swaggerConfig;

    public ConfigureDefault(IApiVersionDescriptionProvider apiVersionDescriptionProvider, SwaggerConfig swaggerConfig)
    {
        _apiVersionDescriptionProvider = apiVersionDescriptionProvider;
        _swaggerConfig = swaggerConfig;
    }

    public void Configure(string? name, SwaggerGenOptions options)
    {
        Configure(options);
    }

    public void Configure(SwaggerGenOptions options)
    {
        foreach (var description in _apiVersionDescriptionProvider.ApiVersionDescriptions)
        {
            options.SwaggerDoc(description.GroupName, CreateVersionInfo(description));
        }
        
        options.DescribeAllParametersInCamelCase();
        options.CustomSchemaIds(x => x.FullName);
    }

    private OpenApiInfo CreateVersionInfo(ApiVersionDescription apiVersionDescription)
    {
        var openApiInfo = new OpenApiInfo()
        {
            Title = _swaggerConfig.Title,
            Description = _swaggerConfig.Description,
            Version = apiVersionDescription.ApiVersion.ToString(),
            Contact = new OpenApiContact
            {
                Name = _swaggerConfig.Author,
                Email = _swaggerConfig.AuthorEmail,
                Url = _swaggerConfig.AuthorUrl.IsNotNullNorWhiteSpace() ? new Uri(_swaggerConfig.AuthorUrl) : null
            }
        };

        if (apiVersionDescription.IsDeprecated)
        {
            openApiInfo.Description += "This version of the API is deprecated.";
        }

        return openApiInfo;
    }
}