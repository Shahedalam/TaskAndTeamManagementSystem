using TaskAndTeamManagementSystem.Models;
using TaskAndTeamManagementSystem.DTOs;
using TaskStatus = TaskAndTeamManagementSystem.Models.TaskStatus;
namespace TaskAndTeamManagementSystem.DTOs
{
    public class GetTaskDto
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public TaskStatus? Status { get; set; }
        public DateTime DueDate { get; set; }
        public User AssignedToUser { get; set; }
        public User CreatedByUser { get; set; }
        public Team Team { get; set; }

        //public GetMinimalUserInfoDto CreatedByUser { get; set; }
        //public GetMinimalTeamInfoDto Team { get; set; }
    }
}
