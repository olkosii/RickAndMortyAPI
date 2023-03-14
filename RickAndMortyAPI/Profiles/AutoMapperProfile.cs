using AutoMapper;
using RickAndMortyAPI.Models.DataModels;
using RickAndMortyAPI.Models.DomainModels;

namespace RickAndMortyAPI.Profiles
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Character, CharacterDto>().ReverseMap();
            CreateMap<CharacterOrigin, CharacterOriginDto>().ReverseMap();
        }
    }
}
