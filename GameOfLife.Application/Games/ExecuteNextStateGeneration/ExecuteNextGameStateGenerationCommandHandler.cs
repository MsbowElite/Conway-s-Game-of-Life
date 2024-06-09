using GameOfLife.Application.Abstractions.Messaging;
using GameOfLife.Domain.Games;
using GameOfLife.Domain.GameStates;
using GameOfLife.SharedKernel;
using GameOfLife.SharedKernel.Abstractions;

namespace GameOfLife.Application.Games.ExecuteNextStateGeneration;

public sealed record ExecuteNextGameStateGenerationCommand(
    Guid GameId) : ICommand<Guid>;

internal sealed class ExecuteNextGameStateGenerationCommandHandler(
    IGameRepository gameRepository,
    IGameStateRepository gameStateRepository,
    IUnitOfWork unitOfWork,
    GameStateConfig gameStateConfig)
    : ICommandHandler<ExecuteNextGameStateGenerationCommand, Guid>
{
    public async Task<Result<Guid>> Handle(
    ExecuteNextGameStateGenerationCommand command,
    CancellationToken cancellationToken)
    {
        var game = await gameRepository.GetByIdAsync(command.GameId, cancellationToken);
        if (game is null)
            return Result.Failure<Guid>(GameErrors.NotFound(command.GameId));

        if (IfExistsFinalState(game))
            return Result.Failure<Guid>(GameErrors.FinalStateAlreadyReached(command.GameId));

        var lastGameState = await gameStateRepository.GetLastByGameId(command.GameId, cancellationToken);
        if (lastGameState is null)
            return Result.Failure<Guid>(GameErrors.StateNotFound(command.GameId));

        if(lastGameState.GenerationNumber >= gameStateConfig.MaxAttempts)
            return Result.Failure<Guid>(GameErrors.MaxAttemptsReached(command.GameId, gameStateConfig.MaxAttempts));

        var newGameState = new GameState(
            Guid.NewGuid(),
            command.GameId,
            lastGameState.State,
            lastGameState.GenerationNumber
            );
        newGameState.ExecuteNextGaneration();

        if (await CheckIfIsFinalStateAsync(lastGameState, newGameState, cancellationToken))
        {
            game.FinalGameStateId = lastGameState.GameId;
            await unitOfWork.SaveChangesAsync(cancellationToken);
            return Result.Failure<Guid>(GameErrors.FinalStateAlreadyReached(command.GameId));
        }

        await gameStateRepository.InsertAsync(newGameState, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return newGameState.Id;
    }

    /// <summary>
    /// If the game stop to create states different then past 2 generations, the game has already reached the conclusion.  
    /// </summary>
    /// <returns></returns>
    private async ValueTask<bool> CheckIfIsFinalStateAsync(GameState lastGameState, GameState newGameState, CancellationToken cancellationToken)
    {
        if (!IfIsSameAsLastState(lastGameState, newGameState) && 
            CheckIfHaveAtLeastTwoValidStates(newGameState.GenerationNumber))
        {
            var pastGameState = await gameStateRepository.GetByGameIdAndGenerationNumberAsync(
                lastGameState.GameId,
                Convert.ToUInt16(lastGameState.GenerationNumber - 1),
                cancellationToken
                );
        }

        return false;
    }

    /// <summary>
    /// Fail first concept applied
    /// </summary>
    /// <param name="game"></param>
    /// <returns></returns>
    private bool IfExistsFinalState(Game game) => game.FinalGameStateId is not null;

    private bool IfIsSameAsLastState(GameState lastGameState, GameState newGameState) => 
        string.Equals(lastGameState.State, newGameState.State);

    private bool CheckIfHaveAtLeastTwoValidStates(ushort generationNumber) => generationNumber > 1;
}

