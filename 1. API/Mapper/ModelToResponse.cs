using _1._API.Request;
using _3._Data;
using _3._Data.Models;
using AutoMapper;

namespace _1._API.Mapper;

public class ModelToResponse : Profile
{
    public ModelToResponse()
    {
        CreateMap<Client, ClientResponse>();
        CreateMap<Order, OrderResponse>();
    }
}