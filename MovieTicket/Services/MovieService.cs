using MovieTicket.Dtos;
using MovieTicket.Interface;
using MovieTicket.Model;
using MovieTicket.Exceptions;
using AutoMapper;

namespace MovieTicket.Services;

public class MovieService : IMovieService
{
    private readonly IMovieRepository _movieRepository;
    private readonly IMapper _mapper;
    public MovieService(IMovieRepository movieRepository,IMapper mapper)
    {
        _movieRepository=movieRepository;
        _mapper=mapper;
    }

    public async Task<ViewMovieDto> AddMovieAsync(AddMovieDto dto)
    {
        var movie=new Movie
        {
            Name=dto.Name,
            Genre=dto.Genre,
            Director=dto.Director,
            ReleaseDate=dto.ReleaseDate,
            Industry=dto.Industry,
            Starcast=dto.Starcast,
            CinemaId=dto.CinemaId

        };

        await _movieRepository.AddMovieAsync(movie);
        await _movieRepository.SaveChangesAsync();

        return _mapper.Map<ViewMovieDto>(movie);

    }

    public async Task<ViewMovieDto?> ViewMovieAsync(int id)
    {
        var movie=await _movieRepository.GetMovieAsync(id);

        if (movie == null)
        {
            throw new NotFoundException("Cannot find the movie");
        }

        return _mapper.Map<ViewMovieDto>(movie);
        
    }

    public async Task<List<ViewMovieDto>> ViewAllMovies()
    {
        var movies=await _movieRepository.GetAllMoviesAsync();

        return _mapper.Map<List<ViewMovieDto>>(movies);

    }

    public async Task<ViewMovieDto?> UpdateMovieAsync(int id, UpdateMovieDto dto)
    {
        var movie=await _movieRepository.GetMovieAsync(id);

        if (movie == null)
        {
            throw new NotFoundException("Cannot find the movie");
        }

        movie.Name=dto.Name;
        movie.Genre=dto.Genre;
        movie.Director=dto.Director;
        movie.ReleaseDate=dto.ReleaseDate;
        movie.Starcast=dto.Starcast;
        movie.Industry=dto.Starcast;
        movie.CinemaId=dto.CinemaId;

        await _movieRepository.SaveChangesAsync();


        return _mapper.Map<ViewMovieDto>(movie);

    }

    public async Task<string?> DeleteMovieAsync(int id)
    {
        var movie=await _movieRepository.GetMovieAsync(id);

        if (movie == null)
        {
            throw new NotFoundException("cannot find movie");
        }

         _movieRepository.RemoveMovieAsync(movie);
         await _movieRepository.SaveChangesAsync();

         return "Movie deleted sucessfully";
    }


}