using System.Security.Claims;
using Asm2.eBookStore.Api.ApiModel;
using Asm2.eBookStore.Api.ApiModel.Response;
using Asm2.eBookStore.Service;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Asm2.eBookStore.Api.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : Controller
{
    private readonly UsersService _usersService;

    public AuthController(UsersService usersService)
    {
        _usersService = usersService;
    }

    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<ActionResult> Login([FromBody] LoginCredential credential)
    {
        var user = _usersService.Login(credential.Email!, credential.Password!);
        var isAdmin = IsAdmin(credential);
        if (user == null && !isAdmin)
        {
            return Unauthorized();
        }

        var roleName = isAdmin ? "admin" : "member";
        var claims = new List<Claim>
        {
            new(type: ClaimTypes.Email, value: credential.Email!),
            new(type: ClaimTypes.Role, value: roleName),
            new(type: ClaimTypes.Sid, value: user?.UserId.ToString() ?? "-1")
        };

        await RegisterClaims(claims);
        var login = new LoginDto { Email = credential.Email!, Role = roleName };
        return Ok(login);
    }

    [HttpPost("signout")]
    [Authorize]
    public async Task Signout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
    }

    private async Task RegisterClaims(List<Claim> claims)
    {
        var identity = new ClaimsIdentity(
            claims,
            CookieAuthenticationDefaults.AuthenticationScheme
        );
        await HttpContext.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme,
            new ClaimsPrincipal(identity),
            new AuthenticationProperties
            {
                IsPersistent = true,
                AllowRefresh = true,
                ExpiresUtc = DateTimeOffset.UtcNow.AddDays(10)
            }
        );
    }

    private static bool IsAdmin(LoginCredential loginCredential)
    {
        var (email, password) = GetAdminCredential();
        return email == loginCredential.Email && password == loginCredential.Password;
    }

    private static (string email, string password) GetAdminCredential()
    {
        var options = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
        var adminEmail = options.GetSection("Admin").GetSection("Email").Value;
        var adminPassword = options.GetSection("Admin").GetSection("Password").Value;
        return (adminEmail!, adminPassword!);
    }
}
