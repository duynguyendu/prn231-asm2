using System.Diagnostics;
using Asm2.eBookStore.Api.Dto.Response;
using Microsoft.AspNetCore.Mvc;
using Asm2.eBookStore.Client.Models;
using Microsoft.OData.Client;

namespace Asm2.eBookStore.Client.Controllers;

public class HomeController : BaseController
{
    public HomeController(HttpClient httpClient, DataServiceContext context)
        : base(httpClient, context) { }

    public async Task<IActionResult> Index()
    {
        await GetAsync(QueryOf<BookDto>());
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(
            new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier }
        );
    }
}
