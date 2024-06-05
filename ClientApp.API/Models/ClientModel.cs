using System.ComponentModel.DataAnnotations;

namespace ClientApp.API.Models;
public class ClientModel
{
    public Guid? Id { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }
    public string PostCode { get; set; }

    public ClientModel() 
    {
        Name = string.Empty;
        Address = string.Empty;
        PostCode = string.Empty;
    }
}