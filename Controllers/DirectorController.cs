using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MovieStore.Data.Domain;
using MovieStore.Data.UnitOfWork;
using MovieStore.Schema.Director;

namespace MovieStore.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DirectorController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public DirectorController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    // GET: api/director
    [HttpGet]
    public async Task<ActionResult<List<DirectorDto>>> GetDirectors()
    {
        var directors = await _unitOfWork.DirectorRepository.GetAllAsync("Movies");
        var directorDtos = _mapper.Map<List<DirectorDto>>(directors);
        return Ok(directorDtos);
    }

    // GET: api/director/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<DirectorDto>> GetDirectorDetail(int id)
    {
        var director = await _unitOfWork.DirectorRepository.GetByIdAsync(id, "Movies");

        if (director == null)
        {
            return NotFound();
        }

        var directorDto = _mapper.Map<DirectorDto>(director);
        return Ok(directorDto);
    }

    // POST: api/director
    [HttpPost]
    public async Task<ActionResult> CreateDirector([FromBody] CreateDirectorDto createDirectorDto)
    {
        var existingDirector = await _unitOfWork.DirectorRepository.FirstOrDefaultAsync(
            x => x.FirstName == createDirectorDto.FirstName && x.LastName == createDirectorDto.LastName
        );

        if (existingDirector != null)
        {
            return Conflict("A director with this name already exists.");
        }

        var director = _mapper.Map<Director>(createDirectorDto);
        await _unitOfWork.DirectorRepository.InsertAsync(director);
        await _unitOfWork.CompleteAsync();

        return Ok(createDirectorDto);
    }

    // PUT: api/director/{id}
    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateDirector(int id, [FromBody] UpdateDirectorDto updateDirectorDto)
    {
        if (id != updateDirectorDto.DirectorId)
        {
            return BadRequest("Director ID mismatch.");
        }

        var existingDirector = await _unitOfWork.DirectorRepository.FirstOrDefaultAsync(x => x.DirectorId == id);

        if (existingDirector == null)
        {
            return NotFound("Director not found.");
        }

        _mapper.Map(updateDirectorDto, existingDirector);
        _unitOfWork.DirectorRepository.Update(existingDirector);
        await _unitOfWork.CompleteAsync();

        return NoContent();
    }

    // DELETE: api/director/{id}
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteDirector(int id)
    {
        var existingDirector = await _unitOfWork.DirectorRepository.FirstOrDefaultAsync(x => x.DirectorId == id);

        if (existingDirector == null)
        {
            return NotFound("Director not found.");
        }

        _unitOfWork.DirectorRepository.Delete(existingDirector);
        await _unitOfWork.CompleteAsync();

        return NoContent();
    }
}