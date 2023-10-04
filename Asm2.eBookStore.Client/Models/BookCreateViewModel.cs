using Asm2.eBookStore.Api.Dto.Request;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Asm2.eBookStore.Client.Models;

public class BookCreateViewModel : IBookViewModel
{
    public BookCreateDto BookCreate { get; set; }
    public int AuthorId { get; set; }
    public IList<SelectListItem> Publishers { get; set; } = new List<SelectListItem>();
    public IList<SelectListItem> Authors { get; set; } = new List<SelectListItem>();
}
