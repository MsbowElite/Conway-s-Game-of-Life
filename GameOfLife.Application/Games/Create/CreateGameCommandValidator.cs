using FluentValidation;

namespace GameOfLife.Application.Games.Create
{
    internal sealed class CreateGameCommandValidator : AbstractValidator<CreateGameCommand>
    {
        public CreateGameCommandValidator()
        {
            RuleFor(game => game.State)
                .NotNull().NotEmpty();
        }
    }
}
