namespace Day08;

public record Tree(Position Position, int Height)
{
    public bool IsHigherThanOrSameHeightAs(Tree other) => Height >= other.Height;
}

public record Position(int X, int Y)
{
    public (List<Position> north, List<Position> south, List<Position> east, List<Position> west) GetPositionsOnSameColumnOrRow(int edgeOfAxisX, int edgeOfAxisY)
    {
        var north = new List<Position>();
        var south = new List<Position>();
        
        for (var y = 0; y < edgeOfAxisY; y++)
        {
            if (y == Y)
            {
                continue;
            }

            if (y < Y)
            {
                north.Add(this with { Y = y });
            }
            else
            {
                south.Add(this with { Y = y });
            }
        }

        var east = new List<Position>();
        var west = new List<Position>();

        for (var x = 0; x < edgeOfAxisX; x++)
        {
            if (x == X)
            {
                continue;
            }

            if (x < X)
            {
                west.Add(this with { X = x });
            }
            else
            {
                east.Add(this with { X = x });
            }
        }

        

        return (north, south, east, west);
    }
}
