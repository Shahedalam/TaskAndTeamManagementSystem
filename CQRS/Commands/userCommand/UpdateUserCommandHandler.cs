using TaskAndTeamManagementSystem.CQRS.Commands.userCommand;
using Microsoft.AspNetCore.Identity;
using TaskAndTeamManagementSystem.Data;
using TaskAndTeamManagementSystem.Models;
using TaskAndTeamManagementSystem.CQRS.Queries.userQueries;
using Microsoft.EntityFrameworkCore;
using TaskAndTeamManagementSystem.DTOs;

namespace TaskAndTeamManagementSystem.CQRS.Commands.userCommand
{
    public class UpdateUserCommandHandler
    {
        private readonly Context _context;
        public UpdateUserCommandHandler(Context context)
        {
            _context = context;
        }

        public async Task<GetUserDto> Handle(int id, UpdateUserCommandDto userDto)
        {
            var hasher = new PasswordHasher<User>();
            var existing = await _context.Users.FindAsync(id);
            if (existing == null)
            {
                throw new Exception("User not exist");
            }
            if(userDto.FullName != null)
                existing.FullName = userDto.FullName;

            if (userDto.Email != null && existing.Email != userDto.Email)
            {
                
                var checkIfuserExist = await _context.Users.AnyAsync(u => u.Email == userDto.Email);
                if (checkIfuserExist)
                {
                    throw new Exception("Email is already registered.");
                }
             
                existing.Email = userDto.Email;
            }
                
            if (userDto.Role.Value != null)
                existing.Role = userDto.Role.Value;

            if (userDto.Password != null)
                existing.Password = hasher.HashPassword(existing, userDto.Password);

            await _context.SaveChangesAsync();
            return new GetUserDto
            {
                Id = existing.Id,
                FullName = existing.FullName,
                Email = existing.Email,
                Role = existing.Role,
                Teams = existing.Teams,
                AssignedTasks = existing.AssignedTasks
            };
        }
    }
}