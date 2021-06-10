using BookStore.BLL.DTO;
using BookStore.BLL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.API.Controllers
{
    /// <summary>
    /// Borrow Configurations
    /// </summary>
    [Authorize(Policy = "AllUsers")]
    [Route("api/[controller]")]
    [ApiController]
    public class BorrowController : ControllerBase
    {
        private readonly IBorrowingService _borrowService;
        public BorrowController(IBorrowingService borrowService)
        {
            _borrowService = borrowService;
        }

        /// <summary>
        /// Get borrowings with the given book and member or all borrowings
        /// </summary>
        /// <returns>Returns a list of borrowings</returns>
        /// <response code="200">Listing successful</response>
        /// <response code="401">Unauthorized access</response>
        /// <response code="409">Concurrency occurred</response>
        /// <response code="400">Bad Request</response>
        /// <response code="500">Server side error</response>
        /// <response code="403">Access denied</response>
        [HttpGet()]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Borrowing>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<IEnumerable<Borrowing>>> Get([FromQuery(Name = "book")] string bookTitle,
                                                                    [FromQuery(Name = "member")] string memberName)
        {
            if(memberName != null || bookTitle != null)
                return (await _borrowService.GetBorrowsByMemberNameOrBookTitleAsync(memberName, bookTitle)).ToList();
            return (await _borrowService.GetBorrowsAsync()).ToList();
        }

        /// <summary>
        /// Get all active borrowings
        /// </summary>
        /// <returns>Returns a list of borrowings</returns>
        /// <response code="200">Listing successful</response>
        /// <response code="401">Unauthorized access</response>
        /// <response code="409">Concurrency occurred</response>
        /// <response code="400">Bad Request</response>
        /// <response code="500">Server side error</response>
        /// <response code="403">Access denied</response>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Borrowing>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [HttpGet("active")]
        public async Task<ActionResult<IEnumerable<Borrowing>>> Get()
        {
            return (await _borrowService.GetBorrowsActiveAsync()).ToList();
        }

        /// <summary>
        /// Get a specific borrowing with the given identifier
        /// </summary>
        /// <param name="id">Borrowing's identifier</param>
        /// <returns>Returns a specific borrowing with the given identifier</returns>
        /// <response code="200">Listing successful</response>
        /// <response code="401">Unauthorized access</response>
        /// <response code="409">Concurrency occurred</response>
        /// <response code="400">Bad Request</response>
        /// <response code="500">Server side error</response>
        /// <response code="403">Access denied</response>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Borrowing))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [HttpGet("{id}")]
        public async Task<ActionResult<Borrowing>> Get(int id)
        {
            return await _borrowService.GetBorrowAsync(id);
        }

        /// <summary>
        /// Create a new borrowing with the given data
        /// </summary>
        /// <param name="borrowing">The Borrowing to create</param>
        /// <returns>Returns the borrowing inserted</returns>
        /// <response code="201">Insert successful</response>
        /// <response code="401">Unauthorized access</response>
        /// <response code="409">Concurrency occurred</response>
        /// <response code="400">Bad Request</response>
        /// <response code="500">Server side error</response>
        /// <response code="403">Access denied</response>
        [Authorize(Policy = "Admin")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Borrowing))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Borrowing>> Post([FromBody] Borrowing borrowing)
        {
            var created = await _borrowService.InsertBorrowAsync(borrowing);
            return CreatedAtAction(
                    nameof(Get),
                    new { id = created.Id },
                    created);
        }

        /// <summary>
        /// Update a borrowings return date
        /// </summary>
        /// <param name="id">ID of the updated borrowing</param>
        /// <returns>Returns no content</returns>
        /// <response code="204">Update returnDate successful</response>
        /// <response code="401">Unauthorized access</response>
        /// <response code="409">Concurrency occurred</response>
        /// <response code="400">Bad Request</response>
        /// <response code="500">Server side error</response>
        /// <response code="403">Access denied</response>
        [Authorize(Policy = "Admin")]
        [HttpPut("{id}/return")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Return(int id)
        {
            await _borrowService.InsertBorrowReturnDateAsync(id);
            return NoContent();
        }

        /// <summary>
        /// Update a borrowing with the given data
        /// </summary>
        /// <param name="id">ID of the updated borrowing</param>
        /// <param name="borrowing">The borrowing to update</param>
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
        public async Task<IActionResult> Put(int id, [FromBody] Borrowing borrowing)
        {
            await _borrowService.UpdateBorrowAsync(id, borrowing);
            return NoContent();
        }

        /// <summary>
        /// Delete a borrowing with the given ID
        /// </summary>
        /// <param name="id">The ID of the borrowing to delete</param>
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
            await _borrowService.DeleteBorrowAsync(id);
            return NoContent();
        }
    }
}
