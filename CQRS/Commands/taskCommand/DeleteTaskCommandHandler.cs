using TaskAndTeamManagementSystem.CQRS.Commands.TaskCommand;
using Microsoft.AspNetCore.Identity;
using TaskAndTeamManagementSystem.Data;
using TaskAndTeamManagementSystem.Models;
using TaskAndTeamManagementSystem.CQRS.Queries.TaskQueries;
using Microsoft.EntityFrameworkCore;
using TaskAndTeamManagementSystem.DTOs;

namespace TaskAndTeamManagementSystem.CQRS.Commands.TaskCommand
{
    public class DeleteTaskCommandHandler
    {
        private readonly Context _context;
        public DeleteTaskCommandHandler(Context context)
        {
            _context = context;
        }

        public async Task Handle(int id)
        {
          
            var existing = await _context.UserTasks.FindAsync(id);
            if (existing == null)
            {
                throw new Exception("Task not exist");
            }
            _context.UserTasks.Remove(existing);
            await _context.SaveChangesAsync();
            
        }
    }
}