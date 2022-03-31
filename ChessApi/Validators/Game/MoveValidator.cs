using ChessApi.Dto;
using ChessApi.Model;
using FluentValidation;

namespace ChessApi.Validators.Game
{
    public class MoveValidator : AbstractValidator<MoveDto>
    {
        public MoveValidator()
        {
            RuleFor(move => move.From).NotEmpty();
            RuleFor(move => move.To).NotEmpty();
            RuleFor(move => move.From).Must(from => CellPosition.IsCellPosition(from)).WithMessage("From is no valid cell position");
            RuleFor(move => move.To).Must(to => CellPosition.IsCellPosition(to)).WithMessage("To is no valid cell position"); ;
        }
    }
}
