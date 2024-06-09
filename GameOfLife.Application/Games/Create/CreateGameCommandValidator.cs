using FluentValidation;

namespace GameOfLife.Application.Games.Create
{
    internal sealed class CreateGameCommandValidator : AbstractValidator<CreateGameCommand>
    {
        public CreateGameCommandValidator()
        {
            RuleFor(c => c.Width)
                .GreaterThan((ushort)0);
            RuleFor(c => c.Height)
                .GreaterThan((ushort)0);
            RuleFor(c => c.State)
                .NotNull().NotEmpty();
        }
    }
}
