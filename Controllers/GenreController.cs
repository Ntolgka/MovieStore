using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MovieStore.Data.Domain;
using MovieStore.Data.UnitOfWork;
using MovieStore.Operations.GenreOperations.Commands;
using MovieStore.Schema.Genre;

namespace MovieStore.Controllers;

[Route("api/[controller]")]
[ApiController]
public class GenreController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GenreController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    
    // GET: api/genre
    [HttpGet]
    public async Task<IActionResult> GetAllGenres()
    {
        var genres = await _unitOfWork.GenreRepository.GetAllAsync();
        var genreDtos = _mapper.Map<IEnumerable<GenreDto>>(genres);
        return Ok(genreDtos);
    }
    
    // GET: api/genre/{id}
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetGenreById(int id)
    {
        var genre = await _unitOfWork.GenreRepository.GetByIdAsync(id);

        if (genre == null)
        {
            return NotFound(new { Message = "Genre not found." });
        }

        var genreDto = _mapper.Map<GenreDto>(genre);
        return Ok(genreDto);
    }

    // POST: api/genre
    [HttpPost]
    public async Task<ActionResult> CreateGenre([FromBody] CreateGenreDto createGenreDto)
    {
        if (string.IsNullOrWhiteSpace(createGenreDto.Name))
        {
            return BadRequest("Genre name is required.");
        }

        var genre = _mapper.Map<Genre>(createGenreDto);
        await _unitOfWork.GenreRepository.InsertAsync(genre);
        await _unitOfWork.CompleteAsync();

        return Ok(genre);
    }

    // PUT: api/genre/{id}
    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateGenre(int id, [FromBody] GenreDto updateGenreDto)
    {
        if (updateGenreDto == null || id != updateGenreDto.GenreId)
        {
            return BadRequest("Invalid genre data or ID mismatch.");
        }

        var command = new UpdateGenreCommandHandler(_unitOfWork, _mapper)
        {
            Model = updateGenreDto
        };

        try
        {
            await command.Handle();
            return NoContent();
        }
        catch (InvalidOperationException ex)
        {
            return NotFound(new { Message = ex.Message });
        }
    }

    // DELETE: api/genre/{id}
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteGenre(int id)
    {
        var command = new DeleteGenreCommandHandler(_unitOfWork)
        {
            Id = id
        };

        try
        {
            await command.Handle();
            return NoContent();
        }
        catch (InvalidOperationException ex)
        {
            return NotFound(new { Message = ex.Message });
        }
    }
}