namespace RickAndMortyAPI.Models.DataModels
{
    public class Episode
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string AirDate { get; set; }
        public string EpisodeCode { get; set; }
        public string[] Characters { get; set; }
        public string Url { get; set; }
        public string Created { get; set; }
    }
}
