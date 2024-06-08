using GameOfLife.SharedKernel;

namespace GameOfLife.Application.Games;

public static class GameErrors
{
    public static Error NotFound(Guid gameId) => Error.NotFound(
        "Games.NotFound",
        $"The game with the Id = '{gameId}' was not found");
}
