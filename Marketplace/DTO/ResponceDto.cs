namespace Marketplace.DTO;

public class ResponceDto<T>
{
    public ResponceDto(T t, int code)
    {
        this.Data = t;
        this.Code = code;
    }
    
    public ResponceDto(T t)
    {
        this.Data = t;
    }
    
    public int Code { get; set; }
    public T Data { get; set; }
}