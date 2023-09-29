namespace Asm2.eBookStore.Client.Middlewares;

public class AuthenticationMiddleware : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        var isAllow = Array.Exists(
            Constants.AllowUrl,
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

        var authCookie = context.Session.GetString("AuthCookie");
        if (authCookie == null)
        {
            context.Items.Add("Error", "Please login before doing anything");
            context.Response.Redirect("/Auth/Login");
            return;
        }

        await next(context);
    }
}
