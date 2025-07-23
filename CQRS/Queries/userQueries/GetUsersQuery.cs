using Microsoft.EntityFrameworkCore;
using TaskAndTeamManagementSystem.CQRS.Commands.userCommand;
using TaskAndTeamManagementSystem.Models;
namespace TaskAndTeamManagementSystem.CQRS.Queries.userQueries
{
    public class GetUsersQuery
    {
        public UserRole? Role { get; set; } = null;
        public string? Search { get; set; }
        public string? sortField { get; set; }
        public string? sortOrder { get; set; }
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 20;
    }
}