namespace data.model;

public class PageEntity<T>
{
   public static PageEntity<T> Create<T, U>(PageEntity<U> pageEntity, IEnumerable<T> items)
    {
        return new PageEntity<T>()
        {
            Count = pageEntity.Count,
            Size = pageEntity.Size,
            Total = pageEntity.Total,
            TotalPages = pageEntity.TotalPages,
            CurrentPage = pageEntity.CurrentPage,
            Items = items,
        };

    }
    public int Total { get; set; } //общее количество в бд
    public int Count { get; set; } //количество эментов, которые возвращаем
    public int? Size { get; set; } //размер страницы
    public int? CurrentPage { get; set; } //кол-во страниц
    public int TotalPages { get; set; } //общее кол-во страниц
    public IEnumerable<T>? Items { get; set; } //элементы
}