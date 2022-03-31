using ChessApi.Data;
using ChessApi.Dto;
using FluentValidation;

namespace ChessApi.Validators.User
{
    public class NewUserValidator : AbstractValidator<NewUserDto>
    {
        public NewUserValidator(IUsersRepository user)
        {
            RuleFor(user => user.Name).NotEmpty();
            RuleFor(user => user.Name).Must(name => !user.UserExists(name)).WithMessage(user => $"A user with the name \"{user.Name}\" already exists.");
        }
    }
}
