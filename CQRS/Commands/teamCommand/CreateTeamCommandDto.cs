using TaskAndTeamManagementSystem.Models;

namespace TaskAndTeamManagementSystem.CQRS.Commands.TeamCommand
{
    public class CreateTeamCommandDto
    {
        public required string Name { get; set; }
        public required string Description { get; set; }
        public List<int> MemberIds { get; set; } = new();
    }
}
