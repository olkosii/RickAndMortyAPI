using System;

namespace RickAndMortyAPI.DataModels
{
    public class CharacterResponse
    {
        public Info Info { get; set; }
        public Character[] Results { get; set; }
    }
}
