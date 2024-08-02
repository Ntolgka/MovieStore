using AutoMapper;
using MovieStore.Data.Domain;
using MovieStore.Data.UnitOfWork;
using MovieStore.Schema.Actor;

namespace MovieStore.Operations.ActorOperations.Commands;

public class CreateActorCommandHandler
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public CreateActorDto Model { get; set; }

    public CreateActorCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task Handle()
    {
        var existingActor = await _unitOfWork.ActorRepository.FirstOrDefaultAsync(
            x => x.FirstName == Model.FirstName && x.LastName == Model.LastName
        );

        if (existingActor != null)
        {
            throw new InvalidOperationException("An actor with this name already exists.");
        }

        var actor = _mapper.Map<Actor>(Model);
        await _unitOfWork.ActorRepository.InsertAsync(actor);
        await _unitOfWork.CompleteAsync();
    }
}