using System.ComponentModel.DataAnnotations;

namespace eBook.Models;

public class Book
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public decimal Price { get; set; }
    public string? UrlPicture { get; set; }
    public string? UrlFile { get; set; }
    
    public Author? Authors { get; set; }
    public List<Genre>? Genres { get; set; }
}