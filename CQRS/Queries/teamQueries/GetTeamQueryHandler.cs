using TaskAndTeamManagementSystem.CQRS.Queries.TeamQueries;
using TaskAndTeamManagementSystem.DTOs;
using TaskAndTeamManagementSystem.Data;
using TaskAndTeamManagementSystem.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;

namespace TaskAndTeamManagementSystem.CQRS.Queries.TeamQueries
{
    public class GetTeamsQueryHandler
    {
        private readonly Context _context;
        public GetTeamsQueryHandler(Context context)
        {
            _context = context;
        }

        public async Task<List<GetTeamDto>> Handle(GetTeamsQuery query)
        {
            var queryable = _context.Teams.AsQueryable();

            if (!string.IsNullOrEmpty(query.Search))
            {
                queryable = queryable.Where(u => u.Name.Contains(query.Search));
            }
               

            if (!string.IsNullOrEmpty(query.sortField) && !string.IsNullOrEmpty(query.sortOrder))
            {
                queryable = queryable.OrderBy($"{query.sortField} {query.sortOrder}");
            }
            var teams = await queryable
                        .Skip((query.Page - 1) * query.PageSize)
                        .Take(query.PageSize)
                        .Select(u => new GetTeamDto
                        {
                            Id = u.Id,
                            Name = u.Name,
                            Description = u.Description,
                            Members = u.Members,
                            Tasks = u.Tasks
                        })
                        .ToListAsync();

            return teams;
        }
        
    }


}