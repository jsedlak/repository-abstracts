using RepositoryAbstracts;
using System;

namespace GatewayRepository
{
    /// <summary>
    /// Describes a basic gateway for building a service layer for a specific tenant.
    /// In the real world a lot of this may be hardcoded and or driven by configuration or even by types.
    /// And likely you'll want to build out a DI service context / container for each tenant (depending on your specific architecture).
    /// For the purposes of this sample, we just want to show that you can potentially swap out the underlying technology using an interface.
    /// </summary>
    public sealed class AwesomeAppGateway
    {
        private readonly Func<Entity, IRepository> _repositoryCallback;

        public AwesomeAppGateway(Func<Entity, IRepository> repositoryCallback)
        {
            _repositoryCallback = repositoryCallback;
        }

        private IRepository GetRepository(Entity entity)
        {
            return _repositoryCallback(entity);
        }

        public AwesomeAppBusinessLayer GetServiceLayer(Entity entity)
        {
            var repo = GetRepository(entity);
            return new AwesomeAppBusinessLayer(repo);
        }
    }
}
