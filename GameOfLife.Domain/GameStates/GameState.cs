using GameOfLife.Domain.Games;
using GameOfLife.SharedKernel;
using System.Text.Json;

namespace GameOfLife.Domain.GameStates;

public sealed partial class GameState : Entity
{
    public GameState(
        Guid id,
        Guid gameId,
        bool[][] cellsState) : base(id)
    {
        GameId = gameId;
        State = JsonSerializer.Serialize(cellsState);
    }

    public GameState(
    Guid id,
    Guid gameId,
    string state,
    ushort generationNumber) : base(id)
    {
        GameId = gameId;
        State = state;
        GenerationNumber = generationNumber;
    }

    private GameState() { }

    public string State { get; set; }
    public ushort GenerationNumber { get; set; }
    public Guid GameId { get; set; }

    public Game? GameRelation { get; set; }
    public Game Game { get; set; }

    public void ExecuteNextGaneration()
    {
        bool[][] cells;
        bool[][] cellsFutureState;

        cells = JsonSerializer.Deserialize<bool[][]>(State);
        var width = cells.Length;
        var height = cells[0].Length;

        cellsFutureState = new bool[width][];
        for (int i = 0; i < width; i++)
        {
            cellsFutureState[i] = new bool[height];
        }

        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                int neighboursLiveCount = liveNeighbours(i, j);

                if (neighboursLiveCount <= 1)
                {
                    cellsFutureState[i][j] = false;
                }
                else if (neighboursLiveCount == 2)
                {
                    cellsFutureState[i][j] = cells[i][j];
                }
                else if (neighboursLiveCount == 3)
                {
                    cellsFutureState[i][j] = true;
                }
                else
                {
                    cellsFutureState[i][j] = false;
                }
            }
        }

        transferBoolArray();
        State = JsonSerializer.Serialize(cells);
        GenerationNumber++;

        #region Local Functions
        //Analyze all lives around
        int liveNeighbours(int x, int y)
        {
            int NeighborsCount = 0;
            int neighborPosX;
            int neighborPosY;

            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    neighborPosX = x + i;

                    if (neighborPosX >= width)
                    {
                        neighborPosX = 0;
                    }

                    if (neighborPosX < 0)
                    {
                        neighborPosX = width - 1;
                    }

                    neighborPosY = y + j;

                    if (neighborPosY >= height)
                    {
                        neighborPosY = 0;
                    }

                    if (neighborPosY < 0)
                    {
                        neighborPosY = height - 1;
                    }

                    if (cells[neighborPosX][neighborPosY] == true)
                        NeighborsCount = NeighborsCount + 1;
                }
            }

            if (cells[x][y] == true)
                NeighborsCount = NeighborsCount - 1;

            return NeighborsCount;
        }
        void transferBoolArray()
        {
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    cells[i][j] = cellsFutureState[i][j];
                }
            }
        }
        #endregion
    }
}
