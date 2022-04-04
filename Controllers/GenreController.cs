using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using eBook.Models;
using eBook.Data;
namespace eBook.Controllers;

[ApiController]
[Route("[controller]")]
public class GenreController: ControllerBase
{   
    private readonly BookContext _context;

    public GenreController(BookContext context)
    {
        _context = context;
    }

    [HttpGet]
    public ActionResult<IEnumerable<Genre>> GetAll()
    {
        var genres = _context.Genres.AsNoTracking().ToList();

        if (genres.Count == 0)
        {
            return NotFound();
        }

        return Ok(genres);
    }   

    [HttpGet("{id}")]
    public ActionResult<Genre> Get(int id)
    {
        var genre = _context.Genres.AsNoTracking().SingleOrDefault(p => p.Id == id);

        if(genre == null)
            return NotFound();

        return Ok(genre);
    }

    [HttpPost]
    public IActionResult Create(Genre genre)
    {           
        if (!ModelState.IsValid)
            return BadRequest("Not a valid model");

        _context.Genres.Add(genre);
        _context.SaveChanges();
        return CreatedAtAction(nameof(Create), new { id = genre.Id }, genre); 
    }

    
    [HttpPut("{id}")]
    public IActionResult Update(int id, Genre genre)
    {
        if (!ModelState.IsValid)
            return BadRequest("Not a valid model");

        if (id != genre.Id)
            return BadRequest();

        var existingGenre = _context.Genres.AsNoTracking().SingleOrDefault(p => p.Id == id);   

        if(existingGenre is null)
            return NotFound(); 
        
        _context.Genres.Update(genre);
        _context.SaveChanges();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        if (id <= 0)
            return BadRequest("Not a valid id");    

        var genreToDelete = _context.Genres.Find(id);
        if (genreToDelete is null)
            return NotFound();

        _context.Genres.Remove(genreToDelete);
        _context.SaveChanges();
        return NoContent();        
    }
}