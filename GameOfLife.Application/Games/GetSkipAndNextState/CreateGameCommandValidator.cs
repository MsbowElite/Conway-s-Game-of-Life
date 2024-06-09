using FluentValidation;

namespace GameOfLife.Application.Games.GetSkipAndNextState
{
    internal sealed class GetNextGameStateCommandValidator : AbstractValidator<GetSkipAndNextGameStateCommand>
    {
        public GetNextGameStateCommandValidator()
        {
            RuleFor(c => c.GameId)
                .NotEmpty();
            RuleFor(c => c.Attempts)
                .GreaterThan((ushort)0);
        }
    }
}
