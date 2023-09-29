using System.ComponentModel.DataAnnotations;

namespace Asm2.eBookStore.Api.Dto.Response;

public class PublisherDto : IODataEntity
{
    public static string EntitySet => "Publishers";

    [Key]
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string? City { get; set; }
    public string? State { get; set; }
    public string? Country { get; set; }
}
