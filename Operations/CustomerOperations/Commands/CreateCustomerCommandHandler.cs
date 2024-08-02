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
        var existingCustomer = await _unitOfWork.CustomerRepository.FirstOrDefaultAsync(
            x => x.IdentityNumber == Model.IdentityNumber
        );

        if (existingCustomer != null)
        {
            throw new InvalidOperationException("A customer with this identity number already exists.");
        }

        var customer = _mapper.Map<Customer>(Model);

        if (Model.FavoriteGenreIds != null && Model.FavoriteGenreIds.Any())
        {
            var genres = await _unitOfWork.GenreRepository.Where(g => Model.FavoriteGenreIds.Contains(g.Id));
            customer.FavoriteGenres = genres.ToList();
        }

        await _unitOfWork.CustomerRepository.InsertAsync(customer);
        await _unitOfWork.CompleteAsync();
    }
}
