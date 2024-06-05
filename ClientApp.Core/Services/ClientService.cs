using ClientApp.Core.Aggregates.Client.Entities;
using ClientApp.Core.Aggregates.Client.Specs;
using ClientApp.Core.Common;
using ClientApp.Core.Interfaces;
using ClientApp.Shared.Interfaces;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Configuration;

namespace ClientApp.Core.Services;

public class ClientService : IClientService
{
    public ClientService(IRepository<ClientEntity> clientRepo, HttpClient httpClient)
    {
        ClientRepo = clientRepo ?? throw new ArgumentNullException(nameof(clientRepo));
        _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
    }
    private IRepository<ClientEntity> ClientRepo { get; }
    private readonly HttpClient _httpClient;

    /// <summary>
    /// Return all clients stored in database
    /// </summary>
    /// <returns>list of clients</returns>
    public async Task<ICollection<ClientEntity>> GetClientList()
    {
        return await ClientRepo.ListAsync();
    }

    /// <summary>
    /// Import list of clients to database
    /// </summary>
    /// <param name="clients">list of clients to be imported</param>
    /// <returns>how many clients imported. -1 if error</returns>
    public async Task<int> ImportClients(List<ClientEntity> clients)
    {
        List<ClientEntity> toImport = new List<ClientEntity>();
        try
        {
            foreach (var client in clients)
            {
                var spec = new GetClientByParamsSpec(client.Name, client.Address);
                var existing = await ClientRepo.FirstOrDefaultAsync(spec);
                if (existing == null)
                {
                    toImport.Add(client);
                }
            }
            await ClientRepo.AddRangeAsync(toImport);
            await ClientRepo.SaveChangesAsync();
        }
        catch(Exception ex)
        {
            //TODO Probably do somthing with exception later
            Console.WriteLine($"Error: {ex.Message}");
            return -1;
        }
        return toImport.Count;
    }

    /// <summary>
    /// Update post code of clients using postit API
    /// Unsuccessful requests are skiped and post code is not updated
    /// </summary>
    /// <param name="apiKey">api key</param>
    /// <returns>success</returns>
    public async Task<bool> UpdateClients(string apiKey)
    {
        try
        {
            var clients = await ClientRepo.ListAsync();
            foreach (var client in clients)
            {
                var response = await _httpClient.GetStringAsync($"?term={client.Address}&key={apiKey}");
                var result = JsonConvert.DeserializeObject<PostItResponse>(response);
                if (result == null || !result.Success) continue;

                var data = result.Data.FirstOrDefault();
                if (data == null) continue;

                client.UpdatePostCode(data.PostCode);
            }

            await ClientRepo.SaveChangesAsync();
        }
        catch(Exception ex)
        {
            //TODO Probably do somthing with exception later
            Console.WriteLine($"Error: {ex.Message}");
            return false;
        }
        return true;
    }

}
