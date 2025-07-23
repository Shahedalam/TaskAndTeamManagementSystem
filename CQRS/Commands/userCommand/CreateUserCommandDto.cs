using TaskAndTeamManagementSystem.Models;

namespace TaskAndTeamManagementSystem.CQRS.Commands.userCommand
{
    public class CreateUserCommandDto
    {
        public required string FullName { get; set; }
        public required string Email { get; set; }
        public required UserRole Role { get; set; }
        public required string Password { get; set; }
    }
}
