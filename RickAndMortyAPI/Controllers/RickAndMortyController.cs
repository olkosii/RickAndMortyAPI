using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RickAndMortyAPI.DomainModels;
using RickAndMortyAPI.Repositories;
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
        [Route("[controller]/GetCharacterByName/{name}")]
        public async Task<IActionResult> GetCharacterByName([FromRoute] string name)
        {
            var result = await _repository.GetCharacterByName(name);

            if(result != null)
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
