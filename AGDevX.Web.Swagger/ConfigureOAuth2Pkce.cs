using System;
using AGDevX.Core.Swagger.OperationFilter;
using AGDevX.Strings;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace AGDevX.Web.Swagger;

public sealed class ConfigureOAuth2Pkce : IConfigureNamedOptions<SwaggerGenOptions>
{
    private readonly string _scheme = "OAuth2";

    private readonly IApiVersionDescriptionProvider _provider;
    private readonly SwaggerConfig _swaggerConfig;

    public ConfigureOAuth2Pkce(IApiVersionDescriptionProvider provider, SwaggerConfig swaggerConfig)
    {
        _provider = provider;
        _swaggerConfig = swaggerConfig;
    }

    public void Configure(string? name, SwaggerGenOptions options)
    {
        Configure(options);
    }

    public void Configure(SwaggerGenOptions options)
    {
        options.OperationFilter<AuthorizeOperationFilter>(_scheme);
        options.AddSecurityDefinition(_scheme, new OpenApiSecurityScheme
        {
            Type = SecuritySchemeType.OAuth2,
            Flows = new OpenApiOAuthFlows()
            {
                AuthorizationCode = new OpenApiOAuthFlow
                {
                    AuthorizationUrl = _swaggerConfig.AuthorizationUrl.IsNotNullNorWhiteSpace() ? new Uri(_swaggerConfig.AuthorizationUrl) : null,
                    TokenUrl = _swaggerConfig.TokenUrl.IsNotNullNorWhiteSpace() ? new Uri(_swaggerConfig.TokenUrl) : null,
                    Scopes = _swaggerConfig.Scopes
                }
            },
            Description = $"{ _scheme } - PKCE"
        });
    }
}