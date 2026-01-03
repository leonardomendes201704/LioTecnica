using Microsoft.AspNetCore.Http;

namespace RhPortal.Api.Infrastructure.Tenancy;

public sealed class TenantMiddleware : IMiddleware
{
    public const string TenantHeaderName = "X-Tenant-Id";
    private readonly ITenantContext _tenantContext;

    public TenantMiddleware(ITenantContext tenantContext)
    {
        _tenantContext = tenantContext;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        if (!context.Request.Headers.TryGetValue(TenantHeaderName, out var tenantValues))
        {
            context.Response.StatusCode = StatusCodes.Status400BadRequest;
            await context.Response.WriteAsync($"Header obrigatório ausente: {TenantHeaderName}");
            return;
        }

        _tenantContext.SetTenantId(tenantValues.ToString());
        await next(context);
    }
}
