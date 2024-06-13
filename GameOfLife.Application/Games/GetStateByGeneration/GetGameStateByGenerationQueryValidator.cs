using FluentValidation;
using GameOfLife.Application.Games.GetStateByGeneration;
using GameOfLife.SharedKernel;

namespace GameOfLife.Application.Games.GetSkipAndNextState
{
    internal sealed class GetGameStateByGenerationQueryValidator : AbstractValidator<GetGameStateByGenerationQuery>
    {
        public GetGameStateByGenerationQueryValidator(GameStateConfig gameStateConfig)
        {
            RuleFor(c => c.GameId)
                .NotEmpty();
            RuleFor(c => c.GenerationNumber)
                .GreaterThanOrEqualTo((ushort)0)
                .LessThanOrEqualTo(gameStateConfig.MaxAttempts);
        }
    }
}
