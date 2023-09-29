using System.Linq.Dynamic.Core;
using Asm2.eBookStore.Api.Dto.Request;
using Asm2.eBookStore.Api.Dto.Response;
using Asm2.eBookStore.Client.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OData.Client;

namespace Asm2.eBookStore.Client.Controllers;

public partial class AdminController : BaseController
{
    private const int PageSize = 3;
    private readonly IMapper _mapper;

    public AdminController(HttpClient httpClient, DataServiceContext context, IMapper mapper)
        : base(httpClient, context)
    {
        _mapper = mapper;
    }

    public async Task<IActionResult> Books(string title = "", int? price = default, int page = 1)
    {
        var query = QueryOf<BookDto>()
            .AddQueryOption("$count", "true")
            .Where(x => x.Title.Contains(title));
        if (price != null)
        {
            query = query.Where(x => x.Price == price);
        }

        var (_, result) = await GetAsync(query, page, PageSize);
        var booksViewModel = new BooksViewModel
        {
            Books = result!.Value,
            Title = title,
            Price = price
        };
        PreparePaging(booksViewModel, result, page);
        return View(booksViewModel);
    }

    public async Task<IActionResult> UpsertBook(int? id)
    {
        // TODO: publisher and author as well
        BookUpdateDto dto;
        if (id != null)
        {
            var (bookResponse, bookResult) = await GetSingleAsync(
                QueryOf<BookDto>().Where(x => x.Id == id)
            );
            if (!bookResponse.IsSuccessStatusCode)
            {
                TempData["NotFoundError"] = $"Book with {id} not found.";
                return RedirectToAction("Books");
            }

            dto = _mapper.Map<BookUpdateDto>(bookResult!);
        }
        else
        {
            dto = new BookUpdateDto();
        }
        var model = new BookUpsertViewModel() { Id = id, Book = dto };
        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> UpsertBook(BookUpsertViewModel model)
    {
        if (!TryValidateModel(model.Book))
        {
            return View(model);
        }

        HttpResponseMessage response;
        if (model.Id != null)
        {
            response = await PutAsync(QueryOf<BookDto>().Where(x => x.Id == model.Id), model.Book);
        }
        else
        {
            (response, _) = await PostAsync(QueryOf<BookDto>(), model.Book);
        }

        if (!response.IsSuccessStatusCode)
        {
            ViewBag.Error = "Unknown error happened";
            return View(model);
        }

        return RedirectToAction("Books");
    }

    [HttpPost]
    public async Task<IActionResult> DeleteBook(int id)
    {
        var response = await DeleteAsync(QueryOf<BookDto>().Where(x => x.Id == id));
        if (response.IsSuccessStatusCode)
        {
            TempData["ConflictError"] = "Book is related with order";
        }
        return RedirectToAction("Books");
    }

    private static void PreparePaging<T>(
        IPagingResult pagingResult,
        ODataResponse<T> response,
        int currentPage
    )
    {
        pagingResult.CurrentPage = currentPage;
        pagingResult.TotalPage = (response.TotalCount + PageSize - 1) / PageSize;
        pagingResult.TotalCount = response.TotalCount;
    }
}
