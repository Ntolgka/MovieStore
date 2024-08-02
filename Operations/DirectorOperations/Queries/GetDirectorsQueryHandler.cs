using MovieStore.Data.Domain;
using MovieStore.Data.UnitOfWork;

namespace MovieStore.Operations.DirectorOperations.Queries;

public class GetDirectorsQueryHandler
{
    private readonly IUnitOfWork _unitOfWork;

    public GetDirectorsQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<List<Director>> Handle()
    {
        return await _unitOfWork.DirectorRepository.GetAllAsync("Movies");
    }
}