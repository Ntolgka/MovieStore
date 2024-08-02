using AutoMapper;
using MovieStore.Data.Domain;
using MovieStore.Data.UnitOfWork;
using MovieStore.Schema.Customer;

namespace MovieStore.Operations.CustomerOperations.Commands;

public class CreateCustomerCommandHandler
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public CreateCustomerDto Model { get; set; }

    public CreateCustomerCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task Handle()
    {
        // First map the Customer without genres
        var customer = _mapper.Map<Customer>(Model);

        if (Model.FavoriteGenreIds != null && Model.FavoriteGenreIds.Any())
        {
            // Retrieve existing genres from the database
            var genres = await _unitOfWork.GenreRepository
                .WhereAsync(g => Model.FavoriteGenreIds.Contains(g.Id));

            if (genres.Count != Model.FavoriteGenreIds.Count)
            {
                throw new InvalidOperationException("One or more favorite genres do not exist.");
            }

            // Assign the retrieved genres directly to the customer
            customer.FavoriteGenres = genres;
        }

        // Insert the customer into the database
        await _unitOfWork.CustomerRepository.InsertAsync(customer);
        await _unitOfWork.CompleteAsync();
    }
}
