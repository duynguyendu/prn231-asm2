namespace Asm2.eBookStore.Client.Models;

public interface IPagingResult
{
    public int CurrentPage { get; set; }
    public int TotalPage { get; set; }
    public int TotalCount { get; set; }
}
