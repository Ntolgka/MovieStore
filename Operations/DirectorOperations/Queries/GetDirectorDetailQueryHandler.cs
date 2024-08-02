using MovieStore.Data.Domain;
using MovieStore.Data.UnitOfWork;

namespace MovieStore.Operations.DirectorOperations.Queries;

public class GetDirectorDetailQueryHandler
{
    private readonly IUnitOfWork _unitOfWork;
    public int DirectorId { get; set; }

    public GetDirectorDetailQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Director?> Handle()
    {
        return await _unitOfWork.DirectorRepository.GetByIdAsync(DirectorId, "Movies");
    }
}