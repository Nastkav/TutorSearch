namespace Web.Middlewares;

public class RemovedRoleMiddleware
{
    private readonly RequestDelegate _next;
    private const string Path = "/RemoveUser/Removed";

    public RemovedRoleMiddleware(RequestDelegate next) => _next = next;

    public async Task Invoke(HttpContext context)
    {
        // Перевірка автентифікований, ролі та сторінки
        if (context.User.Identity.IsAuthenticated && context.User.IsInRole("Removed"))
            if (!context.Request.Path.HasValue || !context.Request.Path.Value.StartsWith("/RemoveUser"))
            {
                context.Response.Redirect(Path);
                return;
            }


        await _next(context);
    }
}