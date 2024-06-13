using FluentValidation;
using GameOfLife.Application.Games.Create;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLife.Application.Games.ExecuteNextStateGeneration;
internal sealed class ExecuteNextGameStateGenerationCommandValidator : AbstractValidator<ExecuteNextGameStateGenerationCommand>
{
    public ExecuteNextGameStateGenerationCommandValidator()
    {
        RuleFor(c => c.GameId)
            .NotEmpty();
    }
}
