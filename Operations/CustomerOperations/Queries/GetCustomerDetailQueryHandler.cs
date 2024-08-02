using AutoMapper;
using MovieStore.Data.UnitOfWork;
using MovieStore.Schema.Customer;

namespace MovieStore.Operations.CustomerOperations.Queries;

public class GetCustomerDetailQueryHandler
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public int CustomerId { get; set; }

    public GetCustomerDetailQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<CustomerDto> Handle()
    {
        var customer = await _unitOfWork.CustomerRepository.FirstOrDefaultAsync(
            x => x.CustomerId == CustomerId
        );

        if (customer == null)
        {
            throw new InvalidOperationException("No customer found with this ID.");
        }

        var customerDto = _mapper.Map<CustomerDto>(customer);

        return customerDto;
    }
}