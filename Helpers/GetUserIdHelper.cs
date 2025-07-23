namespace TaskAndTeamManagementSystem.Helpers;

using TaskAndTeamManagementSystem.DTOs;
using Microsoft.EntityFrameworkCore;
using TaskAndTeamManagementSystem.Data;
using System.Security.Claims;
public class GetUserIdHelper
{
    private readonly Context _context;
    private readonly IHttpContextAccessor _httpContextAccessor;
    public GetUserIdHelper(Context context, IHttpContextAccessor httpContextAccessor)
    {
        _context = context;
        _httpContextAccessor = httpContextAccessor;
    }
    public string getId(){
        var httpContext = _httpContextAccessor.HttpContext;
        if (httpContext == null)
        {
            throw new Exception("HTTP context is not available.");
        }

        var user = httpContext.User;
        if (user == null || !user.Identity.IsAuthenticated)
        {
            throw new Exception("User is not authenticated.");
        }

        var userClaim = user.FindFirst(ClaimTypes.NameIdentifier);
        if (userClaim == null)
        {
            throw new Exception("User ID claim not found.");
        }

        if (!int.TryParse(userClaim.Value, out int userId))
        {
            throw new Exception("Invalid user ID format.");
        }

        return userClaim.Value;
    }
}
