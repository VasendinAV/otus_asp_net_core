﻿using PromoCodeFactory.Core.Abstractions.Repositories;
using PromoCodeFactory.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PromoCodeFactory.DataAccess.Repositories;

public class InMemoryRepository<T> : IRepository<T>
    where T : BaseEntity
{
    protected IEnumerable<T> Data { get; set; }

    public InMemoryRepository(IEnumerable<T> data)
    {
        Data = data;
    }

    public Task<IEnumerable<T>> GetAllAsync()
    {
        return Task.FromResult(Data);
    }

    public Task<T> GetByIdAsync(Guid id)
    {
        return Task.FromResult(Data.FirstOrDefault(x => x.Id == id));
    }

    public Task<Guid> CreateAsync(T entity)
    {
        return Task.Run(() =>
        {
            Data = Data.Append(entity);
            return entity.Id;
        });
    }

    public Task DeleteAsync(Guid id)
    {
        return Task.Run(() =>
        {
            Data = Data.Where(x => x.Id != id);
        });
    }  
}