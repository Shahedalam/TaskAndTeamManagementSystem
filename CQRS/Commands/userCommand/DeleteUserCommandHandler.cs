using TaskAndTeamManagementSystem.CQRS.Commands.userCommand;
using Microsoft.AspNetCore.Identity;
using TaskAndTeamManagementSystem.Data;
using TaskAndTeamManagementSystem.Models;
using TaskAndTeamManagementSystem.CQRS.Queries.userQueries;
using Microsoft.EntityFrameworkCore;
using TaskAndTeamManagementSystem.DTOs;

namespace TaskAndTeamManagementSystem.CQRS.Commands.userCommand
{
    public class DeleteUserCommandHandler
    {
        private readonly Context _context;
        public DeleteUserCommandHandler(Context context)
        {
            _context = context;
        }

        public async Task Handle(int id)
        {
          
            var existing = await _context.Users.FindAsync(id);
            if (existing == null)
            {
                throw new Exception("User not exist");
            }
            _context.Users.Remove(existing);
            await _context.SaveChangesAsync();
            
        }
    }
}