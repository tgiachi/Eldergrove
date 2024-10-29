namespace Eldergrove.Engine.Core.Extensions;

public static class RandomExtension
{
    public static TElement RandomElement<TElement>(this IEnumerable<TElement> enumerable)
    {
        var list = enumerable.ToList();

        if (list.Count == 0)
        {
            return default!;
        }

        if (list == null)
        {
            return default;
        }

        var index = Random.Shared.Next(0, list.Count);
        return list[index];
    }

    public static IEnumerable<TElement> RandomElements<TElement>(this IEnumerable<TElement> enumerable, int count)
    {
        var list = enumerable.ToList();
        var indexes = Enumerable.Range(0, list.Count).ToList();
        var result = new List<TElement>();
        for (var i = 0; i < count; i++)
        {
            var index = Random.Shared.Next(0, indexes.Count);
            result.Add(list[indexes[index]]);
            indexes.RemoveAt(index);
        }

        return result;
    }
}
