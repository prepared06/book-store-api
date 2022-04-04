using Microsoft.EntityFrameworkCore;
using eBook.Models;

namespace eBook.Data;

public class BookContext : DbContext
{
    public BookContext (DbContextOptions<BookContext> options)
        : base(options)
    {
    }

    public DbSet<Book> Books => Set<Book>();
    public DbSet<Author> Authors => Set<Author>();
    public DbSet<Genre> Genres => Set<Genre>();
}