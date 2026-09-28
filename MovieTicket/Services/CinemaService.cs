using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MovieTicket.Dtos;
using MovieTicket.Exceptions;
using MovieTicket.Interface;
using MovieTicket.Model;

namespace MovieTicket.Services;

public class CinemaService : ICinemaService
{
    private readonly ICinemaRepository _cinemaRepository;
    public CinemaService(ICinemaRepository cinemaRepository)
    {
        _cinemaRepository=cinemaRepository;
    }

    public async Task<ViewCinemaDto> RegisterCinema(RegisterCinemaDto dto)
    {
        var cinema=new Cinema
        {
            Name=dto.Name,
            Location=dto.Location,
            ChainName=dto.ChainName,
        };

        await _cinemaRepository.AddCinemaAsync(cinema);
        await _cinemaRepository.SaveChangesAsync();

        return new ViewCinemaDto
        {
            Id=cinema.Id,
            Name=cinema.Name,
            Location=cinema.Location,
            ChainName=cinema.ChainName
        };
    }

    public async Task<ViewCinemaDto?> GetCinemaAsync(int id)
    {
        var cinema=await _cinemaRepository.GetCinemaAsync(id);

        if (cinema == null)
        {
            throw new NotFoundException("Cannot find the cinema");
        }

        return new ViewCinemaDto
        {
            Id=cinema.Id,
            Name=cinema.Name,
            Location=cinema.Location,
            ChainName=cinema.ChainName
        };
    }

    public async Task<ViewCinemaDto?> UpdateCinemaAsync(int id,UpdateCinemaDto dto)
    {
        var cinema=await _cinemaRepository.GetCinemaAsync(id);
        if (cinema == null)
        {
            throw new NotFoundException("Cannot find the cinema");
        }

        cinema.Name=dto.Name;
        cinema.Location=dto.Location;
        cinema.ChainName=dto.ChainName;

        await _cinemaRepository.SaveChangesAsync();

        return new ViewCinemaDto
        {
            Id=cinema.Id,
            Name=cinema.Name,
            Location=cinema.Location,
            ChainName=cinema.ChainName
        };
    } 

    public async Task<string?> DeleteCinemaAsync(int id)
    {
        var cinema=await _cinemaRepository.GetCinemaAsync(id);

        if (cinema == null)
        {
            throw new NotFoundException("Cannot find the cinema");
        }

         _cinemaRepository.RemoveCinema(cinema);

         await _cinemaRepository.SaveChangesAsync();

         return "Cinema Deleted Successfully";

    }
}