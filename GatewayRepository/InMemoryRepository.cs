using System;
using System.Collections.Generic;
using System.Linq;
using RepositoryAbstracts;

namespace GatewayRepository
{
    /// <summary>
    /// Defines an in memory repository, storing entities in a dictionary of collections
    /// </summary>
    public sealed class InMemoryRepository : IRepository
    {
        private readonly Dictionary<Type, object> _tables = new Dictionary<Type, object>();

        private List<TData> GetCollection<TData>()
        {
            var colDataType = typeof(TData);
            if (!_tables.TryGetValue(colDataType, out object col))
            {
                col = new List<TData>();
                _tables.Add(colDataType, col);
            }

            return col as List<TData>;
        }

        public TData Get<TData>(Guid id) where TData : Entity
        {
            return GetCollection<TData>().FirstOrDefault(m => m.Id == id);
        }

        public bool Insert<TData>(TData entity) where TData : Entity
        {
            var col = GetCollection<TData>();

            // validate
            if (col.FirstOrDefault(m => m.Id == entity.Id) != null)
            {
                return false;
            }

            col.Add(entity);
            return true;
        }

        public IEnumerable<TData> Query<TData>(Func<TData, bool> predicate) where TData : Entity
        {
            // return a copy so it can't be modified ;)
            return GetCollection<TData>().Where(predicate).ToArray();
        }

        public bool Update<TData>(TData entity) where TData : Entity
        {
            // we return references, so we dont gotta do anything
            return true;
        }

        public bool Delete<TData>(TData entity) where TData : Entity
        {
            var col = GetCollection<TData>();
            var found = col.FirstOrDefault(m => m.Id == entity.Id);

            if (found == null)
            {
                // change to suit your specific reqs
                // you can return false to say it strictly was not removed and thus the collection wasnt changed
                // or true because it didn't exist and so there is no need to produce an error
                return false;
            }

            return col.Remove(found);
        }
    }
}
