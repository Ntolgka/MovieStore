using AutoMapper;
using MovieStore.Data.Domain;
using MovieStore.Schema.Actor;
using MovieStore.Schema.Customer;
using MovieStore.Schema.Director;
using MovieStore.Schema.Genre;
using MovieStore.Schema.Movie;
using MovieStore.Schema.Order;

namespace MovieStore.Mapper;

public class MapperConfig : Profile
{
    public MapperConfig()
    {
        // Customer
        CreateMap<Customer, CustomerDto>()
            .ForMember(dest => dest.FavoriteGenreIds, 
                opt => opt.MapFrom(src => src.FavoriteGenres != null ? src.FavoriteGenres.Select(g => g.Id).ToList() : new List<int>()));

        CreateMap<CreateCustomerDto, Customer>()
            .ForMember(dest => dest.FavoriteGenres, 
                opt => opt.MapFrom(src => src.FavoriteGenreIds != null ? src.FavoriteGenreIds.Select(id => new Genre { Id = id }).ToList() : new List<Genre>()));
        
        CreateMap<UpdateCustomerDto, Customer>()
            .ForMember(dest => dest.FavoriteGenres, 
                opt => opt.MapFrom(src => src.FavoriteGenreIds != null ? src.FavoriteGenreIds.Select(id => new Genre { Id = id }).ToList() : new List<Genre>()));
        
        // Genre
        CreateMap<Genre, GenreDto>();
        
        // Actor
        CreateMap<Actor, ActorDto>();
        CreateMap<CreateActorDto, Actor>();
        CreateMap<UpdateActorDto, Actor>();

        // Director
        CreateMap<Director, DirectorDto>();
        CreateMap<CreateDirectorDto, Director>();
        CreateMap<UpdateDirectorDto, Director>();

        // Movie
        CreateMap<Movie, MovieDto>()
            .ForMember(dest => dest.Actors, opt => opt.MapFrom(src => src.Actors))
            .ForMember(dest => dest.Directors, opt => opt.MapFrom(src => src.Directors));
        CreateMap<CreateMovieDto, Movie>()
            .ForMember(dest => dest.Actors, opt => opt.Ignore())
            .ForMember(dest => dest.Directors, opt => opt.Ignore());
        CreateMap<UpdateMovieDto, Movie>()
            .ForMember(dest => dest.Actors, opt => opt.Ignore())
            .ForMember(dest => dest.Directors, opt => opt.Ignore());

        // Order
        CreateMap<Order, OrderDto>()
            .ForMember(dest => dest.CustomerName, opt => opt.MapFrom(src => src.Customer.FirstName + " " + src.Customer.LastName))
            .ForMember(dest => dest.MovieTitle, opt => opt.MapFrom(src => src.Movie.Title));
        CreateMap<CreateOrderDto, Order>();
    }
}