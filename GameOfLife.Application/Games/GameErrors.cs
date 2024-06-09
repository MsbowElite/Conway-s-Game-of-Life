using GameOfLife.SharedKernel;

namespace GameOfLife.Application.Games;

public static class GameErrors
{
    public static Error NotFound(Guid gameId) => Error.NotFound(
        "Games.NotFound",
        $"The game with the Id = '{gameId}' was not found");

    public static Error StateNotFound(ushort generationNumber) => Error.NotFound(
        "Game.GameStates.NotFound",
        $"The gameState with the GenerationNumber = '{generationNumber}' was not found");

    public static Error StateNotFound(Guid gameId) => Error.NotFound(
        "Game.GameStates.NotFound",
        $"The game with Id = '{gameId}', no GameState was found");

    public static Error MaxAttemptsReached(Guid gameId, ushort maxNumberOfAttempts) => Error.Conflict(
        "Game.ReachedMaxAttempts",
        $"The game with Id = '{gameId}', reached the max number of attempts {maxNumberOfAttempts}, and have no conclusion");

    public static Error FinalStateAlreadyReached(Guid gameId) => Error.Conflict(
        "Games.Conflict",
        $"The game with the Id = '{gameId}' has already reached the final state, you cannot create the next generation");

    public static Error CriticalFailure() => Error.Problem(
        "Games.Critical",
        $"Critical error occurred");
}
