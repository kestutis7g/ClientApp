using Ardalis.Specification;
using ClientApp.Core.Aggregates.Client.Entities;

namespace ClientApp.Core.Aggregates.Client.Specs;

public class GetClientByParamsSpec : Specification<ClientEntity>, ISingleResultSpecification<ClientEntity>
{
    public GetClientByParamsSpec(string name, string address)
    {
        Query
            .Where(c => c.Name == name && c.Address == address);
    }
}
