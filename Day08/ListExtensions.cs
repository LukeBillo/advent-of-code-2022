namespace Day08;

public static class ListExtensions
{
    public static int CountUpUntilAndIncludingWhere<T>(this List<T> list, Func<T, bool> predicate)
    {
        var count = 0;

        if (list.Count is 0)
        {
            return 0;
        }

        while (count < list.Count && predicate(list[count]))
        {
            count++;
        }

        return count < list.Count && !predicate(list[count]) ? count + 1 : count;
    }
}
