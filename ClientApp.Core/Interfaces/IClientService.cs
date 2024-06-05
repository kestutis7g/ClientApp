using ClientApp.Core.Aggregates.Client.Entities;

namespace ClientApp.Core.Interfaces;

public interface IClientService
{
    Task<ICollection<ClientEntity>> GetClientList();
    Task<int> ImportClients(List<ClientEntity> request);
    Task<bool> UpdateClients(string apiKey);
}
