using AutoMapper;
using MovieStore.Data.UnitOfWork;
using MovieStore.Schema.Director;

namespace MovieStore.Operations.DirectorOperations.Commands;

public class UpdateDirectorCommandHandler
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public UpdateDirectorDto Model { get; set; }

    public UpdateDirectorCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task Handle()
    {
        var existingDirector = await _unitOfWork.DirectorRepository.FirstOrDefaultAsync(x => x.DirectorId == Model.DirectorId);

        if (existingDirector == null)
        {
            throw new InvalidOperationException("Director not found.");
        }

        _mapper.Map(Model, existingDirector);
        _unitOfWork.DirectorRepository.Update(existingDirector);
        await _unitOfWork.CompleteAsync();
    }
}