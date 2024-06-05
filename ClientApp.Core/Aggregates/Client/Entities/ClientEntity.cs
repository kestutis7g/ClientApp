using ClientApp.Shared.Interfaces;

namespace ClientApp.Core.Aggregates.Client.Entities;

public class ClientEntity : BaseEntity, IAggregateRoot
{
    public string Name { get; set; }
    public string Address { get; set; }
    public string PostCode { get; set; }

    public ClientEntity()
    {
        Name = string.Empty;
        Address = string.Empty;
        PostCode = string.Empty;
    }

    public void UpdatePostCode(string postCode)
    {
        PostCode = postCode;
        ModifiedAt = DateTime.UtcNow;
    }
}