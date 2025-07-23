namespace TaskAndTeamManagementSystem.Helpers;
using AutoMapper;
using TaskAndTeamManagementSystem.Models;
using TaskAndTeamManagementSystem.DTOs;

public class TaskLessDataShow : Profile
{
	public TaskLessDataShow()
	{
		CreateMap<User, GetMinimalUserInfoDto>();
		CreateMap<Team, GetMinimalTeamInfoDto>();
		// Add more mappings as needed
	}
}
