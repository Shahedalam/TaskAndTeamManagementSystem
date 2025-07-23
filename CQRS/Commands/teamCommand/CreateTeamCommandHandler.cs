using TaskAndTeamManagementSystem.CQRS.Commands.TeamCommand;
using Microsoft.AspNetCore.Identity;
using TaskAndTeamManagementSystem.Data;
using TaskAndTeamManagementSystem.Models;
using TaskAndTeamManagementSystem.CQRS.Queries.TeamQueries;
using Microsoft.EntityFrameworkCore;
using TaskAndTeamManagementSystem.DTOs;

namespace TaskAndTeamManagementSystem.CQRS.Commands.TeamCommand
{
    public class CreateTeamCommandHandler
    {
        private readonly Context _context;
        public CreateTeamCommandHandler(Context context)
        {
            _context = context;
        }

        public async Task<GetTeamDto> Handle(CreateTeamCommandDto createTeamDto)
        {
            var checkIfTeamExist = await _context.Teams.AnyAsync(u => u.Name == createTeamDto.Name);
            if (checkIfTeamExist)
            {
                throw new Exception("Name is already registered.");
            }
            var team = new Team
            {
                Name = createTeamDto.Name,
                Description = createTeamDto.Description, 
            };
            _context.Teams.Add(team);
            await _context.SaveChangesAsync();

            var newTeam = await _context.Teams
                .Include(t => t.Members)
                .FirstOrDefaultAsync(t => t.Id == team.Id);

            if (newTeam == null)
                throw new Exception("Something Went Wrong");


            var newMembers = await _context.Users
                .Where(u => createTeamDto.MemberIds.Contains(u.Id))
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
                Id = team.Id,
                Name = team.Name,
                Description = team.Description,
                Members = team.Members,
                Tasks = team.Tasks
            };
        }
    }
}