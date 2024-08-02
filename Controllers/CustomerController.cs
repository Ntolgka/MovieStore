using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MovieStore.Data.Domain;
using MovieStore.Data.UnitOfWork;
using MovieStore.Operations.CustomerOperations.Commands;
using MovieStore.Schema.Customer;

namespace MovieStore.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CustomerController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CustomerController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    // GET: api/customer
    [HttpGet]
    public async Task<ActionResult<List<CustomerDto>>> GetCustomers()
    {
        var customers = await _unitOfWork.CustomerRepository.GetAllAsync("FavoriteGenres");
        var customerDtos = _mapper.Map<List<CustomerDto>>(customers);
        return Ok(customerDtos);
    }

    // GET: api/customer/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<CustomerDto>> GetCustomerDetail(int id)
    {
        var customer = await _unitOfWork.CustomerRepository.GetByIdAsync(id, "FavoriteGenres");

        if (customer == null)
        {
            return NotFound();
        }

        var customerDto = _mapper.Map<CustomerDto>(customer);
        return Ok(customerDto);
    }

    // POST: api/customer
    [HttpPost]
    public async Task<ActionResult> CreateCustomer([FromBody] CreateCustomerDto createCustomerDto)
    {
        var existingCustomer = await _unitOfWork.CustomerRepository.FirstOrDefaultAsync(
            x => x.IdentityNumber == createCustomerDto.IdentityNumber
        );

        if (existingCustomer != null)
        {
            return Conflict("A customer with this identity number already exists.");
        }

        var genres = await _unitOfWork.GenreRepository.WhereAsync(g => createCustomerDto.FavoriteGenreIds.Contains(g.Id));
    
        if (genres.Count != createCustomerDto.FavoriteGenreIds.Count)
        {
            return BadRequest("One or more favorite genres do not exist.");
        }
        
        var customer = _mapper.Map<Customer>(createCustomerDto);
        customer.FavoriteGenres = genres;

        await _unitOfWork.CustomerRepository.InsertAsync(customer);
        await _unitOfWork.CompleteAsync();

        return Ok(createCustomerDto);
    }

    // PUT: api/customer/{id}
    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateCustomer(int id, [FromBody] UpdateCustomerDto updateCustomerDto)
    {
        if (id != updateCustomerDto.CustomerId)
        {
            return BadRequest("Customer ID mismatch.");
        }

        var existingCustomer = await _unitOfWork.CustomerRepository.FirstOrDefaultAsync(x => x.CustomerId == id);

        if (existingCustomer == null)
        {
            return NotFound("Customer not found.");
        }

        _mapper.Map(updateCustomerDto, existingCustomer);
        _unitOfWork.CustomerRepository.Update(existingCustomer);
        await _unitOfWork.CompleteAsync();

        return NoContent();
    }

    // DELETE: api/customer/{id}
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteCustomer(int id)
    {
        var existingCustomer = await _unitOfWork.CustomerRepository.FirstOrDefaultAsync(x => x.CustomerId == id);

        if (existingCustomer == null)
        {
            return NotFound("Customer not found.");
        }

        _unitOfWork.CustomerRepository.Delete(existingCustomer);
        await _unitOfWork.CompleteAsync();

        return NoContent();
    }
}