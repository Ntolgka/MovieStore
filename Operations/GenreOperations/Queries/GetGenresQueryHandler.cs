using MovieStore.Data.Domain;
using MovieStore.Data.UnitOfWork;

namespace MovieStore.Operations.GenreOperations.Queries;

public class GetGenresQueryHandler
{
    private readonly IUnitOfWork _unitOfWork;

    public GetGenresQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<Genre>> Handle()
    {
        return await _unitOfWork.GenreRepository.GetAllAsync();
    }
}