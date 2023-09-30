using Asm2.eBookStore.Api.Dto.Request;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Asm2.eBookStore.Client.Models;

public class BookCreateViewModel : IBookViewModel
{
    // TODO: publishers and authors
    public BookCreateDto BookCreate { get; set; }

    public IList<SelectListItem> Publishers { get; set; } = new List<SelectListItem>();
    public IList<SelectListItem> Authors { get; set; } = new List<SelectListItem>();
}
