using Xunit;

namespace AGDevX.Web.Swagger.Tests;

public class SwaggerConfigExtensionsTests : TestBase
{
    public class When_calling_IsConfiguredForOAuth2
    {
        [Fact]
        public void With_oauth2_properly_configured_then_return_true()
        {
            //-- Arrange
            var swaggerConfig = _swaggerConfig.Clone();

            //-- Act
            var isConfiguredForOAuth2 = swaggerConfig.IsConfiguredForOAuth2();

            //-- Assert
            Assert.True(isConfiguredForOAuth2);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void With_missing_authorization_url_then_return_false(string authorizationUrl)
        {
            //-- Arrange
            var swaggerConfig = _swaggerConfig.Clone();
            swaggerConfig.AuthorizationUrl = authorizationUrl;

            //-- Act
            var isConfiguredForOAuth2 = swaggerConfig.IsConfiguredForOAuth2();

            //-- Assert
            Assert.False(isConfiguredForOAuth2);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void With_missing_token_url_then_return_false(string tokenUrl)
        {
            //-- Arrange
            var swaggerConfig = _swaggerConfig.Clone();
            swaggerConfig.TokenUrl = tokenUrl;

            //-- Act
            var isConfiguredForOAuth2 = swaggerConfig.IsConfiguredForOAuth2();

            //-- Assert
            Assert.False(isConfiguredForOAuth2);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void With_missing_client_id_then_return_false(string clientId)
        {
            //-- Arrange
            var swaggerConfig = _swaggerConfig.Clone();
            swaggerConfig.ClientId = clientId;

            //-- Act
            var isConfiguredForOAuth2 = swaggerConfig.IsConfiguredForOAuth2();

            //-- Assert
            Assert.False(isConfiguredForOAuth2);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void With_missing_client_secret_then_return_false(string clientSecret)
        {
            //-- Arrange
            var swaggerConfig = _swaggerConfig.Clone();
            swaggerConfig.ClientSecret = clientSecret;

            //-- Act
            var isConfiguredForOAuth2 = swaggerConfig.IsConfiguredForOAuth2();

            //-- Assert
            Assert.False(isConfiguredForOAuth2);
        }

        [Fact]
        public void With_missing_scopes_then_return_false()
        {
            //-- Arrange
            var swaggerConfig = _swaggerConfig.Clone();
            swaggerConfig.Scopes = new Dictionary<string, string>();

            //-- Act
            var isConfiguredForOAuth2 = swaggerConfig.IsConfiguredForOAuth2();

            //-- Assert
            Assert.False(isConfiguredForOAuth2);
        }
    }
}