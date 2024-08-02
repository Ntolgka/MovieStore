using AutoMapper;
using MovieStore.Data.Domain;
using MovieStore.Data.UnitOfWork;
using MovieStore.Schema.Genre;

namespace MovieStore.Operations.GenreOperations.Commands;

public class CreateGenreCommandHandler
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public CreateGenreDto Model { get; set; }

    public CreateGenreCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Genre> Handle()
    {
        var existingGenre = await _unitOfWork.GenreRepository.FirstOrDefaultAsync(
            x => x.Name == Model.Name
        );

        if (existingGenre != null)
        {
            throw new InvalidOperationException("A genre with this name already exists.");
        }

        var genre = _mapper.Map<Genre>(Model);
        await _unitOfWork.GenreRepository.InsertAsync(genre);
        await _unitOfWork.CompleteAsync();

        return genre;
    }
}