using MovieStore.Data.UnitOfWork;

namespace MovieStore.Operations.CustomerOperations.Commands;

public class DeleteCustomerCommandHandler
{
    private readonly IUnitOfWork _unitOfWork;
    public int CustomerId { get; set; }

    public DeleteCustomerCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task Handle()
    {
        var customer = await _unitOfWork.CustomerRepository.FirstOrDefaultAsync(
            x => x.CustomerId == CustomerId
        );

        if (customer == null)
        {
            throw new InvalidOperationException("No customer found with this ID.");
        }

        _unitOfWork.CustomerRepository.Delete(customer);
        await _unitOfWork.CompleteAsync();
    }
}