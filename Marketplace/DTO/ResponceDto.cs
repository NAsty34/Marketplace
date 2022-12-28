namespace Marketplace.DTO;

public class ResponceDto<T>
{
    public ResponceDto(T t, bool success)
    {
        this.Data = t;
        this.Success = success;
    }
    
    public ResponceDto(T t)
    {
        this.Data = t;
        this.Success = true;
    }
    
    public bool Success { get; set; }
    public T Data { get; set; }
}