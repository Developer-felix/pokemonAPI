using AutoMapper;
using PocumanAPI.Dto;
using PocumanAPI.Models;

namespace PocumanAPI.Helper
{
    public class MpappingProfiles : Profile
    {
        public MpappingProfiles()
        {
            CreateMap<Pokemon, PokemonDto>();
            CreateMap<PokemonDto, Pokemon>();
            CreateMap<Category, CategoryDto>();
            CreateMap<Country, CountryDto>();
            CreateMap<Owner, OwnerDto>();
            CreateMap<Review, ReviewDto>();
            CreateMap<ReviewDto, Review>();
            CreateMap<Reviewer, ReviewerDto>();
            CreateMap<ReviewerDto, Reviewer>();
            
        }
    }
}
