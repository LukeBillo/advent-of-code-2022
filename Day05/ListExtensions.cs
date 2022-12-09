namespace Day05;

public static class ListExtensions
{
    public static void RemoveLast<T>(this List<T> list, int count)
    {
        if (list.Count is 0)
        {
            return;
        }

        if (list.Count < count)
        {
            list.Clear();
            return;
        }

        list.RemoveRange(list.Count - count, count);        
    }
}
