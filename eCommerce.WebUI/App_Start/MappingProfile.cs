using AutoMapper;
using eCommerce.Model;
using eCommerce.Model.Dtos;

namespace eCommerce.WebUI
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Customer, CustomerDto>();
            CreateMap<CustomerDto, Customer>();
        }
    }
}