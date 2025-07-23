using TaskAndTeamManagementSystem.Models;
namespace TaskAndTeamManagementSystem.DTOs
{
    public class GetMinimalTeamInfoDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
    }
}
