using AutoMapper;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MovieTicket.Dtos;
using MovieTicket.Exceptions;
using MovieTicket.Interface;
using MovieTicket.Mapping;
using MovieTicket.Model;

namespace MovieTicket.Services;

public class CinemaService : ICinemaService
{
    private readonly ICinemaRepository _cinemaRepository;
    private readonly IMapper _mapper;
    public CinemaService(ICinemaRepository cinemaRepository, IMapper mapper)
    {
        _cinemaRepository=cinemaRepository;
        _mapper=mapper;
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

        return _mapper.Map<ViewCinemaDto>(cinema);
    }

    public async Task<ViewCinemaDto?> GetCinemaAsync(int id)
    {
        var cinema=await _cinemaRepository.GetCinemaAsync(id);

        if (cinema == null)
        {
            throw new NotFoundException("Cannot find the cinema");
        }

        return _mapper.Map<ViewCinemaDto>(cinema);
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

        return _mapper.Map<ViewCinemaDto>(cinema);
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