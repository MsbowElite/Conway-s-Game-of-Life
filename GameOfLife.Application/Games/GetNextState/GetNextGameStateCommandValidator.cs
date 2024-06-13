using FluentValidation;

namespace GameOfLife.Application.Games.GetNextState
{
    internal sealed class GetNextGameStateCommandValidator : AbstractValidator<GetNextGameStateCommand>
    {
        public GetNextGameStateCommandValidator()
        {
            RuleFor(c => c.GameId)
                .NotEmpty();
        }
    }
}
