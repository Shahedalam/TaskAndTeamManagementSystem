using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using TaskAndTeamManagementSystem.Data;
using TaskAndTeamManagementSystem.DTOs;
using TaskAndTeamManagementSystem.Models;
using TaskAndTeamManagementSystem.Helpers;
using TaskStatus = TaskAndTeamManagementSystem.Models.TaskStatus;
using TaskAndTeamManagementSystem.CQRS.Commands.TaskCommand;
using TaskAndTeamManagementSystem.CQRS.Queries.TaskQueries;
using TaskAndTeamManagementSystem.ExceptionHandler;

namespace TaskAndTeamManagementSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
   
    public class TasksController : ControllerBase
    {
        private readonly Context _context;
        private readonly CreateTaskCommandHandler _createHandler;
        private readonly UpdateTaskCommandHandler _updateHandler;
        private readonly DeleteTaskCommandHandler _deleteHandler;
        private readonly GetTasksQueryHandler _queryHandler;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public TasksController(Context context, IHttpContextAccessor httpContextAccessor) {
            _context = context;
            _createHandler = new CreateTaskCommandHandler(context, httpContextAccessor);
            _updateHandler = new UpdateTaskCommandHandler(context, httpContextAccessor);
            _deleteHandler = new DeleteTaskCommandHandler(context);
            _queryHandler = new GetTasksQueryHandler(context, httpContextAccessor);
        }

        [HttpGet]
        [Authorize(Roles = "Manager,Employee")]
        public async Task<IActionResult> GetTasks([FromQuery] GetTasksQuery query)
        {
            var tasks = await _queryHandler.Handle(query);

            return Ok(tasks);
        }

        [HttpPost]
        [Authorize(Roles = "Manager,Employee")]
        public async Task<IActionResult> CreateTask([FromBody] CreateTaskCommandDto createTaskDto)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                throw new ValidationException(errors);
            }
         
            var tasks = await _createHandler.Handle(createTaskDto);
            return Ok(tasks);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Manager,Employee")]
        public async Task<IActionResult> UpdateTask(int id, [FromBody] UpdateTaskCommandDto updateTaskCommandDto)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                throw new ValidationException(errors);
            }
            var tasks = await _updateHandler.Handle(id, updateTaskCommandDto);
            return Ok(tasks);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> DeleteTask(int id)
        {
            await _deleteHandler.Handle(id);
            return Ok("Task Deleted successfully");
        }
    }
}