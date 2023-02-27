namespace Marketplace.DTO;

public class ResponceDto<T>
{
    public ResponceDto(T t, int code)
    {
        Data = t;
        Code = code;
    }
    
    public ResponceDto(T t)
    {
        Data = t;
    }
    
    public int Code { get; set; }
    public T Data { get; set; }
}