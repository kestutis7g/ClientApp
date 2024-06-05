using ClientApp.API.Models;
using ClientApp.Core.Aggregates.Client.Entities;
using ClientApp.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ClientApp.API.Controllers;

[ApiController]
[Route("api/client")]
public class ClientController : BaseController
{
    public ClientController(IClientService clientService)
    {
        ClientService = clientService ?? throw new ArgumentNullException(nameof(clientService));
    }

    private IClientService ClientService { get; }

    // GET api/client
    [HttpGet]
    public async Task<ActionResult> GetClients()
    {
        var result = await ClientService.GetClientList();
        return Ok(result);
    }

    // POST api/client/import
    [HttpPost("import")]
    public async Task<IActionResult> ImportClients(IFormFile file)
    {
        if (file == null || file.Length == 0)
        {
            return BadRequest("File not provided or is empty.");
        }
        
        if (Path.GetExtension(file.FileName).ToLower() != ".json")
        {
            return BadRequest("Only .json files are allowed.");
        }

        using var stream = new StreamReader(file.OpenReadStream());
        var json = await stream.ReadToEndAsync();
        var clients = JsonConvert.DeserializeObject<List<ClientModel>>(json);
        if(clients == null || 
            (clients.FirstOrDefault() != null && clients.First().Name == null)) 
        {
            return BadRequest(clients);
        }
        var result = await ClientService.ImportClients(Mapper.Map<List<ClientEntity>>(clients));
        if (result != -1)
        {
            return Ok(result);
        }
        else
        {
            return BadRequest(result);
        }
    }

    // POST api/client/import
    [HttpPost("update-postcodes")]
    public async Task<IActionResult> UpdatePostCodes()
    {
        var apiKey = Configuration["PostitApiKey"] ?? string.Empty;

        var result = await ClientService.UpdateClients(apiKey);

        if (result)
        {
            return Ok();
        }
        else
        {
            return BadRequest(result);
        }
    }
}
