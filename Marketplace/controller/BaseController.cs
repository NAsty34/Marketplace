using AutoMapper;
using data.model;
using logic.Service.Inreface;
using Marketplace.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Marketplace.controller;

public class BaseController<T, U>:Controller where U: DictionaryDto<T>
{
    private readonly IBaseService<T> _baseService;
    private readonly IMapper _mapper;
    
    public BaseController(IBaseService<T> @base, IMapper mapper)
    {
        _mapper = mapper;
        _baseService = @base;
    }

    
    [HttpGet]
    public async Task<ResponceDto<Page<U>>> All( int? page, int? size)
    {
        Page<T> getAll = await _baseService.Page(page, size);
        Page<U> result = _mapper.Map<Page<U>>(getAll);
        return new (result);
    }
    
    [HttpPost]
    public async Task<ResponceDto<U>> Create([FromBody] T t)
    {
        await _baseService.Create(t);
        U result = _mapper.Map<U>(t);
        return new (result);
    }
    [Authorize (Roles = nameof(Role.Admin))]
    [HttpPut]
    public async Task<ResponceDto<U>> Edit([FromBody] T t)
    {
        await _baseService.Edit(t);
        U result = _mapper.Map<U>(t);
        return new (result);
    }
    
    [Authorize (Roles = nameof(Role.Admin))]
    [Route("{id}")]
    [HttpDelete]
    public string Delete(Guid id)
    {
        _baseService.Delete(id);
        return ("Успешно удалено");
    }
}