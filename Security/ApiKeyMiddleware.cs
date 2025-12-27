namespace TechnicalTestApi.Security;

public class ApiKeyMiddleware
{
    private readonly RequestDelegate _next;
    private readonly string _apiKey;
    private readonly string _headerName;

    public ApiKeyMiddleware(RequestDelegate next, IConfiguration config)
    {
        _next = next;

        _apiKey = config["ApiKey:Value"]
            ?? throw new InvalidOperationException("API Key value not configured");

        _headerName = config["ApiKey:HeaderName"] ?? "X-API-KEY";
    }

    public async Task InvokeAsync(HttpContext context)
    {
        if (!context.Request.Headers.TryGetValue(_headerName, out var extractedKey) ||
            extractedKey != _apiKey)
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            await context.Response.WriteAsync("Unauthorized");
            return;
        }

        await _next(context);
    }
}
