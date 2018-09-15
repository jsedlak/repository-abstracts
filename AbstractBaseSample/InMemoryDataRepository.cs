using System;
using System.Collections.Generic;
using System.Linq;

namespace AbstractBaseSample
{
    /// <summary>
    /// Defines a base implementation for providing in memory storage of entities
    /// </summary>
    public abstract class InMemoryDataRepository : DataRepository
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

        protected override TData Get<TData>(Guid id) 
        {
            return GetCollection<TData>().FirstOrDefault(m => m.Id == id);
        }

        protected override bool Insert<TData>(TData data) 
        {
            var col = GetCollection<TData>();

            // validate
            if(col.FirstOrDefault(m => m.Id == data.Id) != null)
            {
                return false;
            }

            col.Add(data);
            return true;
        }

        protected override IEnumerable<TData> Query<TData>(Func<TData, bool> predicate)
        {
            // return a copy so it can't be modified ;)
            return GetCollection<TData>().Where(predicate).ToArray();
        }

        protected override bool Update<TData>(TData data)
        {
            // we return references, so we dont gotta do anything
            return true;
        }

        protected override bool Delete<TData>(Guid id)
        {
            var col = GetCollection<TData>();
            var found = col.FirstOrDefault(m => m.Id == id);

            if(found == null)
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
