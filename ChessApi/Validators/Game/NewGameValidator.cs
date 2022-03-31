using ChessApi.Data;
using ChessApi.Dto;
using FluentValidation;

namespace ChessApi.Validators.Game
{
    public class NewGameValidator : AbstractValidator<NewGameDto>
    {
        public NewGameValidator(IUsersRepository userRepository)
        {
            CascadeMode = CascadeMode.Stop;

            RuleFor(game => game.Player1).NotEmpty();
            RuleFor(game => game.Player2).NotEmpty();
            RuleFor(game => game.Player1).NotEqual(game => game.Player2).WithMessage("Player 1 and 2 can't be the same");
            RuleFor(game => game.Player1).Must(userId => userRepository.GetUserById(userId) != null).WithMessage(game => $"A Player with the id \"{game.Player1}\" does not exist");
            RuleFor(game => game.Player2).Must(userId => userRepository.GetUserById(userId) != null).WithMessage(game => $"A Player with the id \"{game.Player2}\" does not exist"); ;
        }
    }
}
