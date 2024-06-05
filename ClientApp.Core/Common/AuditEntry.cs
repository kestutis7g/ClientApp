

using ClientApp.Core.Aggregates.AuditLog.Entities;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Newtonsoft.Json;

namespace ClientApp.Core.Common;

public class AuditEntry
{
    public AuditEntry(EntityEntry entry)
    {
        Entry = entry;
        TableName = string.Empty;
        Action = string.Empty;
    }

    public EntityEntry Entry { get; }
    public string TableName { get; set; }
    public string Action { get; set; }
    public Dictionary<string, object?> KeyValues { get; } = new Dictionary<string, object?>();
    public Dictionary<string, object?> OldValues { get; } = new Dictionary<string, object?>();
    public Dictionary<string, object?> NewValues { get; } = new Dictionary<string, object?>();
    public List<PropertyEntry> TemporaryProperties { get; } = new List<PropertyEntry>();

    public AuditLogEntity ToAuditLog()
    {
        var audit = new AuditLogEntity
        {
            TableName = TableName,
            Action = Action,
            KeyValues = JsonConvert.SerializeObject(KeyValues),
            OldValues = OldValues.Count == 0 ? null : JsonConvert.SerializeObject(OldValues),
            NewValues = NewValues.Count == 0 ? null : JsonConvert.SerializeObject(NewValues),
            Timestamp = DateTime.UtcNow
        };
        return audit;
    }
}
