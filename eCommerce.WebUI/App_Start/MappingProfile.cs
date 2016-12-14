using System;
using AutoMapper;
using eCommerce.Model;
using eCommerce.Model.Dtos;

namespace eCommerce.WebUI
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Map Domain Model to DTO
            CreateMap<Customer, CustomerDto>();
            CreateMap<Movie, MovieDto>().ForMember(dest => dest.GenreDto, opt => opt.MapFrom(src => src.Genre));
            CreateMap<Genre, GenreDto>();
            CreateMap<Rental, RentalDto>()
                .ForMember(dest => dest.CustomerDto, opt => opt.MapFrom(src => src.Customer))
                .ForMember(dest => dest.MovieDto, opt => opt.MapFrom(src => src.Movie));

            // Map DTO to Domain Model
            CreateMap<CustomerDto, Customer>();
            CreateMap<MovieDto, Movie>();
            CreateMap<GenreDto, Genre>();
            CreateMap<RentalDto, Rental>();
        }
    }
}
