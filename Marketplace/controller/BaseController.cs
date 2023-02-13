using AutoMapper;
using data.model;
using logic.Service;
using logic.Service.Inreface;
using Marketplace.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Marketplace.controller;

public class BaseController<T, U>:Controller where U: DictionaryDTO<T>
{
    private readonly IBaseService<T> BaseService;
    private readonly IMapper _mapper;
    
    public BaseController(IBaseService<T> _base, IMapper mapper)
    {
        this._mapper = mapper;
        this.BaseService = _base;
    }

    
    [HttpGet]
    public async Task<ResponceDto<Page<U>>> All()
    {
        Page<T> GetAll = await BaseService.Page(1, 20);
        Page<U> result = _mapper.Map<Page<U>>(GetAll);
        return new (result);
    }
    
    [HttpPost]
    public async Task<ResponceDto<U>> Create([FromBody] T _t)
    {
        BaseService.Create(_t);
        U result = _mapper.Map<U>(_t);
        return new (result);
    }
    [Authorize (Roles = "Admin")]
    [HttpPut]
    public async Task<ResponceDto<U>> Edit([FromBody] T _t)
    {
        await BaseService.Edit(_t);
        U result = _mapper.Map<U>(_t);
        return new (result);
    }
    
    [Authorize (Roles = "Admin")]
    [Route("{id}")]
    [HttpDelete]
    public string Delete(Guid id)
    {
        BaseService.Delete(id);
        return ("Успешно удалено");
    }
}