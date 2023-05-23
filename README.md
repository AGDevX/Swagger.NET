# AGDevX Swagger .NET

AGDevX Swagger is a wrapper that makes configuring Swagger for .NET Core Web APIs simpler.

# Features

- Supports both unauthorized endpoints and endpoints secured by the OAuth 2.0 protocol
- UI authentication handled by the OIDC PKCE flow
- API Versioning
- Simple configuration
- Optionally displays API owner and contact information in the Swagger UI
- Displays endpoint XML comments

# How To Use

## Configuring Services

There are only two steps to get Swagger configured.

The first is to new up a `SwaggerConfig` object.

If you do not want to display author information, the following properties can be ignored:

- `Author`
- `AuthorEmail` (this is optional anyway)

If there aren't any OAuth 2.0 protected endpoints, the following properties can be ignored:

- `AuthorizationUrl`
- `TokenUrl`
- `ClientId`
- `ClientSecret`
- `Scopes`

```
var swaggerConfig = new SwaggerConfig
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
```

The second is to call the `AddSwaggerToApi` extension method on the `IServiceCollection` object and with the `SwaggerConfig` object.

```
services.AddSwaggerToApi(swaggerConfig);
```

## Adding the Middleware

All that is needed here is to call `UseSwaggerForApi`.

```
 webApi.UseSwaggerForApi();
```

# Tech Debt

- Additional unit tests
