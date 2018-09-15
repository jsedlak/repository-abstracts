using RepositoryAbstracts;
using System;
using System.Collections.Generic;

namespace AbstractBaseSample
{
    /// <summary>
    /// Defines the base functionality that a repository must contain
    /// </summary>
    public abstract class DataRepository
    {
        protected abstract TData Get<TData>(Guid id) where TData : Entity;
        protected abstract bool Insert<TData>(TData data) where TData : Entity;
        protected abstract IEnumerable<TData> Query<TData>(Func<TData, bool> predicate) where TData : Entity;
        protected abstract bool Update<TData>(TData data) where TData : Entity;
        protected abstract bool Delete<TData>(Guid id) where TData : Entity;
    }
}
