using Asm2.eBookStore.Api.Dto.Request;
using Asm2.eBookStore.Api.Dto.Response;
using Asm2.eBookStore.Client.Models.Authors;
using Microsoft.AspNetCore.Mvc;

namespace Asm2.eBookStore.Client.Controllers;

public partial class AdminController
{
    public async Task<IActionResult> Authors(int page = 1)
    {
        var query = QueryOf<AuthorDto>().AddQueryOption("$count", "true");
        var (_, result) = await GetAsync(query, page, PageSize);
        var authorsViewModel = new AuthorsViewModel { Authors = result!.Value, };
        PreparePaging(authorsViewModel, result, page);
        return View(authorsViewModel);
    }

    public async Task<IActionResult> CreateAuthor()
    {
        return View(new AuthorEditViewModel { Author = new AuthorUpdateDto() });
    }

    [HttpPost]
    public async Task<IActionResult> CreateAuthor(AuthorEditViewModel model)
    {
        if (!TryValidateModel(model.Author))
        {
            return View(model);
        }
        var (response, _) = await PostAsync(QueryOf<AuthorDto>(), model.Author);
        if (!response.IsSuccessStatusCode)
        {
            ViewBag.Error = "Unknown error happened";
            return View(model);
        }
        return RedirectToAction("Authors");
    }

    public async Task<IActionResult> EditAuthor(int id)
    {
        var (authorResponse, authorResult) = await GetSingleAsync(
            QueryOf<AuthorDto>().Where(x => x.Id == id)
        );
        if (!authorResponse.IsSuccessStatusCode)
        {
            TempData["NotFoundError"] = $"Author with {id} not found.";
            return RedirectToAction("Authors");
        }

        var model = new AuthorEditViewModel
        {
            Author = _mapper.Map<AuthorUpdateDto>(authorResult!),
            Id = id
        };
        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> EditAuthor(AuthorEditViewModel model)
    {
        if (!TryValidateModel(model.Author))
        {
            return View(model);
        }
        var response = await PutAsync(
            QueryOf<AuthorDto>().Where(x => x.Id == model.Id),
            model.Author
        );
        if (!response.IsSuccessStatusCode)
        {
            ViewBag.Error = "Unknown error happened";
            return View(model);
        }

        return RedirectToAction("Authors");
    }

    [HttpPost]
    public async Task<IActionResult> DeleteAuthor(int id)
    {
        var response = await DeleteAsync(QueryOf<AuthorDto>().Where(x => x.Id == id));
        if (!response.IsSuccessStatusCode)
        {
            TempData["ConflictError"] = "Author is related with books";
        }
        return RedirectToAction("Authors");
    }
}
