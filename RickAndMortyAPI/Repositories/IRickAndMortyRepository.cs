using RickAndMortyAPI.DataModels;
using RickAndMortyAPI.DomainModels;
using System.Threading.Tasks;

namespace RickAndMortyAPI.Repositories
{
    public interface IRickAndMortyRepository
    {
        Task<Character> GetCharacterByName(string name);
        Task<bool> CheckCharacterByEpisode(CheckCharacter character);
        Task<bool> CheckCharacterAndEpisodeExistenceByNames(string characterName, string episodeName);
    }
}
