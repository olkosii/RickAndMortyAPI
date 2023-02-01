using AutoMapper;
using RickAndMortyAPI.DataModels;
using RickAndMortyAPI.DomainModels;

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
