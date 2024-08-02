using AutoMapper;
using MovieStore.Data.UnitOfWork;
using MovieStore.Schema.Customer;

namespace MovieStore.Operations.CustomerOperations.Commands;

public class UpdateCustomerCommandHandler
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public UpdateCustomerDto Model { get; set; }

    public UpdateCustomerCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task Handle()
    {
        var existingCustomer = await _unitOfWork.CustomerRepository.FirstOrDefaultAsync(
            x => x.CustomerId == Model.CustomerId
        );

        if (existingCustomer == null)
        {
            throw new InvalidOperationException("Customer not found.");
        }

        if (Model.FavoriteGenreIds != null && Model.FavoriteGenreIds.Any())
        {
            var genres = await _unitOfWork.GenreRepository.Where(g => Model.FavoriteGenreIds.Contains(g.Id));
            existingCustomer.FavoriteGenres = genres.ToList();
        }
        else
        {
            existingCustomer.FavoriteGenres.Clear();
        }

        _mapper.Map(Model, existingCustomer);
        _unitOfWork.CustomerRepository.Update(existingCustomer);
        await _unitOfWork.CompleteAsync();
    }
}