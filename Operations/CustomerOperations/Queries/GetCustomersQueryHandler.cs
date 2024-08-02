using AutoMapper;
using MovieStore.Data.UnitOfWork;
using MovieStore.Schema.Customer;

namespace MovieStore.Operations.CustomerOperations.Queries;

public class GetCustomersQueryHandler
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetCustomersQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<List<CustomerDto>> Handle()
    {
        var customers = await _unitOfWork.CustomerRepository.GetAllAsync();
            
        var customerDtos = _mapper.Map<List<CustomerDto>>(customers);

        return customerDtos;
    }
}