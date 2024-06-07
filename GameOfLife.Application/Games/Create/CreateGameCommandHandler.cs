using GameOfLife.Application.Abstractions.Messaging;
using GameOfLife.Domain.Games;
using GameOfLife.SharedKernel;

namespace GameOfLife.Application.Games.Create;

public sealed record CreateGameCommand(
    string State) : ICommand<Guid>;

internal sealed class CreateGameCommandHandler(
    IGameRepository gameRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<CreateGameCommand, Guid>
{
    public async Task<Result<Guid>> Handle(
    CreateGameCommand command,
    CancellationToken cancellationToken)
    {
        var game = new Game(
            Guid.NewGuid());

        await gameRepository.InsertAsync(game, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return game.Id;
    }
}

