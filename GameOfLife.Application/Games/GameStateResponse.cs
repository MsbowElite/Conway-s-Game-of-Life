using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLife.Application.Games;

public sealed record GameStateResponse
{
    public Guid Id { get; init; }
    public string State { get; init; }
    public ushort Generation { get; init; }
    public DateTime CreatedAt { get; init; }
    public DateTime LastUpdatedAt { get; init; }
    public DateTime DeletedAt { get; init; }
}
