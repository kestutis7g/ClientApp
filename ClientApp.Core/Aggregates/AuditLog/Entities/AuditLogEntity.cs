using ClientApp.Shared.Interfaces;

namespace ClientApp.Core.Aggregates.AuditLog.Entities;

public class AuditLogEntity : BaseEntity, IAggregateRoot
{
    public string TableName { get; set; }
    public string Action { get; set; }
    public string? KeyValues { get; set; }
    public string? OldValues { get; set; }
    public string? NewValues { get; set; }
    public DateTimeOffset Timestamp { get; set; }


    public AuditLogEntity()
    {
        TableName = string.Empty;
        Action = string.Empty;
        KeyValues = string.Empty;
        OldValues = string.Empty;
        NewValues = string.Empty;
        Timestamp = DateTimeOffset.UtcNow;
    }
    public AuditLogEntity(string tableName, string action, string keyValues, string oldValues, string newValues, DateTimeOffset timestamp)
    {
        TableName = tableName;
        Action = action;
        KeyValues = keyValues;
        OldValues = oldValues;
        NewValues = newValues;
        Timestamp = timestamp;
    }
}