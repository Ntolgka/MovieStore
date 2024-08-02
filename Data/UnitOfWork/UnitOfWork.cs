using MovieStore.Data.Context;
using MovieStore.Data.GenericRepository;
using MovieStore.Data.Domain;

namespace MovieStore.Data.UnitOfWork;

public class UnitOfWork : IUnitOfWork, IDisposable
{
    private readonly AppDbContext dbContext;

    public IGenericRepository<Movie> MovieRepository { get; }
    public IGenericRepository<Actor> ActorRepository { get; }
    public IGenericRepository<Director> DirectorRepository { get; }
    public IGenericRepository<Customer> CustomerRepository { get; }
    public IGenericRepository<Order> OrderRepository { get; }
    public IGenericRepository<Genre> GenreRepository { get; }

    public UnitOfWork(AppDbContext dbContext)
    {
        this.dbContext = dbContext;

        MovieRepository = new GenericRepository<Movie>(this.dbContext);
        ActorRepository = new GenericRepository<Actor>(this.dbContext);
        DirectorRepository = new GenericRepository<Director>(this.dbContext);
        CustomerRepository = new GenericRepository<Customer>(this.dbContext);
        OrderRepository = new GenericRepository<Order>(this.dbContext);
    }

    public void Dispose()
    {
        dbContext?.Dispose();
    }

    public async Task CompleteAsync()   
    {
        await dbContext.SaveChangesAsync();
    }

    public async Task CompleteWithTransaction()
    {
        using (var dbTransaction = await dbContext.Database.BeginTransactionAsync())
        {
            try
            {
                await dbContext.SaveChangesAsync();
                await dbTransaction.CommitAsync();
            }
            catch (Exception ex)
            {
                await dbTransaction.RollbackAsync();
                Console.WriteLine(ex);
                throw;
            }
        }
    }
}