using System.Text.Json;

namespace GameOfLife.Domain.GameStates
{
    public class GameStateSimulation
    {
        public int Width { get; }
        public int Height { get; }
        public ushort[][] Cells { get; private set; }

        public GameStateSimulation(GameState gamesState)
        {
            var currentState = JsonSerializer.Deserialize<ushort[][]>(gamesState.State);

            Width = currentState.Length;
            Height = currentState[0].Length;

            Cells = new ushort[Width][];
            for (int i = 0; i < Width; i++)
            {
                Cells[i] = new ushort[Height];
                Array.Copy(currentState[i], Cells[i], Height);
            }
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

                    liveNeighbours += Cells[x + i][y + j] >= 1 ? 1 : 0;
                }
            }

            return liveNeighbours;
        }

        public string Next()
        {
            var cells = new ushort[Width][];
            for (var i = 0; i < Width; i++)
            {
                cells[i] = new ushort[Height];
            }

            for (var x = 0; x < Width; x++)
            {
                for (var y = 0; y < Height; y++)
                {
                    var liveNeighbours = LiveNeighbours(x, y);
                    var age = Cells[x][y];

                    if (liveNeighbours == 3)
                    {
                        if (age > 8)
                        {
                            cells[x][y] = 0;
                        }
                        else
                        {
                            cells[x][y] = (ushort)(age + 1);
                        }
                    }
                    else if (liveNeighbours == 2)
                    {
                        cells[x][y] = age;
                    }
                    else
                    {
                        cells[x][y] = 0;
                    }
                }
            }

            Cells = cells;
            return JsonSerializer.Serialize(Cells);
        }
    }
}
