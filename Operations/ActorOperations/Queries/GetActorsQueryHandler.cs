using AutoMapper;
using MovieStore.Data.UnitOfWork;
using MovieStore.Schema.Actor;

namespace MovieStore.Operations.ActorOperations.Queries;

public class GetActorsQueryHandler
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetActorsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<List<ActorDto>> Handle()
    {
        var actors = await _unitOfWork.ActorRepository.GetAllAsync("Movies");
        return _mapper.Map<List<ActorDto>>(actors);
    }
}