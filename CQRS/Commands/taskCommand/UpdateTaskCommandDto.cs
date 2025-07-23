using TaskAndTeamManagementSystem.Models;
using TaskStatus = TaskAndTeamManagementSystem.Models.TaskStatus;

namespace TaskAndTeamManagementSystem.CQRS.Commands.TaskCommand
{
    public class UpdateTaskCommandDto
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public TaskStatus? Status { get; set; }
        public int? AssignedToUserId { get; set; }
        public int? TeamId { get; set; }
        public DateTime? DueDate { get; set; }
    }

}
