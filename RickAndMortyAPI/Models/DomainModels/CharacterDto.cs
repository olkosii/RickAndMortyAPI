
namespace RickAndMortyAPI.Models.DomainModels
{
    public class CharacterDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Status { get; set; }
        public string Species { get; set; }
        public string Type { get; set; }
        public string Gender { get; set; }
        public string Image { get; set; }

        public CharacterOriginDto Origin { get; set; }
    }
}
