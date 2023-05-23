namespace AGDevX.Web.Swagger.Tests;

public static class SwaggerConfigExtensions
{
    public static SwaggerConfig Clone(this SwaggerConfig swaggerConfig)
    {
        return new SwaggerConfig
        {
            Enabled = swaggerConfig.Enabled,
            ApiXmlDocumentationFilename = swaggerConfig.ApiXmlDocumentationFilename,
            Author = swaggerConfig.Author,
            AuthorEmail = swaggerConfig.Author,
            AuthorUrl = swaggerConfig.AuthorUrl,
            Title = swaggerConfig.Title,
            Description = swaggerConfig.Description,
            AuthorizationUrl = swaggerConfig.AuthorizationUrl,
            TokenUrl = swaggerConfig.TokenUrl,
            ClientId = swaggerConfig.ClientId,
            ClientSecret = swaggerConfig.ClientSecret,
            Scopes = swaggerConfig.Scopes
        };
    }
}