using RickAndMortyAPI.DataModels;

namespace RickAndMortyAPI.DomainModels
{
    public class CharacterDto
    {
        public string Name { get; set; }
        public string Status { get; set; }
        public string Species { get; set; }
        public string Type { get; set; }
        public string Gender { get; set; }
        public CharacterOriginDto Origin { get; set; }
    }
}
