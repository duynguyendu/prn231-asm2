using System.Net;
using Asm2.eBookStore.Api.Dto;
using Asm2.eBookStore.Api.Dto.Response;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OData.Client;

namespace Asm2.eBookStore.Client.Controllers;

public class AuthController : BaseController
{
    private const string LoginUri = "api/auth/login";

    public AuthController(HttpClient httpClient, DataServiceContext context)
        : base(httpClient, context) { }

    public IActionResult Login()
    {
        var authCookie = HttpContext.Session.GetString("AuthCookie");
        if (authCookie == null)
        {
            return View();
        }

        return RedirectToAction("Books", "Admin");
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginCredential credential)
    {
        var (response, loginDto) = await PostAsync<LoginDto>(LoginUri, credential);
        if (response.StatusCode == HttpStatusCode.Unauthorized)
        {
            ViewBag.Error = "Wrong email or password";
            return View();
        }

        if (!response.IsSuccessStatusCode)
        {
            ViewBag.Error = "Error while login, please try again";
            return View();
        }

        response.Headers.TryGetValues("Set-Cookie", out var values);
        SaveAuthCookie(values!.First());
        HttpContext.Session.SetString("Email", loginDto!.Email);
        HttpContext.Session.SetString("Role", loginDto.Role);
        return RedirectToAction("Books", "Admin");
    }

    private void SaveAuthCookie(string values)
    {
        HttpContext.Session.SetString("AuthCookie", values);
    }
}
