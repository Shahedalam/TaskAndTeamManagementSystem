namespace TaskAndTeamManagementSystem.DTOs;
public class TaskFilterDto
{
    public string? Status { get; set; }
    public int? AssignedTo { get; set; }
    public int? TeamId { get; set; }
    public DateTime? DueDate { get; set; }
}
