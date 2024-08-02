using MovieStore.Data.GenericRepository;
using MovieStore.Data.Domain;

namespace MovieStore.Data.UnitOfWork;

public interface IUnitOfWork
{
    Task Complete(); 
    Task CompleteWithTransaction();
    void Dispose();
    
    IGenericRepository<Movie> MovieRepository { get; }
    IGenericRepository<Actor> ActorRepository { get; }
    IGenericRepository<Director> DirectorRepository { get; }
    IGenericRepository<Customer> CustomerRepository { get; }
    IGenericRepository<Order> OrderRepository { get; }

}