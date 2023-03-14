using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RickAndMortyAPI.Models.DomainModels;
using RickAndMortyAPI.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RickAndMortyAPI.Controllers
{
    public class RickAndMortyController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IRickAndMortyRepository _repository;

        public RickAndMortyController(IRickAndMortyRepository repository, IMapper mapper)
        {
            _mapper = mapper;
            _repository = repository;
        }

        [HttpGet]
        [Route("[controller]/GetCharacters")]
        public async Task<IActionResult> GetCharacters()
        {
            var result = await _repository.GetCharacters();

            if (result != null)
                return Ok(_mapper.Map<List<CharacterDto>>(result));

            return NotFound();
        }

        [HttpGet]
        [Route("[controller]/GetOneCharacterByName/{name}")]
        public async Task<IActionResult> GetOneCharacterByName([FromRoute] string name)
        {
            var result = await _repository.GetCharacterByName(name);

            if(result != null)
                return Ok(_mapper.Map<CharacterDto>(result));

            return NotFound();
        }

        [HttpGet]
        [Route("[controller]/GetSimilarCharactersByName/{name}")]
        public async Task<IActionResult> GetSimilarCharactersByName([FromRoute] string name)
        {
            var result = await _repository.GetSimilarCharactersByName(name);

            if (result != null)
                return Ok(_mapper.Map<List<CharacterDto>>(result));

            return NotFound();
        }

        [HttpGet]
        [Route("[controller]/GetCharacterById/{id}")]
        public async Task<IActionResult> GetCharacterById([FromRoute] int id)
        {
            var result = await _repository.GetCharacterById(id);

            if (result != null)
                return Ok(_mapper.Map<CharacterDto>(result));

            return NotFound();
        }

        [HttpPost]
        [Route("[controller]/CheckPerson")]
        public async Task<IActionResult> CheckCharacter([FromBody] CheckCharacter character)
        {
            var result = await _repository.CheckCharacterByEpisode(character);

            if (!await _repository.CheckCharacterAndEpisodeExistenceByNames(character.CharacterName, character.EpisodeName))
                return NotFound();

            return Ok(result);
        }
    }
}
