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
}
