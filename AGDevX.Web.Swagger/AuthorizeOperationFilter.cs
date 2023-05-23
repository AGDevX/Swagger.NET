using System.Linq;
using System.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace AGDevX.Core.Swagger.OperationFilter;

public sealed class AuthorizeOperationFilter : IOperationFilter
{
    private readonly string _scheme;

    public AuthorizeOperationFilter(string scheme)
    {
        _scheme = scheme;
    }

    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        var authorize = (context.MethodInfo.DeclaringType != null && context.MethodInfo.DeclaringType.GetCustomAttributes(true).OfType<AuthorizeAttribute>().Any())
                            || context.MethodInfo.GetCustomAttributes(true).OfType<AuthorizeAttribute>().Any();

        var allowAnonymous = context.MethodInfo.GetCustomAttributes(true).OfType<AllowAnonymousAttribute>().Any();

        if (!authorize || allowAnonymous)
        {
            return;
        }

        operation.Responses.Add(StatusCodes.Status401Unauthorized.ToString(), new OpenApiResponse { Description = nameof(HttpStatusCode.Unauthorized) });

        var oauth2SecurityScheme = new OpenApiSecurityScheme()
        {
            Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = _scheme },
        };

        operation.Security.Add(new OpenApiSecurityRequirement()
        {
            [oauth2SecurityScheme] = new[] { _scheme }
        });
    }
}