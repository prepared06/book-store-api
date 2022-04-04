using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using eBook.Models;
using eBook.Data;
namespace eBook.Controllers;

[ApiController]
[Route("[controller]")]
public class BookController: ControllerBase
{
    private readonly BookContext _context;

    public BookController(BookContext context)
    {
        _context = context;
    }

    [HttpGet]
    public ActionResult<IEnumerable<Book>> GetAll()
    {
        var books = _context.Books.Include(m=>m.Genres).Include(m=>m.Authors).ToList();

        if (books.Count == 0)
        {
            return NotFound();
        }

        return Ok(books);
    }   
    [HttpGet("{id}")]
    public ActionResult<Book> Get(int id)
    {
        var books = _context.Books.Include(m=>m.Genres).Include(m=>m.Authors).SingleOrDefault(p => p.Id == id);

        if(books == null)
            return NotFound();

        return Ok(books);
    }
    [HttpPost]
    public IActionResult Create(Book book)
    {           
        if (!ModelState.IsValid)
            return BadRequest("Not a valid model");

        _context.Books.Add(book);
        _context.SaveChanges();
        return CreatedAtAction(nameof(Create), new { id = book.Id }, book); 
    }
    [HttpPut("{id}")]
    public IActionResult Update(int id, Book book)
    {
        if (!ModelState.IsValid)
            return BadRequest("Not a valid model");

        if (id != book.Id)
            return BadRequest();

        var existingBook = _context.Books.SingleOrDefault(p => p.Id == id);   

        if(existingBook is null)
            return NotFound(); 
        
        _context.Books.Update(book);
        _context.SaveChanges();

        return NoContent();
    }
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        if (id <= 0)
            return BadRequest("Not a valid id");    

        var bookToDelete = _context.Books.Find(id);
        if (bookToDelete is null)
            return NotFound();

        _context.Books.Remove(bookToDelete);
        _context.SaveChanges();
        return NoContent();        
    }
}