namespace FirstApp;

public class MiddleC
{
    public RequestDelegate _next;

    public MiddleC(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke (HttpContext context)
    {
        await context.Response.WriteAsync(" Middle C\n");
        await _next(context);
    }

}
