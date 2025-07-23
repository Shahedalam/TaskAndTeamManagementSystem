namespace TaskAndTeamManagementSystem.Helpers;

using TaskAndTeamManagementSystem.DTOs;
using Microsoft.EntityFrameworkCore;

public static class PaginationHelper
{
    public static async Task<object> Paginate<T>(IQueryable<T> query, PaginationDto pagination)
    {
        var totalItems = await query.CountAsync();
        var items = await query.Skip((pagination.Page - 1) * pagination.PageSize).Take(pagination.PageSize).ToListAsync();
        return new { totalItems, pagination.Page, pagination.PageSize, items };
    }
}
