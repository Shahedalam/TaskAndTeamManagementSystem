using Microsoft.EntityFrameworkCore;
using TaskAndTeamManagementSystem.CQRS.Commands.TeamCommand;
using TaskAndTeamManagementSystem.Models;
namespace TaskAndTeamManagementSystem.CQRS.Queries.TeamQueries
{
    public class GetTeamsQuery
    {
        public string? Search { get; set; }
        public string? sortField { get; set; }
        public string? sortOrder { get; set; }
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 20;
    }
}