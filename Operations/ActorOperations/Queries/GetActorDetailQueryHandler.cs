using AutoMapper;
using MovieStore.Data.UnitOfWork;
using MovieStore.Schema.Actor;

namespace MovieStore.Operations.ActorOperations.Queries;

public class GetActorDetailQueryHandler
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public int ActorId { get; set; }

    public GetActorDetailQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<ActorDto?> Handle()
    {
        var actor = await _unitOfWork.ActorRepository.GetByIdAsync(ActorId, "Movies");

        if (actor == null)
        {
            throw new InvalidOperationException("Actor not found.");
        }

        return _mapper.Map<ActorDto>(actor);
    }
}