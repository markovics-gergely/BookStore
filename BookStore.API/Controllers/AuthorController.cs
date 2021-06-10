using BookStore.BLL.DTO;
using BookStore.BLL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using BookStore.API.Examples;
using Swashbuckle.AspNetCore.Filters;
using NSwag.Annotations;
using Microsoft.OpenApi.Models;

namespace BookStore.API.Controllers
{
    /// <summary>
    /// Author Configurations
    /// </summary>
    [Authorize(Policy = "AllUsers")]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private readonly IAuthorService _authorService;
        public AuthorController(IAuthorService authorService)
        {
            _authorService = authorService;
        }

        /// <summary>
        /// Get authors with the given name or all author
        /// </summary>
        /// <param name="author">Author's name</param>
        /// <returns>Returns a list of authors with the given name or all author</returns>
        /// <response code="200">Listing successful</response>
        /// <response code="401">Unauthorized access</response>
        /// <response code="409">Concurrency occurred</response>
        /// <response code="400">Bad Request</response>
        /// <response code="500">Server side error</response>
        /// <response code="403">Access denied</response>
        [HttpGet()]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Author>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<IEnumerable<Author>>> Get([FromQuery(Name = "author")] string author)
        {
            if(author == null) return (await _authorService.GetAuthorsAsync()).ToList();
            return (await _authorService.GetAuthorsByNameAsync(author)).ToList();
        }

        /// <summary>
        /// Get a specific author with the given identifier
        /// </summary>
        /// <param name="id">Author's identifier</param>
        /// <returns>Returns a specific author with the given identifier</returns>
        /// <response code="200">Listing successful</response>
        /// <response code="401">Unauthorized access</response>
        /// <response code="409">Concurrency occurred</response>
        /// <response code="400">Bad Request</response>
        /// <response code="500">Server side error</response>
        /// <response code="403">Access denied</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Author))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<Author>> Get(int id)
        {
            return await _authorService.GetAuthorAsync(id);
        }

        /// <summary>
        /// Create a new author with the given data
        /// </summary>
        /// <param name="author">The Author to create</param>
        /// <returns>Returns the author inserted</returns>
        /// <response code="201">Insert successful</response>
        /// <response code="401">Unauthorized access</response>
        /// <response code="409">Concurrency occurred</response>
        /// <response code="400">Bad Request</response>
        /// <response code="500">Server side error</response>
        /// <response code="403">Access denied</response>
        [Authorize(Policy = "Admin")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Author))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Author>> Post([FromBody] Author author)
        {
            var created = await _authorService.InsertAuthorAsync(author);
            return CreatedAtAction(
                    nameof(Get),
                    new { id = created.Id },
                    created);
        }

        /// <summary>
        /// Update an author with the given data
        /// </summary>
        /// <param name="id">ID of the updated book</param>
        /// <param name="author">The Author to update</param>
        /// <returns>Returns no content</returns>
        /// <response code="204">Update successful</response>
        /// <response code="401">Unauthorized access</response>
        /// <response code="409">Concurrency occurred</response>
        /// <response code="400">Bad Request</response>
        /// <response code="500">Server side error</response>
        /// <response code="403">Access denied</response>
        [Authorize(Policy = "Admin")]
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Put(int id, [FromBody] Author author)
        {
            await _authorService.UpdateAuthorAsync(id, author);
            return NoContent();
        }

        /// <summary>
        /// Delete an author with the given ID
        /// </summary>
        /// <param name="id">The ID of the author to delete</param>
        /// <returns>Returns no content</returns>
        /// <response code="204">Delete successful</response>
        /// <response code="401">Unauthorized access</response>
        /// <response code="409">Concurrency occurred</response>
        /// <response code="400">Bad Request</response>
        /// <response code="500">Server side error</response>
        /// <response code="403">Access denied</response>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [Authorize(Policy = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _authorService.DeleteAuthorAsync(id);
            return NoContent();
        }
    }
}
