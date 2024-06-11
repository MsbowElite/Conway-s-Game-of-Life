using System.Text.Json;

namespace GameOfLife.Domain.GameStates
{
    public class GameStateSimulation
    {
        private int Width { get; }
        private int Height { get; }
        private bool[][] Cells { get; set; }
        private bool[][] CellsFutureState { get; set; }

        public GameStateSimulation(string state)
        {
            Cells = JsonSerializer.Deserialize<bool[][]>(state);

            Width = Cells.Length;
            Height = Cells[0].Length;

            CellsFutureState = new bool[Width][];
            for (int i = 0; i < Width; i++)
            {
                CellsFutureState[i] = new bool[Height];
            }

            Next();
        }

        public string GetJson()
        {
            return JsonSerializer.Serialize(Cells);
        }

        private int LiveNeighbours(int x, int y)
        {
            int NeighborsCount = 0;
            int neighborPosX;
            int neighborPosY;

            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    neighborPosX = x + i;

                    if (neighborPosX >= Width)
                    {
                        neighborPosX = 0;
                    }

                    if (neighborPosX < 0)
                    {
                        neighborPosX = Width - 1;
                    }

                    neighborPosY = y + j;

                    if (neighborPosY >= Height)
                    {
                        neighborPosY = 0;
                    }

                    if (neighborPosY < 0)
                    {
                        neighborPosY = Height - 1;
                    }

                    if (Cells[neighborPosX][neighborPosY] == true)
                        NeighborsCount = NeighborsCount + 1;
                }
            }

            if (Cells[x][y] == true)
                NeighborsCount = NeighborsCount - 1;

            return NeighborsCount;
        }

        private void Next()
        {
            for (int i = 0; i < Width; i++)
            {
                for (int j = 0; j < Height; j++)
                {
                    int NbVoisin = LiveNeighbours(i, j);

                    if (NbVoisin <= 1)
                    {
                        CellsFutureState[i][j] = false;
                    }
                    else if (NbVoisin == 2)
                    {
                        CellsFutureState[i][j] = Cells[i][j];
                    }
                    else if (NbVoisin == 3)
                    {
                        CellsFutureState[i][j] = true;
                    }
                    else
                    {
                        CellsFutureState[i][j] = false;
                    }
                }
            }

            transferBoolArray();
        }

        private void transferBoolArray()
        {
            for (int i = 0; i < Width; i++)
            {
                for (int j = 0; j < Height; j++)
                {
                    Cells[i][j] = CellsFutureState[i][j];
                }
            }
        }
    }
}
