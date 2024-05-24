using _1._API.Request;
using _3._Data;
using _3._Data.Models;
using AutoMapper;

namespace _1._API.Mapper;

public class ModelToRequest : Profile
{
    public ModelToRequest()
    {
        CreateMap<Client,ClientRequest>();
        CreateMap<Order,OrderRequest>();
    }
}