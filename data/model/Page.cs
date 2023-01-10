namespace data.model;

public class Page<T>
{


    public static Page<T> Create<T, U>(Page<U> q, IEnumerable<T> items)
    {
        return new Page<T>()
        {
            Count = q.Count,
            Size = q.Size,
            Total = q.Total,
            TotalPages = q.TotalPages,
            CurrentPage = q.CurrentPage,
            Items = items,
        };

    }
    public int Total { get; set; }
    public int Count { get; set; }
    public int Size { get; set; }
    public int CurrentPage { get; set; }
    public int TotalPages { get; set; }
    public IEnumerable<T> Items { get; set; }
}