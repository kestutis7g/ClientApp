using ClientApp.Core.Aggregates.Client.Entities;
using ClientApp.Core.Aggregates.Client.Specs;
using ClientApp.Core.Interfaces;
using ClientApp.Core.Services;
using ClientApp.Shared.Interfaces;

namespace ClientApp.UnitTests;

public class ClientServiceTests
{
    private readonly IClientService clientService;
    private readonly HttpClient _httpClient;
    private readonly IRepository<ClientEntity> clientRepo = Substitute.For<IRepository<ClientEntity>>();
    public ClientServiceTests()
    {
        _httpClient = new HttpClient
        {
            BaseAddress = new Uri("https://api.postit.lt/")
        };
        clientService = new ClientService(clientRepo, _httpClient);
    }

    [Fact]
    public async Task GetClients_ShouldReturnClientList()
    {
        //Arrange
        var clientId = Guid.NewGuid();
        var name = "Gintarinė vaistinė";
        var address = "Baltų pr. 7A-1, Kaunas";
        var postCode = "12345";
        var entity = new ClientEntity { Id = clientId, Name = name, Address = address, PostCode = postCode };
        List<ClientEntity> list = new List<ClientEntity>();
        list.Add(entity);
        clientRepo.ListAsync().Returns(list);
        //Act
        var result = await clientService.GetClientList();
        //Assert
        Assert.NotNull(result);
        Assert.NotNull(result.FirstOrDefault());
        Assert.Equal(clientId, result.First().Id);
        Assert.Equal("Gintarinė vaistinė", result.First().Name);
        Assert.Equal("Baltų pr. 7A-1, Kaunas", result.First().Address);
        Assert.Equal("12345", result.First().PostCode);
    }

    [Fact]
    public async Task ImportClients_ShouldReturnZero_WhenClientExist()
    {
        //Arrange
        var clientId = Guid.NewGuid();
        var name = "Gintarinė vaistinė";
        var address = "Baltų pr. 7A-1, Kaunas";
        var postCode = "12345";
        var entity = new ClientEntity { Id = clientId, Name = name, Address = address, PostCode = postCode };
        List<ClientEntity> list = new List<ClientEntity>();
        list.Add(entity);
        clientRepo.FirstOrDefaultAsync(Arg.Any<GetClientByParamsSpec>()).Returns(entity);
        //Act
        var result = await clientService.ImportClients(list);
        //Assert
        Assert.Equal(0, result);
    }

    [Fact]
    public async Task ImportClients_ShouldReturnOne_WhenClientNotExist()
    {
        //Arrange
        var clientId = Guid.NewGuid();
        var name = "Gintarinė vaistinė";
        var address = "Baltų pr. 7A-1, Kaunas";
        var postCode = "12345";
        var entity = new ClientEntity { Id = clientId, Name = name, Address = address, PostCode = postCode };
        List<ClientEntity> list = new List<ClientEntity>();
        list.Add(entity);
        //Act
        var result = await clientService.ImportClients(list);
        //Assert
        Assert.Equal(1, result);
    }
    
    [Fact]
    public async Task UpdateClients_ShouldReturnTrue_WhenKeyIsValid()
    {
        //Arrange
        var clientId = Guid.NewGuid();
        var name = "Gintarinė vaistinė";
        var address = "Baltų pr. 7A-1, Kaunas";
        var postCode = "";
        var key = "postit.lt-examplekey";
        var entity = new ClientEntity { Id = clientId, Name = name, Address = address, PostCode = postCode };
        List<ClientEntity> list = new List<ClientEntity>();
        list.Add(entity);
        clientRepo.ListAsync().Returns(list);
        //Act
        var result = await clientService.UpdateClients(key);
        //Assert
        Assert.True(result);
    }
}