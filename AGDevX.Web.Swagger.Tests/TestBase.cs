namespace AGDevX.Web.Swagger.Tests;

public class TestBase
{
    public static readonly SwaggerConfig _swaggerConfig = new()
    {
        Enabled = true,
        ApiXmlDocumentationFilename = "SwaggerExtensions.xml",
        Author = "AGDevX",
        AuthorEmail = "civvie11@decino_cheesepopps.com",
        AuthorUrl = "https://github.com/AGDevX",
        Title = "Swagger Extensions",
        Description = "Wrapper that makes configuring Swagger for .NET Core Web APIs simpler",
        AuthorizationUrl = "https://agdevx.auth0.com/authorize",
        TokenUrl = "https://agdevx.auth0.com/oauth/token",
        ClientId = "8b4852e9-2188-451f-a3d6-be618f24176b",
        ClientSecret = "7a723694-1a79-4ce0-b00c-3953f5f911c1",
        Scopes = new() {
                { "api:access", "Access to the API" }
            }
    };
}