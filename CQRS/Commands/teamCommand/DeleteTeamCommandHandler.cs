using TaskAndTeamManagementSystem.CQRS.Commands.TeamCommand;
using Microsoft.AspNetCore.Identity;
using TaskAndTeamManagementSystem.Data;
using TaskAndTeamManagementSystem.Models;
using TaskAndTeamManagementSystem.CQRS.Queries.TeamQueries;
using Microsoft.EntityFrameworkCore;
using TaskAndTeamManagementSystem.DTOs;

namespace TaskAndTeamManagementSystem.CQRS.Commands.TeamCommand
{
    public class DeleteTeamCommandHandler
    {
        private readonly Context _context;
        public DeleteTeamCommandHandler(Context context)
        {
            _context = context;
        }

        public async Task Handle(int id)
        {
          
            var existing = await _context.Teams.FindAsync(id);
            if (existing == null)
            {
                throw new Exception("Team not exist");
            }
            _context.Teams.Remove(existing);
            await _context.SaveChangesAsync();
            
        }
    }
}