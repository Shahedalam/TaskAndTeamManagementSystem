using Microsoft.AspNetCore.Mvc;
using TaskAndTeamManagementSystem.Data;
using TaskAndTeamManagementSystem.Models;
using TaskAndTeamManagementSystem.DTOs;
using TaskAndTeamManagementSystem.ExceptionHandler;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using TaskAndTeamManagementSystem.CQRS.Commands.userCommand;
using TaskAndTeamManagementSystem.CQRS.Queries.userQueries;


namespace TaskAndTeamManagementSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
	public class UsersController : ControllerBase
	{
		private readonly Context _context;
        private readonly CreateUserCommandHandler _createHandler;
        private readonly UpdateUserCommandHandler _updateHandler;
        private readonly DeleteUserCommandHandler _deleteHandler;
        private readonly GetUsersQueryHandler _queryHandler;

        public UsersController(Context context)
		{
			_context = context;
            _createHandler = new CreateUserCommandHandler(context);
            _updateHandler = new UpdateUserCommandHandler(context);
            _deleteHandler = new DeleteUserCommandHandler(context);
            _queryHandler = new GetUsersQueryHandler(context);
        }

		[HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetUsers([FromQuery] GetUsersQuery query)
		{
            var users = await _queryHandler.Handle(query);

            return Ok(users);
		}

		[HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserCommandDto createUserDto)
		{
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                throw new ValidationException(errors);
            }
            var users = await _createHandler.Handle(createUserDto);
            return Ok(users);
		}

		[HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] UpdateUserCommandDto updateUserDto)
		{
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                throw new ValidationException(errors);
            }
            var users = await _updateHandler.Handle(id,updateUserDto);
            return Ok(users);
		}

		[HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteUser(int id)
		{
			await _deleteHandler.Handle(id);
            return Ok("User Deleted successfully");
        }
    }
}
