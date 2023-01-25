using data.model;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace logic.Service.Inreface;

public interface IBaseService<T> 
{
    public Page<T> Page(int page, int size);
    public T Create(T t);
    public T Edit(T t);
    public void Delete (Guid id);
}