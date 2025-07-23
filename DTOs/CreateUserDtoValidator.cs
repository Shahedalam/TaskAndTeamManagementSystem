using TaskAndTeamManagementSystem.Models;
using TaskAndTeamManagementSystem.CQRS.Commands.userCommand;
using FluentValidation;
namespace TaskAndTeamManagementSystem.DTOs
{
    public class CreateUserDtoValidator : AbstractValidator<CreateUserCommandDto>
    {
        public CreateUserDtoValidator()
        {
            RuleFor(x => x.FullName)
                .NotEmpty().WithMessage("Full name is required.")
                .MaximumLength(100);

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Invalid email format.");

            RuleFor(x => x.Role)
                .IsInEnum()
                .Must(role => new[] { UserRole.Manager, UserRole.Employee }.Contains(role))
                .WithMessage("Role must be Manager or Employee only.");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required.")
                .MinimumLength(6).WithMessage("Password must be at least 6 characters long.");
        }
    }
}
