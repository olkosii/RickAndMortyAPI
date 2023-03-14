using RickAndMortyAPI.Models.DataModels;
using RickAndMortyAPI.Models.DomainModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RickAndMortyAPI.Repositories
{
    public interface IRickAndMortyRepository
    {
        Task<List<Character>> GetCharacters();
        Task<Character> GetCharacterById(int id);
        Task<Character> GetCharacterByName(string name);
        Task<List<Character>> GetSimilarCharactersByName(string name);
        Task<bool> CheckCharacterByEpisode(CheckCharacter character);
        Task<bool> CheckCharacterAndEpisodeExistenceByNames(string characterName, string episodeName);
    }
}
