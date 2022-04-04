using eBook.Models;

namespace eBook.Data;

public static class DbInitializer
{
    public static void Initialize(BookContext context)
    {
        if (context.Books.Any()
                && context.Authors.Any()
                && context.Genres.Any())
            {
                return;   // DB has been seeded
            }

        var classic=new Genre{GenreName="Classics"};
        var actionAndAdventure=new Genre{GenreName="Action and Adventure"};
        var detective = new Genre{GenreName="Detective"};
        var fantasy = new Genre{GenreName="Fantasy"};

        Book TheFellowshipOfTheRing=new Book{
            Name="The Fellowship Of The Ring",
            Price=25m,
            UrlPicture="https://upload.wikimedia.org/wikipedia/en/8/8e/The_Fellowship_of_the_Ring_cover.gif",
            UrlFile="https://s3.amazonaws.com/scschoolfiles/112/j-r-r-tolkien-lord-of-the-rings-01-the-fellowship-of-the-ring-retail-pdf.pdf"           
            };

        var Tolkien = new Author{
         AuthorName="J. R. R. Tolkien",
         Books=new List<Book>{TheFellowshipOfTheRing},
         Genres=new List<Genre>{fantasy,actionAndAdventure}
        };

        TheFellowshipOfTheRing.Authors=Tolkien;
        TheFellowshipOfTheRing.Genres=Tolkien.Genres;

        //--------------------------------------------------------------------------------------------//

        Book Kobzar=new Book{
            Name="Kobzar",
            Price=25m,
            UrlPicture="https://upload.wikimedia.org/wikipedia/commons/thumb/5/5e/Kobzar1840.png/1024px-Kobzar1840.png",
            UrlFile="https://diasporiana.org.ua/wp-content/uploads/books/13894/file.pdf"               
        };
        var Shevchenko = new Author{
            AuthorName="Taras Shevchenko",
            Books=new List<Book>{Kobzar},
            Genres=new List<Genre>{classic}
        };
        Kobzar.Authors=Shevchenko;
        Kobzar.Genres=Shevchenko.Genres;

        context.Genres.AddRange(classic,actionAndAdventure,detective,fantasy);
        context.Authors.AddRange(Tolkien,Shevchenko);
        context.Books.AddRange(TheFellowshipOfTheRing,Kobzar);
        
        context.SaveChanges();
    }
}