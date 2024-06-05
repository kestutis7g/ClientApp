using AutoMapper;
using ClientApp.API.Models;
using ClientApp.Core.Aggregates.Client.Entities;
using ClientApp.Core.Common;

namespace ClientApp.API;

public class AutoMapperProfiles : Profile
{
    public AutoMapperProfiles()
    {
        CreateMap(typeof(IPaginatedCollection<>), typeof(PaginatedList<>));

        CreateMap<ClientEntity, ClientModel>().ReverseMap();

    }
}
