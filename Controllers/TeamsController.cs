using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using Microsoft.EntityFrameworkCore;
using TaskAndTeamManagementSystem.Data;
using TaskAndTeamManagementSystem.Models;
using TaskAndTeamManagementSystem.DTOs;
using TaskAndTeamManagementSystem.ExceptionHandler;
using Microsoft.AspNetCore.Identity;
using TaskAndTeamManagementSystem.CQRS.Commands.TeamCommand;
using TaskAndTeamManagementSystem.CQRS.Queries.TeamQueries;

namespace TaskAndTeamManagementSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TeamsController : ControllerBase
    {
       
        private readonly Context _context;
        private readonly CreateTeamCommandHandler _createHandler;
        private readonly UpdateTeamCommandHandler _updateHandler;
        private readonly DeleteTeamCommandHandler _deleteHandler;
        private readonly GetTeamsQueryHandler _queryHandler;

        public TeamsController(Context context)
        {
            _context = context;
            _createHandler = new CreateTeamCommandHandler(context);
            _updateHandler = new UpdateTeamCommandHandler(context);
            _deleteHandler = new DeleteTeamCommandHandler(context);
            _queryHandler = new GetTeamsQueryHandler(context);
        }

        [HttpGet]
        [Authorize(Roles = "Admin,Manager")]
        public async Task<IActionResult> GetTeams([FromQuery] GetTeamsQuery query)
        {
            var users = await _queryHandler.Handle(query);

            return Ok(users);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateTeam([FromBody] CreateTeamCommandDto createTeamDto)
        {
            var team = await _createHandler.Handle(createTeamDto);
            return Ok(team);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateTeam(int id, [FromBody] UpdateTeamCommandDto updateTeamDto)
        {
            var team = await _updateHandler.Handle(id, updateTeamDto);
            return Ok(team);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteTeam(int id)
        {
            await _deleteHandler.Handle(id);
            return Ok("Team Deleted successfully");
        }
    }
}