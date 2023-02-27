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
    public async Task<ResponceDto<PageEntity<U>>> All( int? page, int? size)
    {
        PageEntity<T> getAll = await _baseService.Page(page, size);
        PageEntity<U> result = _mapper.Map<PageEntity<U>>(getAll);
        return new (result);
    }
    
    [HttpPost]
    public async Task<ResponceDto<U>> Create([FromBody] T t)
    {
        await _baseService.Create(t);
        U result = _mapper.Map<U>(t);
        return new (result);
    }
    [Authorize (Roles = nameof(RoleEntity.Admin))]
    [HttpPut]
    public async Task<ResponceDto<U>> Edit([FromBody] T t)
    {
        await _baseService.Edit(t);
        U result = _mapper.Map<U>(t);
        return new (result);
    }
    
    [Authorize (Roles = nameof(RoleEntity.Admin))]
    [Route("{id}")]
    [HttpDelete]
    public async Task<string> Delete(Guid id)
    {
        await _baseService.Delete(id);
        return ("Успешно удалено");
    }
}