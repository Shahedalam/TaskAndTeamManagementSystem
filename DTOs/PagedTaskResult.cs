using TaskAndTeamManagementSystem.DTOs;
namespace TaskAndTeamManagementSystem.DTOs
{
    public class PagedTaskResult
    {
        public int TotalItems { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
        public List<GetTaskDto> Tasks { get; set; } = new();
    }
}