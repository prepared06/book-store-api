using System.ComponentModel.DataAnnotations;

namespace eBook.Models;

public class Author
{   
    [Key]
    public int IdAuthor { get; set; }
    public string? AuthorName { get; set; }

    public List<Book>? Books { get; set; }

    public List<Genre>? Genres { get; set; }
}