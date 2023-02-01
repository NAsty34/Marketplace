using data.model;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace logic.Service;

public interface IBaseService<T> 
{
    public Page<T> Page(int page, int size);
    public void Create(T t);
    public T Edit(T t);
    public void Delete (Guid id);
}