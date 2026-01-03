using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using RhPortal.Api.Infrastructure.Tenancy;

namespace RhPortal.Api.Swagger;

public sealed class TenantHeaderOperationFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        operation.Parameters ??= new List<OpenApiParameter>();

        operation.Parameters.Add(new OpenApiParameter
        {
            Name = TenantMiddleware.TenantHeaderName,
            In = ParameterLocation.Header,
            Required = true,
            Description = "Identificador do tenant (ex.: liotecnica, dev)",
            Schema = new OpenApiSchema { Type = "string" }
        });
    }
}
