using data.model;

namespace data;

public static class QuerableExtension
{
    public static Page<T> GetPage<T> (this IQueryable<T> queryable, int? page, int? size)
    {
        if (page == null || size == null)
        {
            page = 1;
            size = 1;
        }

        if (page > size) throw new SystemException("Размер страницы должен быть больше текущей страницы");
        IEnumerable<T> items = queryable.Skip((int)((page-1) * size)).Take((int)size).ToList();
        Page<T> p = new Page<T>();
        p.Count = items.Count();
        /*p.CurrentPage = page;*/
        p.Size = (int)size;
        p.Items = items;
        /*p.Total = queryable.Count();
        p.TotalPages = (int)Math.Ceiling(p.Total / (double)size);*/
        return p;
    }
}