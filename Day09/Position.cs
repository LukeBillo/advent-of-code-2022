namespace Day09;

public record Position(int X, int Y)
{
    public static Position operator-(Position first, Position second)
    {
        var x = first.X - second.X;
        var y = first.Y - second.Y;

        return new Position(x, y);
    }
    public Position ToAbsolute()
    {
        var x = Math.Abs(X);
        var y = Math.Abs(Y);

        return new Position(x, y);
    }
}
