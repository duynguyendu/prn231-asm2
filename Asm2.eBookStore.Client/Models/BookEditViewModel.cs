using Asm2.eBookStore.Api.Dto.Request;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Asm2.eBookStore.Client.Models;

public class BookEditViewModel : IBookViewModel
{
    public int Id { get; set; }
    public BookUpdateDto BookUpdate { get; set; }
    public IList<SelectListItem> Publishers { get; set; } = new List<SelectListItem>();
    public IList<SelectListItem> Authors { get; set; } = new List<SelectListItem>();
}
