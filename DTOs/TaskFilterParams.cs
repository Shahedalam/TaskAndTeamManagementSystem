namespace TaskAndTeamManagementSystem.DTOs
{
    public class TaskFilterParams
    {
        public string? Status { get; set; }
        public int? AssignedToId { get; set; }
        public int? TeamId { get; set; }
        public DateTime? DueDate { get; set; }

        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}