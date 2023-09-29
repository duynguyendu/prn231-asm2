namespace Asm2.eBookStore.Client.Middlewares;

using static Constants;

public class AuthorizationMiddleware : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        var isAllow = Array.Exists(
            AllowUrl,
            url =>
                string.Compare(
                    context.Request.Path.Value!.ToLower(),
                    url.ToLower(),
                    StringComparison.Ordinal
                ) == 0
        );
        if (isAllow)
        {
            await next(context);
            return;
        }

        var role = context.Session.GetString("Role");
        var allow =
            (role == Admin && context.Request.Path.Value!.ToLower().StartsWith("admin"))
            || role == Member;
        if (!allow)
        {
            context.Response.Redirect("/Error");
        }
        await next(context);
    }
}
