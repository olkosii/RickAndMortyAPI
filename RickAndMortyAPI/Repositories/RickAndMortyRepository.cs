using System;
using RickAndMortyAPI.RickAndMortyAPI;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Xml.Linq;
using RickAndMortyAPI.Models.DataModels;
using RickAndMortyAPI.Models.DomainModels;
using RickAndMortyAPI.Deserializers;

namespace RickAndMortyAPI.Repositories
{
    public class RickAndMortyRepository : IRickAndMortyRepository
    {
        private static readonly HttpClient httpClient;

        static RickAndMortyRepository()
        {
            httpClient  = new HttpClient();
        }

        //Returns first 20 characters from first page
        public async Task<List<Character>> GetCharacters()
        {
            var url = string.Format(RickAndMortyApi.Characters);

            var response = await httpClient.GetAsync(url);

            return await CharacterDeserializer.DeserializeToCharactersList(response, GetCharacterOriginInfo);
        }

        //Returns the most appropriate character by name
        public async Task<Character> GetCharacterByName(string name)
        {
            var url = string.Format(RickAndMortyApi.Characters + $"?name={name.ToLower().Trim()}");

            var response = await httpClient.GetAsync(url);

            return await CharacterDeserializer.DeserializeToCharacterResponse(response, GetCharacterOriginInfo);
        }

        //Returns one/few characters with similar name
        public async Task<List<Character>> GetSimilarCharactersByName(string name)
        {
            var url = string.Format(RickAndMortyApi.Characters + $"?name={name.ToLower().Trim()}");

            var response = await httpClient.GetAsync(url);

            return await CharacterDeserializer.DeserializeToCharactersList(response, GetCharacterOriginInfo);
        }

        //Returns one character by Id
        public async Task<Character> GetCharacterById(int id)
        {
            var url = string.Format(RickAndMortyApi.Characters + id);

            var response = await httpClient.GetAsync(url);

            return await CharacterDeserializer.DeserializeToCharacter(response,GetCharacterOriginInfo);
        }

        public async Task<bool> CheckCharacterByEpisode(CheckCharacter character)
        {
            var url = string.Format(RickAndMortyApi.Characters + $"?name={character.CharacterName.ToLower().Trim(' ')}");
            var response = await httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var responseObject = await CharacterDeserializer.Deserialize<CharacterResponses>(response);

                var episodeUrl = await GetEpisodeUrlByName(character.EpisodeName);

                if(responseObject.Results[0].Episode.Contains(episodeUrl))
                  return true;
            }
            
            return false;
        }

        public async Task<bool> CheckCharacterAndEpisodeExistenceByNames(string characterName, string episodeName)
        {
            var character = await GetCharacterByName(characterName);
            var episodeUrl = await GetEpisodeUrlByName(episodeName);

            if(character == null || episodeUrl == null)
                return false;

            return true;
        }


        private async Task<string> GetEpisodeUrlByName(string name)
        {
            var url = string.Format(RickAndMortyApi.Episodes + $"?name={name.ToLower().Trim()}");
            var response = await httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var responseObject = await CharacterDeserializer.Deserialize<EpisodeResponse>(response);

                var result = responseObject.Results[0].Url;

                return result;
            }

            return null;
        }
        private async Task<CharacterOrigin> GetCharacterOriginInfo(string url)
        {
            var response = await httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var responseObject = await CharacterDeserializer.Deserialize<CharacterOrigin>(response);

                return responseObject;
            }

            return null;
        }
    }
}
