using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MovieStore.Data.Domain;
using MovieStore.Data.UnitOfWork;
using MovieStore.Schema.Actor;

namespace MovieStore.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ActorController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public ActorController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    // GET: api/actor
    [HttpGet]
    public async Task<ActionResult<List<ActorDto>>> GetActors()
    {
        var actors = await _unitOfWork.ActorRepository.GetAllAsync("Movies");
        var actorDtos = _mapper.Map<List<ActorDto>>(actors);
        return Ok(actorDtos);
    }

    // GET: api/actor/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<ActorDto>> GetActorDetail(int id)
    {
        var actor = await _unitOfWork.ActorRepository.GetByIdAsync(id, "Movies");

        if (actor == null)
        {
            return NotFound();
        }

        var actorDto = _mapper.Map<ActorDto>(actor);
        return Ok(actorDto);
    }

    // POST: api/actor
    [HttpPost]
    public async Task<ActionResult> CreateActor([FromBody] CreateActorDto createActorDto)
    {
        var existingActor = await _unitOfWork.ActorRepository.FirstOrDefaultAsync(
            x => x.FirstName == createActorDto.FirstName && x.LastName == createActorDto.LastName
        );

        if (existingActor != null)
        {
            return Conflict("An actor with this name already exists.");
        }

        var actor = _mapper.Map<Actor>(createActorDto);
        await _unitOfWork.ActorRepository.InsertAsync(actor);
        await _unitOfWork.CompleteAsync();

        return Ok(createActorDto);
    }

    // PUT: api/actor/{id}
    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateActor(int id, [FromBody] UpdateActorDto updateActorDto)
    {
        if (id != updateActorDto.ActorId)
        {
            return BadRequest("Actor ID mismatch.");
        }

        var existingActor = await _unitOfWork.ActorRepository.FirstOrDefaultAsync(x => x.ActorId == id);

        if (existingActor == null)
        {
            return NotFound("Actor not found.");
        }

        _mapper.Map(updateActorDto, existingActor);
        _unitOfWork.ActorRepository.Update(existingActor);
        await _unitOfWork.CompleteAsync();

        return NoContent();
    }

    // DELETE: api/actor/{id}
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteActor(int id)
    {
        var existingActor = await _unitOfWork.ActorRepository.FirstOrDefaultAsync(x => x.ActorId == id);

        if (existingActor == null)
        {
            return NotFound("Actor not found.");
        }

        _unitOfWork.ActorRepository.Delete(existingActor);
        await _unitOfWork.CompleteAsync();

        return NoContent();
    }
}