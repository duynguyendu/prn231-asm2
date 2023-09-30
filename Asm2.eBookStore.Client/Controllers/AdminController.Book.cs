using Asm2.eBookStore.Api.Dto.Request;
using Asm2.eBookStore.Api.Dto.Response;
using Asm2.eBookStore.Client.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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

    public async Task<IActionResult> CreateBook()
    {
        var model = new BookCreateViewModel();
        await PrepareModel(model);
        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> CreateBook(BookCreateViewModel model)
    {
        if (!TryValidateModel(model.BookCreate))
        {
            await PrepareModel(model);
            return View(model);
        }
        return RedirectToAction("Books");
    }

    public async Task<IActionResult> EditBook(int id)
    {
        var (bookResponse, bookResult) = await GetSingleAsync(
            QueryOf<BookDto>().Where(x => x.Id == id)
        );
        if (!bookResponse.IsSuccessStatusCode)
        {
            TempData["NotFoundError"] = $"Book with {id} not found.";
            return RedirectToAction("Books");
        }

        var model = new BookEditViewModel
        {
            BookUpdate = _mapper.Map<BookUpdateDto>(bookResult!),
            Id = id
        };
        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> EditBook(BookEditViewModel model)
    {
        if (!TryValidateModel(model.BookUpdate))
        {
            await PrepareModel(model);
            return View(model);
        }
        var response = await PutAsync(
            QueryOf<BookDto>().Where(x => x.Id == model.Id),
            model.BookUpdate
        );
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

    private async Task PrepareModel(IBookViewModel model)
    {
        // prepare publishers and authors
        var (_, publishers) = await GetAsync(QueryOf<PublisherDto>());
        var publisherSelectItems = publishers!.Value
            .Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Name })
            .ToList();

        var (_, authors) = await GetAsync(QueryOf<AuthorDto>());
        var authorSelectItems = authors!.Value
            .Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.FullName })
            .ToList();
        model.Publishers = publisherSelectItems;
        model.Authors = authorSelectItems;
        // model.BookDto.BookDetails = GetBookDetails();
    }

    // private void SaveBookAuthors(BookAuthorCreateDto dto)
    // {
    //     var dtos = GetBookAuthors();
    //
    //     dtos = dtos.Where(x => x.ProductId != dto.ProductId).ToList();
    //     dtos.Add(dto);
    //     HttpContext.Session.SetString("orderAuthors", JsonConvert.SerializeObject(dtos));
    // }
    //
    // private IList<BookAuthorCreateDto> GetBookAuthors()
    // {
    //     var session = HttpContext.Session;
    //     var sessionDto = session.GetString("orderAuthors");
    //     IList<BookAuthorCreateDto> dtos;
    //     if (!sessionDto.IsNullOrEmpty())
    //     {
    //         dtos = JsonConvert.DeserializeObject<List<BookAuthorCreateDto>>(sessionDto);
    //     }
    //     else
    //     {
    //         dtos = new List<BookAuthorCreateDto>();
    //     }
    //
    //     if (dtos == null)
    //     {
    //         dtos = new List<BookAuthorCreateDto>();
    //     }
    //
    //     return dtos;
    // }
    //
    // private void RemoveBookAuthor(int productId)
    // {
    //     var dtos = GetBookAuthors();
    //     dtos = dtos.Where(x => x.ProductId != productId).ToList();
    //     HttpContext.Session.SetString("orderAuthors", JsonConvert.SerializeObject(dtos));
    // }
}
