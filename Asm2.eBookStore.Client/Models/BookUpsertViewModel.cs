using Asm2.eBookStore.Api.Dto.Request;
using Asm2.eBookStore.Api.Dto.Response;

namespace Asm2.eBookStore.Client.Models;

public class BookUpsertViewModel
{
    public BookUpdateDto Book { get; set; }
    public int? Id { get; set; }
}
