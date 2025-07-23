namespace TaskAndTeamManagementSystem.Helpers;

using Microsoft.EntityFrameworkCore;
using TaskAndTeamManagementSystem.Models;
using TaskAndTeamManagementSystem.DTOs;
public static class TaskFilter
{
    public static IQueryable<UserTask> Apply(IQueryable<UserTask> query, TaskFilterDto filter)
    {
        
        if (!string.IsNullOrEmpty(filter.Status) && Enum.TryParse<TaskStatus>(filter.Status, true, out var statusEnum))
        {
            query = query.Where(t => t.Status == statusEnum);
        }
        if (filter.AssignedTo.HasValue)
            query = query.Where(t => t.AssignedToUserId == filter.AssignedTo);
        if (filter.TeamId.HasValue)
            query = query.Where(t => t.TeamId == filter.TeamId);
        if (filter.DueDate.HasValue)
            query = query.Where(t => t.DueDate.Date == filter.DueDate.Value.Date);
        return query;
    }
}