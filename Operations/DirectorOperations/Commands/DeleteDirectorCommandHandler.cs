using MovieStore.Data.UnitOfWork;

namespace MovieStore.Operations.DirectorOperations.Commands;

public class DeleteDirectorCommandHandler
{
    private readonly IUnitOfWork _unitOfWork;
    public int DirectorId { get; set; }

    public DeleteDirectorCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task Handle()
    {
        var existingDirector = await _unitOfWork.DirectorRepository.FirstOrDefaultAsync(x => x.DirectorId == DirectorId);

        if (existingDirector == null)
        {
            throw new InvalidOperationException("Director not found.");
        }

        _unitOfWork.DirectorRepository.Delete(existingDirector);
        await _unitOfWork.CompleteAsync();
    }
}