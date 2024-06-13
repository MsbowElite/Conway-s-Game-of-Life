using FluentValidation;

namespace GameOfLife.Application.Games.GetFinalGameState
{
    internal sealed class GetFinalGameStateQueryValidator : AbstractValidator<GetFinalGameStateQuery>
    {
        public GetFinalGameStateQueryValidator()
        {
            RuleFor(c => c.GameId)
                .NotEmpty();
        }
    }
}
