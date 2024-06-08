using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLife.Application.Games;

public sealed record GameResponse
{
    public Guid Id { get; init; }
    public int Width { get; init; }
    public int Height { get; init; }
    public DateTime CreatedAt { get; init; }
    public DateTime LastUpdatedAt { get; init; }
    public DateTime DeletedAt { get; init; }
}
