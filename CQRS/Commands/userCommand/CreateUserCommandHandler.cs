using TaskAndTeamManagementSystem.CQRS.Commands.userCommand;
using Microsoft.AspNetCore.Identity;
using TaskAndTeamManagementSystem.Data;
using TaskAndTeamManagementSystem.Models;
using TaskAndTeamManagementSystem.CQRS.Queries.userQueries;
using Microsoft.EntityFrameworkCore;
using TaskAndTeamManagementSystem.DTOs;

namespace TaskAndTeamManagementSystem.CQRS.Commands.userCommand
{
    public class CreateUserCommandHandler
    {
        private readonly Context _context;
        public CreateUserCommandHandler(Context context)
        {
            _context = context;
        }

        public async Task<GetUserDto> Handle(CreateUserCommandDto createUserDto)
        {
            var checkIfuserExist = await _context.Users.AnyAsync(u => u.Email == createUserDto.Email);
            if (checkIfuserExist)
            {
                throw new Exception("Email is already registered.");
            }
                
            var hasher = new PasswordHasher<User>();
            var user = new User
            {
                FullName = createUserDto.FullName,
                Email = createUserDto.Email,
                Role = (UserRole) createUserDto.Role,
            };
            user.Password = hasher.HashPassword(user, createUserDto.Password);
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return new GetUserDto
            {
                Id = user.Id,
                FullName = user.FullName,
                Email = user.Email,
                Role = user.Role,
                Teams = user.Teams,
                AssignedTasks = user.AssignedTasks
            };
        }
    }
}