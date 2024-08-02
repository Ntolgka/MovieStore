using MovieStore.Data.UnitOfWork;

namespace MovieStore.Operations.ActorOperations.Commands;

public class DeleteActorCommandHandler
{
    private readonly IUnitOfWork _unitOfWork;
    public int ActorId { get; set; }

    public DeleteActorCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task Handle()
    {
        var existingActor = await _unitOfWork.ActorRepository.FirstOrDefaultAsync(x => x.ActorId == ActorId);

        if (existingActor == null)
        {
            throw new InvalidOperationException("Actor not found.");
        }

        _unitOfWork.ActorRepository.Delete(existingActor);
        await _unitOfWork.CompleteAsync();
    }
}