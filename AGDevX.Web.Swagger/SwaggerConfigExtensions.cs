using AGDevX.IEnumerables;
using AGDevX.Strings;

namespace AGDevX.Web.Swagger;

public static class SwaggerConfigExtensions
{
    public static bool IsConfiguredForOAuth2(this SwaggerConfig swaggerConfig)
    {
        return swaggerConfig.AuthorizationUrl.IsNotNullNorWhiteSpace()
                && swaggerConfig.TokenUrl.IsNotNullNorWhiteSpace()
                && swaggerConfig.ClientId.IsNotNullNorWhiteSpace()
                && swaggerConfig.ClientSecret.IsNotNullNorWhiteSpace()
                && swaggerConfig.Scopes.AnySafe();
    }
}