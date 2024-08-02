using AutoMapper;
using MovieStore.Data.UnitOfWork;
using MovieStore.Schema.Genre;

namespace MovieStore.Operations.GenreOperations.Commands;

public class UpdateGenreCommandHandler
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public GenreDto Model { get; set; }

    public UpdateGenreCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task Handle()
    {
        var genre = await _unitOfWork.GenreRepository.GetByIdAsync(Model.GenreId);

        if (genre == null)
        {
            throw new InvalidOperationException("Genre not found.");
        }

        _mapper.Map(Model, genre);

        _unitOfWork.GenreRepository.Update(genre);
        await _unitOfWork.CompleteAsync();
    }
}