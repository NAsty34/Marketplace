namespace data.model;

public class Page<T>
{


    public static Page<T> Create<T, U>(Page<U> page, IEnumerable<T> items)
    {
        return new Page<T>()
        {
            Count = page.Count,
            Size = page.Size,
            Total = page.Total,
            TotalPages = page.TotalPages,
            CurrentPage = page.CurrentPage,
            Items = items,
        };

    }
    public int Total { get; set; }
    public int Count { get; set; }
    public int Size { get; set; }
    public int CurrentPage { get; set; }
    public int TotalPages { get; set; }
    public IEnumerable<T>? Items { get; set; }
}