using Ardalis.Specification;
using ClientApp.Core.Aggregates.Client.Entities;
using ClientApp.Core.Common;

namespace ClientApp.Core.Aggregates.Client.Specs;

public class GetClientAuditSpec : Specification<ClientEntity>
{
    public GetClientAuditSpec(Pagination pagination, GetClientAuditRequest request)
    {
        Query
            .OrderByDescending(x => x.ModifiedAt)
            .Skip(pagination.Skip)
            .Take(pagination.PageSize);
    }
}
