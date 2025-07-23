using TaskAndTeamManagementSystem.CQRS.Commands.TaskCommand;
using Microsoft.AspNetCore.Identity;
using TaskAndTeamManagementSystem.Data;
using TaskAndTeamManagementSystem.Models;
using TaskAndTeamManagementSystem.CQRS.Queries.TaskQueries;
using Microsoft.EntityFrameworkCore;
using TaskAndTeamManagementSystem.DTOs;
using TaskStatus = TaskAndTeamManagementSystem.Models.TaskStatus;
using TaskAndTeamManagementSystem.Helpers;
namespace TaskAndTeamManagementSystem.CQRS.Commands.TaskCommand
{
    public class UpdateTaskCommandHandler
    {
        private readonly Context _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public UpdateTaskCommandHandler(Context context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<GetTaskDto> Handle(int id, UpdateTaskCommandDto taskDto)
        {

            var userId = int.Parse(new GetUserIdHelper(_context, _httpContextAccessor).getId());
            var currentUser = await _context.Users.FindAsync(userId);

            var existing = await _context.UserTasks
                .FirstOrDefaultAsync(t => t.Id == id);

            if (existing == null)
                throw new Exception("Task not exist");
            if (currentUser.Role == UserRole.Manager)
            {
                existing.Title = taskDto.Title;
                existing.Description = taskDto.Description;

                if (taskDto.DueDate.HasValue)
                    existing.DueDate = taskDto.DueDate.Value;

                // Assuming AssignedToUserId and TeamId are nullable in DTO to allow optional update
                if (taskDto.AssignedToUserId.HasValue)
                {
                    var assignedToUser = await _context.Users.FindAsync(taskDto.AssignedToUserId.Value);
                    if (assignedToUser == null)
                        throw new Exception("Assigned user not found");

                    existing.AssignedToUserId = taskDto.AssignedToUserId.Value;
                    existing.AssignedToUser = assignedToUser;
                }

                if (taskDto.TeamId.HasValue)
                {
                    var team = await _context.Teams.FindAsync(taskDto.TeamId.Value);
                    if (team == null)
                        throw new Exception("Team not found");

                    existing.TeamId = taskDto.TeamId.Value;
                    existing.Team = team;
                }
            }
            if (taskDto.Status.HasValue)
                existing.Status = (TaskStatus)taskDto.Status.Value;

            // Optional: Update CreatedByUserId if needed, or disallow

            await _context.SaveChangesAsync();

            return new GetTaskDto
            {
                Id = existing.Id,
                Title = existing.Title,
                Description = existing.Description,
                Status = existing.Status,
                DueDate = existing.DueDate,
                AssignedToUser = existing.AssignedToUser,
                CreatedByUser = existing.CreatedByUser,
                Team = existing.Team
            };
        }
    }
}