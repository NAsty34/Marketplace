using data.model;

namespace data;

public static class QuerableExtension
{
    public static Page<T> GetPage<T> (this IQueryable<T> queryable, int page, int size)
    {
        IEnumerable<T> items = queryable.Skip((page-1) * size).Take(size).ToList();
        Page<T> p = new Page<T>();
        p.Count = items.Count();
        p.CurrentPage = page;
        p.Size = size;
        p.Items = items;
        p.Total = queryable.Count();
        p.TotalPages = (int)Math.Ceiling(p.Total / (double)size);
        return p;
    }
}