using Microsoft.AspNetCore.Mvc.Rendering;

namespace Asm2.eBookStore.Client.Models;

public interface IBookViewModel
{
    public IList<SelectListItem> Publishers { get; set; }
    public IList<SelectListItem> Authors { get; set; }
}
