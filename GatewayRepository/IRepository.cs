using RepositoryAbstracts;
using System;
using System.Collections.Generic;

namespace GatewayRepository
{
    /// <summary>
    /// We'll build a generic repository interface because the assumption is that we don't know the implementation at runtime.
    /// We fully recognize that exposing such an interface could be problematic as developers can pull an IRepository interface from DI container.
    /// </summary>
    public interface IRepository
    {
        TData Get<TData>(Guid id) where TData : Entity;
        IEnumerable<TData> Query<TData>(Func<TData, bool> predicate) where TData : Entity;

        bool Insert<TData>(TData entity) where TData : Entity;
        bool Update<TData>(TData entity) where TData : Entity;
        bool Delete<TData>(TData entity) where TData : Entity;
    }
}
