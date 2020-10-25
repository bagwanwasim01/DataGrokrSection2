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
    public class BooksController : ControllerBase
    {
        private readonly LibraryContext _context;

        public BooksController(LibraryContext context)
        {
            _context = context;
        }

        // GET: api/Books
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BooksBDL>>> GetBooks()
        {
            return await _context.Books
                .Select(books => new BooksBDL()
                {
                    BookId = books.BookId,
                    Isbn = books.Isbn,
                    BookTitle = books.BookTitle,
                    TotalPages = books.TotalPages,
                    Abbrv = books.Abbrv,
                    AuthorId = books.AuthorId,
                })
                .ToListAsync();
        }

        // GET: api/Books/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BooksBDL>> GetBook(int id)
        {
            var book = await _context.Books.FindAsync(id);

            if (book == null)
            {
                return NotFound("Book details not found");
            }
            else
            {
                return await _context.Books
                     .Where(a => a.BookId == id)
                    .Select(books => new BooksBDL()
                    {
                        BookId = books.BookId,
                        Isbn = books.Isbn,
                        BookTitle = books.BookTitle,
                        TotalPages = books.TotalPages,
                        Abbrv = books.Abbrv,
                        AuthorId = books.AuthorId,
                    })
                    .FirstOrDefaultAsync();
            }
           
        }

        // PUT: api/Books/5        
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBook(int id, [FromForm] BooksBDL book)
        {
            var bookDetails = await _context.Books.FindAsync(id);
            var authorlist = await _context.Authors.ToListAsync();
            var booklist = await _context.Books.ToListAsync();
            if (bookDetails == null)
            {
                return BadRequest(new { message = "Book not found" });
            }
            if (id != book.BookId)
            {
                return BadRequest(new { message = "Book ID is incorrect" });
            }
            foreach (var books in booklist)
            {
                if (books.Abbrv == book.Abbrv)
                {
                    return Conflict("Book already exists");
                }
            }
            if (book.AuthorId != null)
            {
                bool author = authorlist.Any(aId => aId.AuthorId == book.AuthorId);
                if (author)
                {
                    bookDetails.AuthorId = book.AuthorId;
                }
                else
                {
                    return Conflict("Author not exists");
                }
            }
            if (book.Abbrv != null && book.BookTitle != null)            
                bookDetails.Abbrv = book.Abbrv;
                bookDetails.BookTitle = book.BookTitle;            
            if (book.Isbn != null)            
                bookDetails.Isbn = book.Isbn;            
            if (book.BookId != 0)            
                bookDetails.BookId = book.BookId;                
            if (book.TotalPages != 0)            
                bookDetails.TotalPages = book.TotalPages;
            



            _context.Entry(bookDetails).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                return Ok(new { message = "Book details updated Successfully." });
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }           
        }

        // POST: api/Books       
        [HttpPost]
        public async Task<ActionResult<Book>> PostBook([FromForm]BooksBDL book)
        {
            Book bookDetails = new Book();
            var authorlist = await _context.Authors.ToListAsync();
            var booklist = await _context.Books.ToListAsync();
            if (book == null)
            {
                return NotFound();
            }
            foreach (var books in booklist)
            {
                if (books.Abbrv == book.Abbrv)
                {
                    return Conflict("Book already exists");
                }
            }
            bool alreadyexist = authorlist.Any(aId => aId.AuthorId == book.AuthorId);
            if (alreadyexist)
            {
                bookDetails.AuthorId = book.AuthorId;
                bookDetails.Abbrv = book.Abbrv;
                bookDetails.BookTitle = book.BookTitle;
                bookDetails.TotalPages = book.TotalPages;
                bookDetails.BookId = book.BookId;
               
            }
            else
            {
                return Conflict("Author not exists");
            }        
            _context.Books.Add(bookDetails);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (BookExists(book.BookId))
                {
                    return Conflict("Book already exists");
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("BookCreated", new { message = "Book details created with Book ID : " + bookDetails.BookId, id = bookDetails.BookId });
        }
        public string BookCreated(int id)
        {
            return "Author created" + id;
        }
       
        private bool BookExists(int id)
        {
            return _context.Books.Any(e => e.BookId == id);
        }
    }
}
