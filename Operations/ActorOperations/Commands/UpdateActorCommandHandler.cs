using AutoMapper;
using MovieStore.Data.UnitOfWork;
using MovieStore.Schema.Actor;

namespace MovieStore.Operations.ActorOperations.Commands;

public class UpdateActorCommandHandler
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public UpdateActorDto Model { get; set; }

    public UpdateActorCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task Handle()
    {
        var existingActor = await _unitOfWork.ActorRepository.FirstOrDefaultAsync(x => x.ActorId == Model.ActorId);

        if (existingActor == null)
        {
            throw new InvalidOperationException("Actor not found.");
        }

        _mapper.Map(Model, existingActor);
        _unitOfWork.ActorRepository.Update(existingActor);
        await _unitOfWork.CompleteAsync();
    }
}