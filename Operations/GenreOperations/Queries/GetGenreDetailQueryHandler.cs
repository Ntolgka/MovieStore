using MovieStore.Data.Domain;
using MovieStore.Data.UnitOfWork;

namespace MovieStore.Operations.GenreOperations.Queries;

public class GetGenreDetailQueryHandler
{
    private readonly IUnitOfWork _unitOfWork;
    public int GenreId { get; set; }

    public GetGenreDetailQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Genre> Handle()
    {
        var genre = await _unitOfWork.GenreRepository.GetByIdAsync(GenreId);

        if (genre == null)
        {
            throw new InvalidOperationException("Genre not found.");
        }

        return genre;
    }
}