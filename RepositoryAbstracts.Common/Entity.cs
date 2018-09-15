using System;

namespace RepositoryAbstracts
{
    /// <summary>
    /// Describes the base class for an entity
    /// </summary>
    public abstract class Entity
    {
        /// <summary>
        /// The unique identifier of this object
        /// </summary>
        public Guid Id { get; private set; } = Guid.NewGuid();
    }
}
