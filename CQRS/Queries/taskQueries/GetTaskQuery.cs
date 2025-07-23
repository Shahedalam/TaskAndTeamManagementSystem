using Microsoft.EntityFrameworkCore;
using TaskAndTeamManagementSystem.CQRS.Commands.TaskCommand;
using TaskAndTeamManagementSystem.Models;
namespace TaskAndTeamManagementSystem.CQRS.Queries.TaskQueries
{
    public class GetTasksQuery
    {
        public string? Search { get; set; }
        public int? AssignedTo { get; set; }
        public int? Team { get; set; }
        public string? sortField { get; set; }
        public string? sortOrder { get; set; }
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 20;
    }
}