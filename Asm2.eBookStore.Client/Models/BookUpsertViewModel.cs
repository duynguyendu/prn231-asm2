﻿using Asm2.eBookStore.Api.Dto.Request;
using Asm2.eBookStore.Api.Dto.Response;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Asm2.eBookStore.Client.Models;

public class BookUpsertViewModel
{
    public BookUpdateDto BookUpdate { get; set; }
    public BookCreateDto BookCreate { get; set; }
    public IList<SelectListItem> Publishers { get; set; } = new List<SelectListItem>();
    public IList<SelectListItem> Authors { get; set; } = new List<SelectListItem>();
    public int? Id { get; set; }
}
