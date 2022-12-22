namespace Day11;

public static class IntegerExtensions
{
    public static double DivideByAndRoundDown(this double integer, int divideBy)
    {
        var integerAsFloat = (float)integer;
        var divided = integerAsFloat / divideBy;

        return Math.Floor(divided);
    }
}
