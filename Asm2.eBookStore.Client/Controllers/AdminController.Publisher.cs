using Asm2.eBookStore.Api.Dto.Request;
using Asm2.eBookStore.Api.Dto.Response;
using Asm2.eBookStore.Client.Models.Publishers;
using Microsoft.AspNetCore.Mvc;

namespace Asm2.eBookStore.Client.Controllers;

public partial class AdminController
{
    public async Task<IActionResult> Publishers(int page = 1)
    {
        var query = QueryOf<PublisherDto>().AddQueryOption("$count", "true");
        var (_, result) = await GetAsync(query, page, PageSize);
        var publishersViewModel = new PublishersViewModel { Publishers = result!.Value, };
        PreparePaging(publishersViewModel, result, page);
        return View(publishersViewModel);
    }

    public async Task<IActionResult> CreatePublisher()
    {
        return View(new PublisherEditViewModel { Publisher = new PublisherUpdateDto() });
    }

    [HttpPost]
    public async Task<IActionResult> CreatePublisher(PublisherEditViewModel model)
    {
        if (!TryValidateModel(model.Publisher))
        {
            return View(model);
        }
        var (response, _) = await PostAsync(QueryOf<PublisherDto>(), model.Publisher);
        if (!response.IsSuccessStatusCode)
        {
            ViewBag.Error = "Unknown error happened";
            return View(model);
        }
        return RedirectToAction("Publishers");
    }

    public async Task<IActionResult> EditPublisher(int id)
    {
        var (publisherResponse, publisherResult) = await GetSingleAsync(
            QueryOf<PublisherDto>().Where(x => x.Id == id)
        );
        if (!publisherResponse.IsSuccessStatusCode)
        {
            TempData["NotFoundError"] = $"Publisher with {id} not found.";
            return RedirectToAction("Publishers");
        }

        var model = new PublisherEditViewModel
        {
            Publisher = _mapper.Map<PublisherUpdateDto>(publisherResult!),
            Id = id
        };
        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> EditPublisher(PublisherEditViewModel model)
    {
        if (!TryValidateModel(model.Publisher))
        {
            return View(model);
        }
        var response = await PutAsync(
            QueryOf<PublisherDto>().Where(x => x.Id == model.Id),
            model.Publisher
        );
        if (!response.IsSuccessStatusCode)
        {
            ViewBag.Error = "Unknown error happened";
            return View(model);
        }

        return RedirectToAction("Publishers");
    }

    [HttpPost]
    public async Task<IActionResult> DeletePublisher(int id)
    {
        var response = await DeleteAsync(QueryOf<PublisherDto>().Where(x => x.Id == id));
        if (response.IsSuccessStatusCode)
        {
            TempData["ConflictError"] = "Publisher is related with order";
        }
        return RedirectToAction("Publishers");
    }
}
