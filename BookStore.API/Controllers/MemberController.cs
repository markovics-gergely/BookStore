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
    /// Member Configurations
    /// </summary>
    [Authorize(Policy = "AllUsers")]
    [Route("api/[controller]")]
    [ApiController]
    public class MemberController : ControllerBase
    {
        private readonly IMemberService _memberService;
        public MemberController(IMemberService memberService)
        {
            _memberService = memberService;
        }

        /// <summary>
        /// Get members with given name or all members
        /// </summary>
        /// <returns>Returns a list of members</returns>
        /// <response code="200">Listing successful</response>
        /// <response code="401">Unauthorized access</response>
        /// <response code="409">Concurrency occurred</response>
        /// <response code="400">Bad Request</response>
        /// <response code="500">Server side error</response>
        /// <response code="403">Access denied</response>
        [HttpGet()]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Member>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<IEnumerable<Member>>> Get([FromQuery(Name = "name")] string memberName)
        {
            if(memberName == null) return (await _memberService.GetMembersAsync()).ToList();
            return (await _memberService.GetMembersByNameAsync(memberName)).ToList();
        }

        /// <summary>
        /// Get a specific member with the given identifier
        /// </summary>
        /// <param name="id">Member's identifier</param>
        /// <returns>Returns a specific member with the given identifier</returns>
        /// <response code="200">Listing successful</response>
        /// <response code="401">Unauthorized access</response>
        /// <response code="409">Concurrency occurred</response>
        /// <response code="400">Bad Request</response>
        /// <response code="500">Server side error</response>
        /// <response code="403">Access denied</response>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Member))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [HttpGet("{id}")]
        public async Task<ActionResult<Member>> Get(int id)
        {
            return await _memberService.GetMemberAsync(id);
        }

        /// <summary>
        /// Create a new member with the given data
        /// </summary>
        /// <param name="member">The member to create</param>
        /// <returns>Returns the member inserted</returns>
        /// <response code="201">Insert successful</response>
        /// <response code="401">Unauthorized access</response>
        /// <response code="409">Concurrency occurred</response>
        /// <response code="400">Bad Request</response>
        /// <response code="500">Server side error</response>
        /// <response code="403">Access denied</response>
        [Authorize(Policy = "Admin")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Member))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Member>> Post([FromBody] Member member)
        {
            var created = await _memberService.InsertMemberAsync(member);
            return CreatedAtAction(
                    nameof(Get),
                    new { id = created.Id },
                    created);
        }

        /// <summary>
        /// Update a member with the given data
        /// </summary>
        /// <param name="id">ID of the updated member</param>
        /// <param name="member">The member to update</param>
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
        public async Task<IActionResult> Put(int id, [FromBody] Member member)
        {
            await _memberService.UpdateMemberAsync(id, member);
            return NoContent();
        }

        /// <summary>
        /// Delete a member with the given ID
        /// </summary>
        /// <param name="id">The ID of the member to delete</param>
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
            await _memberService.DeleteMemberAsync(id);
            return NoContent();
        }
    }
}
