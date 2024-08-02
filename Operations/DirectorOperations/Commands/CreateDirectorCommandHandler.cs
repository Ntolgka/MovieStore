using AutoMapper;
using MovieStore.Data.Domain;
using MovieStore.Data.UnitOfWork;
using MovieStore.Schema.Director;

namespace MovieStore.Operations.DirectorOperations.Commands;

public class CreateDirectorCommandHandler
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public CreateDirectorDto Model { get; set; }

    public CreateDirectorCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task Handle()
    {
        var existingDirector = await _unitOfWork.DirectorRepository.FirstOrDefaultAsync(
            x => x.FirstName == Model.FirstName && x.LastName == Model.LastName
        );

        if (existingDirector != null)
        {
            throw new InvalidOperationException("A director with this name already exists.");
        }

        var director = _mapper.Map<Director>(Model);
        await _unitOfWork.DirectorRepository.InsertAsync(director);
        await _unitOfWork.CompleteAsync();
    }
}