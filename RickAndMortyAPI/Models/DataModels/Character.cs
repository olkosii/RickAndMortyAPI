namespace RickAndMortyAPI.Models.DataModels
{
    public class Character
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Status { get; set; }
        public string Species { get; set; }
        public string Type { get; set; }
        public string Gender { get; set; }
        public string Image { get; set; }
        public string Url { get; set; }
        public string Created { get; set; }
        public CharacterOrigin Location { get; set; }
        public CharacterOrigin Origin { get; set; }
        public string[] Episode { get; set; }
    }
}
