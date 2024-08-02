using MovieStore.Data.UnitOfWork;

namespace MovieStore.Operations.GenreOperations.Commands;

public class DeleteGenreCommandHandler
{
    private readonly IUnitOfWork _unitOfWork;
    public int Id { get; set; }

    public DeleteGenreCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task Handle()
    {
        var genre = await _unitOfWork.GenreRepository.GetByIdAsync(Id);

        if (genre == null)
        {
            throw new InvalidOperationException("Genre not found.");
        }

        _unitOfWork.GenreRepository.Delete(genre);
        await _unitOfWork.CompleteAsync();
    }
}