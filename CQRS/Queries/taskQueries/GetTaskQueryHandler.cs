using TaskAndTeamManagementSystem.CQRS.Queries.TaskQueries;
using TaskAndTeamManagementSystem.CQRS.Commands.TaskCommand;
using TaskAndTeamManagementSystem.DTOs;
using TaskAndTeamManagementSystem.Data;
using TaskAndTeamManagementSystem.Models;
using TaskAndTeamManagementSystem.Helpers;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;
using TaskStatus = TaskAndTeamManagementSystem.Models.TaskStatus;
namespace TaskAndTeamManagementSystem.CQRS.Queries.TaskQueries
{
    public class GetTasksQueryHandler
    {
        private readonly Context _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public GetTasksQueryHandler(Context context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<PagedTaskResult> Handle(GetTasksQuery query)
        {


            var userId = int.Parse(new GetUserIdHelper(_context, _httpContextAccessor).getId());
            var currentUser = await _context.Users.FindAsync(userId);


            var queryable = _context.UserTasks.AsQueryable();
            if (currentUser.Role == UserRole.Employee)
            {
                queryable = queryable.Where(u => u.AssignedToUserId == userId);
            }

            if (!string.IsNullOrEmpty(query.Search))
            {
                queryable = queryable.Where(u => u.Title.Contains(query.Search));
            }
            if (query.AssignedTo.HasValue)
            {
                queryable = queryable.Where(u => u.AssignedToUserId == query.AssignedTo.Value);
            }
            if (query.Team.HasValue)
            {
                queryable = queryable.Where(u => u.TeamId == query.Team.Value);
            }


            if (!string.IsNullOrEmpty(query.sortField) && !string.IsNullOrEmpty(query.sortOrder))
            {
                queryable = queryable.OrderBy($"{query.sortField} {query.sortOrder}");
            }
            var totalItems = await queryable.CountAsync();
            var tasks = await queryable
                        .Skip((query.Page - 1) * query.PageSize)
                        .Take(query.PageSize)
                        .Select(u => new GetTaskDto
                        {
                            Id = u.Id,
                            Title = u.Title,
                            Description = u.Description,
                            Status = u.Status,
                            DueDate = u.DueDate,
                            AssignedToUser = u.AssignedToUser,
                            CreatedByUser = u.CreatedByUser,
                            Team = u.Team
                        })
                        .ToListAsync();

            var result = new PagedTaskResult
            {
                TotalItems = totalItems,
                Page = query.Page,
                PageSize = query.PageSize,
                Tasks = tasks
            };

            return result;
        }
        
    }


}