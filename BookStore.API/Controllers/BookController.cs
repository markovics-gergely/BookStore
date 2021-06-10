using BookStore.BLL.DTO;
using BookStore.BLL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.API.Controllers
{
    /// <summary>
    /// Book Configurations
    /// </summary>
    [Authorize(Policy = "AllUsers")]
    [Authorize(Policy = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookService _bookService;
        public BookController(IBookService bookService)
        {
            _bookService = bookService;
        }

        /// <summary>
        /// Get books with the given title or all books
        /// </summary>
        /// <param name="book">Book's title</param>
        /// <returns>Returns a list of books with the given title or all book</returns>
        /// <response code="200">Listing successful</response>
        /// <response code="401">Unauthorized access</response>
        /// <response code="409">Concurrency occurred</response>
        /// <response code="400">Bad Request</response>
        /// <response code="500">Server side error</response>
        /// <response code="403">Access denied</response>
        [HttpGet()]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Book>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [AllowAnonymous]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<Book>>> Get([FromQuery(Name = "book")] string book)
        {
            Debug.WriteLine("asdasd");
            if (book == null) return (await _bookService.GetBooksAsync()).ToList();
            return (await _bookService.GetBooksByTitleAsync(book)).ToList();
        }

        /// <summary>
        /// Get a specific book with the given identifier
        /// </summary>
        /// <param name="id">Book's identifier</param>
        /// <returns>Returns a specific book with the given identifier</returns>
        /// <response code="200">Listing successful</response>
        /// <response code="401">Unauthorized access</response>
        /// <response code="409">Concurrency occurred</response>
        /// <response code="400">Bad Request</response>
        /// <response code="500">Server side error</response>
        /// <response code="403">Access denied</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Book))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<Book>> Get(int id)
        {
            return await _bookService.GetBookAsync(id);
        }

        /// <summary>
        /// Create a new book with the given data
        /// </summary>
        /// <param name="book">The book to create</param>
        /// <returns>Returns the book inserted</returns>
        /// <response code="201">Insert successful</response>
        /// <response code="401">Unauthorized access</response>
        /// <response code="409">Concurrency occurred</response>
        /// <response code="400">Bad Request</response>
        /// <response code="500">Server side error</response>
        /// <response code="403">Access denied</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Author))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Book>> Post([FromBody] Book book)
        {
            var created = await _bookService.InsertBookAsync(book);
            return CreatedAtAction(
                    nameof(Get),
                    new { id = created.Id },
                    created);
        }

        /// <summary>
        /// Update a book with the given data
        /// </summary>
        /// <param name="id">ID of the updated book</param>
        /// <param name="book">The book to update</param>
        /// <returns>Returns no content</returns>
        /// <response code="204">Update successful</response>
        /// <response code="401">Unauthorized access</response>
        /// <response code="409">Concurrency occurred</response>
        /// <response code="400">Bad Request</response>
        /// <response code="500">Server side error</response>
        /// <response code="403">Access denied</response>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Put(int id, [FromBody] Book book)
        {
            await _bookService.UpdateBookAsync(id, book);
            return NoContent();
        }

        /// <summary>
        /// Delete a book with the given ID
        /// </summary>
        /// <param name="id">The ID of the book to delete</param>
        /// <returns>Returns no content</returns>
        /// <response code="204">Delete successful</response>
        /// <response code="401">Unauthorized access</response>
        /// <response code="409">Concurrency occurred</response>
        /// <response code="400">Bad Request</response>
        /// <response code="500">Server side error</response>
        /// <response code="403">Access denied</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> Delete(int id)
        {
            await _bookService.DeleteBookAsync(id);
            return NoContent();
        }
    }
}
