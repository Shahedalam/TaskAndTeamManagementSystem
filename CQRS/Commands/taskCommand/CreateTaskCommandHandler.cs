using TaskAndTeamManagementSystem.CQRS.Commands.TaskCommand;
using Microsoft.AspNetCore.Identity;
using TaskAndTeamManagementSystem.Data;
using TaskAndTeamManagementSystem.Models;
using TaskAndTeamManagementSystem.CQRS.Queries.TaskQueries;
using Microsoft.EntityFrameworkCore;
using TaskAndTeamManagementSystem.DTOs;
using TaskAndTeamManagementSystem.Helpers;
using System.Security.Claims;

using TaskStatus = TaskAndTeamManagementSystem.Models.TaskStatus;

namespace TaskAndTeamManagementSystem.CQRS.Commands.TaskCommand
{
    public class CreateTaskCommandHandler
    {
        private readonly Context _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public CreateTaskCommandHandler(Context context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<GetTaskDto> Handle(CreateTaskCommandDto createTaskDto)
        {

            var userId = int.Parse( new GetUserIdHelper(_context, _httpContextAccessor).getId());
                 


            var assignedToUser = await _context.Users.FindAsync(createTaskDto.AssignedToUserId);
            var createdByUser = await _context.Users.FindAsync(userId);
            var team = await _context.Teams.FindAsync(createTaskDto.TeamId);

            if (assignedToUser == null || createdByUser == null || team == null)
            {
                throw new Exception("Invalid user or team ID");
            }

            var task = new UserTask
            {
                Title = createTaskDto.Title,
                Description = createTaskDto.Description,
                Status = (TaskStatus)createTaskDto.Status,
                AssignedToUserId = createTaskDto.AssignedToUserId,
                CreatedByUserId = userId,
                TeamId = createTaskDto.TeamId,
                DueDate = createTaskDto.DueDate,
                AssignedToUser = assignedToUser,
                CreatedByUser = createdByUser,
                Team = team
            };

            _context.UserTasks.Add(task);
            await _context.SaveChangesAsync();

            return new GetTaskDto
            {
                Id = task.Id,
                Title = task.Title,
                Description = task.Description,
                Status = task.Status,
                DueDate = task.DueDate,
                AssignedToUser = task.AssignedToUser,
                CreatedByUser = task.CreatedByUser,
                Team = task.Team
            };
        }

    }
}