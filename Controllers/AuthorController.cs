using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using eBook.Models;
using eBook.Data;
namespace eBook.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthorController: ControllerBase
{
    private readonly BookContext _context;

    public AuthorController(BookContext context)
    {
        _context = context;
    }

    [HttpGet]
    public ActionResult<IEnumerable<Author>> GetAll()
    {
        var authors = _context.Authors.Include(m=>m.Genres).Include(m=>m.Books).ToList();

        if (authors.Count == 0)
        {
            return NotFound();
        }

        return Ok(authors);
    }   

    [HttpGet("{id}")]
    public ActionResult<Author> Get(int id)
    {
        var author = _context.Authors.Include(m=>m.Genres).Include(m=>m.Books).SingleOrDefault(p => p.IdAuthor == id);

        if(author == null)
            return NotFound();

        return Ok(author);
    }
    [HttpPost]
    public IActionResult Create(Author author)
    {           
        if (!ModelState.IsValid)
            return BadRequest("Not a valid model");

        _context.Authors.Add(author);
        _context.SaveChanges();
        return CreatedAtAction(nameof(Create), new { id = author.IdAuthor }, author); 
    }
    [HttpPut("{id}")]
    public IActionResult Update(int id, Genre author)
    {
        if (!ModelState.IsValid)
            return BadRequest("Not a valid model");

        if (id != author.Id)
            return BadRequest();

        var existingAuthor = _context.Authors.AsNoTracking().SingleOrDefault(p => p.IdAuthor == id);   

        if(existingAuthor is null)
            return NotFound(); 
        
        _context.Genres.Update(author);
        _context.SaveChanges();

        return NoContent();
    }
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        if (id <= 0)
            return BadRequest("Not a valid id");    

        var authorToDelete = _context.Authors.Find(id);
        if (authorToDelete is null)
            return NotFound();

        _context.Authors.Remove(authorToDelete);
        _context.SaveChanges();
        return NoContent();        
    }
}