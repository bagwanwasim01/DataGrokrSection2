using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DataGrokrA2.BDL;
using DataGrokrA2.Models;

namespace DataGrokr2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly LibraryContext _context;

        public AuthorsController(LibraryContext context)
        {
            _context = context;
        }

        // GET: api/Authors
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AuthorsBDL>>> GetAuthors()
        {
            return await _context.Authors
                .Select(authors => new AuthorsBDL()
                {
                    AuthorId = authors.AuthorId,
                    Abbrv = authors.Abbrv,
                    FirstName = authors.FirstName,
                    LastName = authors.LastName,
                    BookId = authors.BookId
                })
                .ToListAsync();
        }

        // GET: api/Authors/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AuthorsBDL>> GetAuthor(int id)
        {
            var author = await _context.Authors.FindAsync(id);

            if (author == null)
            {
                return NotFound("Author details not found");
            }
            else
            {
                return await _context.Authors
                    .Where(a => a.AuthorId == id)
                       .Select(authors => new AuthorsBDL()
                       {
                           AuthorId = authors.AuthorId,
                           Abbrv = authors.Abbrv,
                           FirstName = authors.FirstName,
                           LastName = authors.LastName,
                           BookId = authors.BookId
                       })
                       .FirstOrDefaultAsync();
            }           
        }

        // PUT: api/Authors/5       
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAuthor(int id, [FromForm] AuthorsBDL author)
        {
            var authorDetails = await _context.Authors.FindAsync(id);
            if (authorDetails == null)
            {
                return BadRequest(new { message = "Author not found" });
            }
            if (id != author.AuthorId)
            {
                return BadRequest(new { message = "Author ID is incorrect" });
            }
            if(author.AuthorId != 0)
            {
                authorDetails.AuthorId = author.AuthorId;
            }
            if (author.Abbrv != null)
            {
                authorDetails.Abbrv = author.Abbrv;
            }
            if (author.FirstName != null)
            {
                authorDetails.FirstName = author.FirstName;
            }
            if (author.LastName != null)
            {
                authorDetails.LastName = author.LastName;
            }
            if (author.BookId != 0)
            {
                authorDetails.BookId = author.BookId;
            }
            _context.Entry(author).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                return Ok(new { message = "Author details updated Successfully." });
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AuthorExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }           
        }

        // POST: api/Authors
        
        [HttpPost]
        public async Task<ActionResult<Author>> PostAuthor([FromForm] AuthorsBDL author)
        {
            var authorlist = await _context.Authors.ToListAsync();
            Author authorDetails = new Author();
            if (author == null)
            {
                return NotFound();
            }
            foreach(var authors in authorlist)
            {
                if (authors.Abbrv == author.Abbrv)
                {
                    return Conflict("Author already exists");
                }
            }
            
            authorDetails.AuthorId = author.AuthorId;
            authorDetails.Abbrv = author.Abbrv;
            authorDetails.FirstName = author.FirstName;
            authorDetails.LastName = author.LastName;
            authorDetails.Dob = author.Dob;
            authorDetails.Dod = author.Dod;
            authorDetails.BookId = author.BookId;
            _context.Authors.Add(authorDetails);
            try
            {
                await _context.SaveChangesAsync();               
            }
            catch (DbUpdateException)
            {
                if (AuthorExists(author.AuthorId))
                {
                    return Conflict("Author already exists");
                }
                else
                {
                    throw;
                }
            }
            return CreatedAtAction("AuthorCreated", new { message = "Author details created with Author ID : " + authorDetails.AuthorId, id = authorDetails.AuthorId });           
        }
        public string AuthorCreated(int id)
        {
            return "Author created" + id;
        }
       
        private bool AuthorExists(int id)
        {
            return _context.Authors.Any(e => e.AuthorId == id);
        }
    }
}
