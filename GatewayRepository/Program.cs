using RepositoryAbstracts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GatewayRepository
{
    class Program
    {
        static Dictionary<Guid, IRepository> _repositories = new Dictionary<Guid, IRepository>();

        static void Main(string[] args)
        {
            // Build our gateway.
            // You could store this in a separate project and keep the IRepository pattern segregated from 
            // engineer's code, thus preventing them from pulling a repo up
            var gateway = new AwesomeAppGateway((e) =>
            {
                if (!_repositories.TryGetValue(e.TenantId, out IRepository repo))
                {
                    // do some lookup in the config
                    if (e.TenantId == Guid.Empty)
                    {
                        repo = new InMemoryRepository();
                    }
                    else
                    {
                        repo = new InMemoryRepository();
                    }

                    _repositories.TryAdd(e.TenantId, repo);
                }

                return repo;
            });

            var tenant1Id = Guid.NewGuid();
            var tenant2Id = Guid.Empty;

            // get access to the service layer
            var tenant1Service = gateway.GetServiceLayer(new Book { TenantId = tenant1Id });
            var tenant2Service = gateway.GetServiceLayer(new Book { TenantId = tenant2Id });

            // as always, we don't interact directly with the repository
            tenant1Service.InsertBook("Test Book", "Jane Doe");

            // let's make sure it works...
            var crossLookupResult = tenant2Service.LookForBooksByAuthor("Jane Doe");
            Console.WriteLine($"Looking in Tenant 2's repo for books stored in Tenant 1's repo");
            Console.WriteLine($"Found {crossLookupResult.Count()} book[s].");

            var correctLookupResult = tenant1Service.LookForBooksByAuthor("Jane Doe");
            Console.WriteLine($"How about in the correct one...");
            Console.WriteLine($"Found {correctLookupResult.Count()} book[s].");
        }
    }
}
