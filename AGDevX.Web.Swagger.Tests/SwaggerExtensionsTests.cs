using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace AGDevX.Web.Swagger.Tests;

public class SwaggerExtensionsTestsTests : TestBase
{
    public class When_calling_AddSwaggerToApi
    {
        [Fact]
        public void And_swagger_is_enabled_then_add_required_services()
        {
            //-- Arrange
            var services = new ServiceCollection();
            var swaggerConfig = _swaggerConfig.Clone();

            //-- Act
            services.AddSwaggerToApi(swaggerConfig);

            //-- Assert
            Assert.Contains(typeof(SwaggerConfig), services.Select(s => s.ServiceType));
            Assert.Contains(typeof(IApiVersionReader), services.Select(s => s.ServiceType));
            Assert.Contains(typeof(DefaultApiVersionDescriptionProvider), services.Select(s => s.ImplementationType));
            Assert.Contains(typeof(ConfigureDefault), services.Select(s => s.ImplementationType));
            Assert.Contains(typeof(ConfigureOAuth2Pkce), services.Select(s => s.ImplementationType));
        }

        [Fact]
        public void With_ApiVersioning_already_added_and_swagger_is_enabled_then_add_required_services()
        {
            //-- Arrange
            var services = new ServiceCollection();
            services.AddApiVersioning();
            var swaggerConfig = _swaggerConfig.Clone();

            //-- Act
            services.AddSwaggerToApi(swaggerConfig);

            //-- Assert
            Assert.Contains(typeof(SwaggerConfig), services.Select(s => s.ServiceType));
            Assert.Contains(typeof(IApiVersionReader), services.Select(s => s.ServiceType));
            Assert.Contains(typeof(DefaultApiVersionDescriptionProvider), services.Select(s => s.ImplementationType));
            Assert.Contains(typeof(ConfigureDefault), services.Select(s => s.ImplementationType));
            Assert.Contains(typeof(ConfigureOAuth2Pkce), services.Select(s => s.ImplementationType));
        }

        [Fact]
        public void With_VersionedApiExplorer_already_added_and_swagger_is_enabled_then_add_required_services()
        {
            //-- Arrange
            var services = new ServiceCollection();
            services.AddVersionedApiExplorer();
            var swaggerConfig = _swaggerConfig.Clone();

            //-- Act
            services.AddSwaggerToApi(swaggerConfig);

            //-- Assert
            Assert.Contains(typeof(SwaggerConfig), services.Select(s => s.ServiceType));
            Assert.Contains(typeof(IApiVersionReader), services.Select(s => s.ServiceType));
            Assert.Contains(typeof(DefaultApiVersionDescriptionProvider), services.Select(s => s.ImplementationType));
            Assert.Contains(typeof(ConfigureDefault), services.Select(s => s.ImplementationType));
            Assert.Contains(typeof(ConfigureOAuth2Pkce), services.Select(s => s.ImplementationType));
        }

        [Fact]
        public void And_swagger_is_not_enabled_then_do_not_add_required_services()
        {
            //-- Arrange
            var services = new ServiceCollection();
            var swaggerConfig = _swaggerConfig.Clone();
            swaggerConfig.Enabled = false;

            //-- Act
            services.AddSwaggerToApi(swaggerConfig);

            //-- Assert
            Assert.DoesNotContain(typeof(SwaggerConfig), services.Select(s => s.ServiceType));
            Assert.DoesNotContain(typeof(IApiVersionReader), services.Select(s => s.ServiceType));
            Assert.DoesNotContain(typeof(DefaultApiVersionDescriptionProvider), services.Select(s => s.ImplementationType));
            Assert.DoesNotContain(typeof(ConfigureDefault), services.Select(s => s.ImplementationType));
            Assert.DoesNotContain(typeof(ConfigureOAuth2Pkce), services.Select(s => s.ImplementationType));
        }
    }

    public class When_calling_UseSwaggerForApi
    {
        [Fact]
        public void And_swagger_is_enabled_then_do_not_throw_exception()
        {
            //-- Arrange
            var swaggerConfig = _swaggerConfig.Clone();

            var webApplicationBuilder = WebApplication.CreateBuilder();
            webApplicationBuilder.Services.AddSwaggerToApi(swaggerConfig);
            var webApplication = webApplicationBuilder.Build();

            //-- Act & Implied Assert
            webApplication.UseSwaggerForApi();
        }

        [Fact]
        public void And_swagger_is_not_enabled_then_throw_exception()
        {
            //-- Arrange
            var swaggerConfig = _swaggerConfig.Clone();
            swaggerConfig.Enabled = false;

            var webApplication = WebApplication.Create();

            //-- Act & Assert   
            Assert.Throws<InvalidOperationException>(webApplication.UseSwaggerForApi);
        }
    }
}