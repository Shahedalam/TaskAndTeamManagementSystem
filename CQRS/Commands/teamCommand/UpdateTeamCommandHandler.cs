using TaskAndTeamManagementSystem.CQRS.Commands.TeamCommand;
using Microsoft.AspNetCore.Identity;
using TaskAndTeamManagementSystem.Data;
using TaskAndTeamManagementSystem.Models;
using TaskAndTeamManagementSystem.CQRS.Queries.TeamQueries;
using Microsoft.EntityFrameworkCore;
using TaskAndTeamManagementSystem.DTOs;

namespace TaskAndTeamManagementSystem.CQRS.Commands.TeamCommand
{
    public class UpdateTeamCommandHandler
    {
        private readonly Context _context;
        public UpdateTeamCommandHandler(Context context)
        {
            _context = context;
        }

        public async Task<GetTeamDto> Handle(int id, UpdateTeamCommandDto TeamDto)
        {
         
            var existing = await _context.Teams.FindAsync(id);
            if (existing == null)
            {
                throw new Exception("Team not exist");
            }
      
            if (TeamDto.Name != null && existing.Name != TeamDto.Name)
            {
                
                var checkIfTeamExist = await _context.Teams.AnyAsync(u => u.Name == TeamDto.Name);
                if (checkIfTeamExist)
                {
                    throw new Exception("Name is already registered.");
                }
             
                existing.Name = TeamDto.Name;
            }
                
            if (TeamDto.Description != null)
                existing.Description = TeamDto.Description;

          

            await _context.SaveChangesAsync();


            var newTeam = await _context.Teams
                .Include(t => t.Members)
                .FirstOrDefaultAsync(t => t.Id == existing.Id);

            if (newTeam == null)
                throw new Exception("Something Went Wrong");


            var newMembers = await _context.Users
                .Where(u => TeamDto.MemberIds.Contains(u.Id))
                .ToListAsync();

            // Clear and reassign members
            newTeam.Members.Clear();
            foreach (var member in newMembers)
            {
                newTeam.Members.Add(member);
            }
            await _context.SaveChangesAsync();
            return new GetTeamDto
            {
                Id = existing.Id,
                Name = existing.Name,
                Description = existing.Description,
                Members = existing.Members,
                Tasks = existing.Tasks
            };
        }
    }
}