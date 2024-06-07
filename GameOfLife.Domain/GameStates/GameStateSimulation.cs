using GameOfLife.Domain.GamesStates;
using System.Text.Json;

namespace GameOfLife.Domain.GameStates
{
    public class GameStateSimulation
    {
        public int Width { get; }
        public int Height { get; }
        public byte[,] Cells { get; private set; }

        public GameStateSimulation(GamesState gamesState)
        {
            var currentState = JsonSerializer.Deserialize<byte[,]>(gamesState.State);

            Width = currentState.Length;
            Height = currentState.Length;

            Cells = new byte[Width, Height];
        }

        private int LiveNeighbours(int x, int y)
        {
            var liveNeighbours = 0;
            for (var i = -1; i <= 1; i++)
            {
                for (var j = -1; j <= 1; j++)
                {
                    if (x + i < 0 || x + i >= Width)
                        continue;
                    if (y + j < 0 || y + j >= Height)
                        continue;
                    if (x + i == x && y + j == y)
                        continue;

                    liveNeighbours += Cells[x + i, y + j] >= 1 ? 1 : 0;
                }
            }

            return liveNeighbours;
        }

        public string Next()
        {
            var cells = new byte[Width, Height];

            for (var x = 0; x < Width; x++)
            {
                for (var y = 0; y < Height; y++)
                {
                    var liveNeighbours = LiveNeighbours(x, y);
                    var age = Cells[x, y];

                    if (liveNeighbours == 3)
                    {
                        if (age > 8)
                        {
                            cells[x, y] = 0;
                        }
                        else
                        {
                            cells[x, y] = age++;
                        }
                    }
                    else if (liveNeighbours == 2)
                    {
                        cells[x, y] = age;
                    }
                    else
                    {
                        cells[x, y] = 0;
                    }
                }
            }

            Cells = cells;
            return JsonSerializer.Serialize(Cells);
        }
    }
}
