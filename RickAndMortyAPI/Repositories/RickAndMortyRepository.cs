using System;
using RickAndMortyAPI.RickAndMortyAPI;
using System.Net.Http;
using System.Threading.Tasks;
using RickAndMortyAPI.DomainModels;
using RickAndMortyAPI.DataModels;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace RickAndMortyAPI.Repositories
{
    public class RickAndMortyRepository : IRickAndMortyRepository
    {
        private static readonly HttpClient httpClient;

        static RickAndMortyRepository()
        {
            httpClient  = new HttpClient();
        }

        public async Task<bool> CheckCharacterByEpisode(CheckCharacter character)
        {
            var url = string.Format(RickAndMortyApi.Characters + $"?name={character.CharacterName.ToLower().Trim(' ')}");
            var response = await httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var responseObject = JsonSerializer.Deserialize<CharacterResponse>(await response.Content.ReadAsStringAsync(),
                    new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });

                var episodeUrl = await GetEpisodeUrlByName(character.EpisodeName);

                if(responseObject.Results[0].Episode.Contains(episodeUrl))
                  return true;
            }
            
            return false;
        }

        public async Task<Character> GetCharacterByName(string name)
        {
            var url = string.Format(RickAndMortyApi.Characters + $"?name={name.ToLower().Trim()}");
            var response = await httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var responseObject = JsonSerializer.Deserialize<CharacterResponse>(await response.Content.ReadAsStringAsync(),
                    new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });

                var result = new Character()
                {
                    Name = responseObject.Results[0].Name,
                    Status = responseObject.Results[0].Status,
                    Species = responseObject.Results[0].Species,
                    Type = responseObject.Results[0].Type,
                    Gender = responseObject.Results[0].Gender
                };

                result.Origin = responseObject.Results[0].Origin.Url != string.Empty ?
                    await GetCharacterOriginInfo(responseObject.Results[0].Origin.Url) :
                    responseObject.Results[0].Origin;


                return result;
            }

            return null;
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
                var responseObject = JsonSerializer.Deserialize<EpisodeResponse>(await response.Content.ReadAsStringAsync(),
                    new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });

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
                var responseObject = JsonSerializer.Deserialize<CharacterOrigin>(await response.Content.ReadAsStringAsync(),
                    new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });

                return responseObject;
            }

            return null;
        }
    }
}
