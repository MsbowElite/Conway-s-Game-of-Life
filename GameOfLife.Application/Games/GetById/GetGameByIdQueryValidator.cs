using FluentValidation;

namespace GameOfLife.Application.Games.GetById;
internal sealed class GetGameByIdQueryValidator : AbstractValidator<GetGameByIdQuery>
{
    public GetGameByIdQueryValidator()
    {
        RuleFor(c => c.GameId)
            .NotEmpty();
    }
}
