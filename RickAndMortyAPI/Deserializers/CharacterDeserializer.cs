using RickAndMortyAPI.Models.DataModels;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace RickAndMortyAPI.Deserializers
{
    public static class CharacterDeserializer
    {
        public static async Task<Character> DeserializeToCharacter(HttpResponseMessage responseMessage, Func<string ,Task<CharacterOrigin>> characterOriginHandler)
        {
            if (responseMessage.IsSuccessStatusCode)
            {
                var result = JsonSerializer.Deserialize<Character>(await responseMessage.Content.ReadAsStringAsync(),
                    new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });

                result.Origin = result.Origin.Url != string.Empty ?
                    await characterOriginHandler(result.Origin.Url) :
                    result.Origin;

                return result;
            }

            return null;
        }

        public static async Task<Character> DeserializeToCharacterResponse(HttpResponseMessage responseMessage, Func<string, Task<CharacterOrigin>> characterOriginHandler)
        {
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseObject = JsonSerializer.Deserialize<CharacterResponses>(await responseMessage.Content.ReadAsStringAsync(),
                    new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });

                var result = new Character()
                {
                    Id = responseObject.Results[0].Id,
                    Name = responseObject.Results[0].Name,
                    Status = responseObject.Results[0].Status,
                    Species = responseObject.Results[0].Species,
                    Type = responseObject.Results[0].Type,
                    Gender = responseObject.Results[0].Gender,
                    Image = responseObject.Results[0].Image,
                };

                result.Origin = responseObject.Results[0].Origin.Url != string.Empty ?
                    await characterOriginHandler(responseObject.Results[0].Origin.Url) :
                    responseObject.Results[0].Origin;


                return result;
            }

            return null;
        }

        public static async Task<List<Character>> DeserializeToCharactersList(HttpResponseMessage responseMessage, Func<string, Task<CharacterOrigin>> characterOriginHandler)
        {
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseObject = JsonSerializer.Deserialize<CharacterResponses>(await responseMessage.Content.ReadAsStringAsync(),
                    new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });

                var resultList = new List<Character>(responseObject.Results);

                for (int i = 0; i < resultList.Count; i++)
                {
                    resultList[i].Origin = responseObject.Results[i].Origin.Url != string.Empty ?
                    await characterOriginHandler(responseObject.Results[i].Origin.Url) :
                    responseObject.Results[i].Origin;
                }

                return resultList;
            }

            return null;
        }
    }
}
