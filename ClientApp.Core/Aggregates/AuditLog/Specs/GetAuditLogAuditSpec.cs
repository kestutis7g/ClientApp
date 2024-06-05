using Ardalis.Specification;
using ClientApp.Core.Aggregates.AuditLog.Entities;
using ClientApp.Core.Aggregates.AuditLog;
using ClientApp.Core.Common;

namespace ClientApp.Core.Aggregates.Client.Specs;

public class GetAuditLogAuditSpec : Specification<AuditLogEntity>
{
    public GetAuditLogAuditSpec(Pagination pagination, GetAuditLogAuditRequest request)
    {
        Query
            .OrderByDescending(x => x.ModifiedAt)
            .Skip(pagination.Skip)
            .Take(pagination.PageSize);
    }
}
