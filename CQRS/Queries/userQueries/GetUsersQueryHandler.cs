using TaskAndTeamManagementSystem.CQRS.Queries.userQueries;
using TaskAndTeamManagementSystem.DTOs;
using TaskAndTeamManagementSystem.Data;
using TaskAndTeamManagementSystem.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;

namespace TaskAndTeamManagementSystem.CQRS.Queries.userQueries
{
    public class GetUsersQueryHandler
    {
        private readonly Context _context;
        public GetUsersQueryHandler(Context context)
        {
            _context = context;
        }

        public async Task<List<GetUserDto>> Handle(GetUsersQuery query)
        {
            var queryable = _context.Users.AsQueryable();

            if (!string.IsNullOrEmpty(query.Search))
            {
                queryable = queryable.Where(u => u.FullName.Contains(query.Search) || u.Email.Contains(query.Search));
            }
                
            if (query.Role.HasValue)
            {
                queryable = queryable.Where(u => u.Role == (UserRole) query.Role.Value);
            }

            if (!string.IsNullOrEmpty(query.sortField) && !string.IsNullOrEmpty(query.sortOrder))
            {
                queryable = queryable.OrderBy($"{query.sortField} {query.sortOrder}");
            }
            var users = await queryable
                        .Skip((query.Page - 1) * query.PageSize)
                        .Take(query.PageSize)
                        .Select(u => new GetUserDto
                        {
                            Id = u.Id,
                            FullName = u.FullName,
                            Email = u.Email,
                            Role = u.Role,
                            Teams = u.Teams,
                            AssignedTasks = u.AssignedTasks
                        })
                        .ToListAsync();

            return users;
        }
        
    }


}