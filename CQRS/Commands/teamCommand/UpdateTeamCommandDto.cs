using TaskAndTeamManagementSystem.Models;

namespace TaskAndTeamManagementSystem.CQRS.Commands.TeamCommand
{
    public class UpdateTeamCommandDto
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public List<int> MemberIds { get; set; } = new();
    }
}
