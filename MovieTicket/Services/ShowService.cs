using AutoMapper;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MovieTicket.Dtos;
using MovieTicket.Exceptions;
using MovieTicket.Interface;
using MovieTicket.Model;

namespace MovieTicket.Services;

public class ShowService : IShowService
{
    private readonly IShowRepository _showRepository;
    private readonly IMapper _mapper;

    public ShowService(IShowRepository showRepository,IMapper mapper)
    {
        _showRepository=showRepository;
        _mapper=mapper;
    }

    public async Task<ViewShowDto> AddShowAsync(AddShowDto dto)
    {
        var show=new Show
        {
            ShowDate=dto.ShowDate,
            ShowTime=dto.ShowTime,
            Price=dto.Price,
            CinemaId=dto.CinemaId,
            MovieId=dto.MovieId
        };

        await _showRepository.AddShowAsync(show);
        await _showRepository.SaveChangesAsync();

        return  _mapper.Map<ViewShowDto>(show);

    }

    public async Task<ViewShowDto?> ViewShowByIdAsync(int id)
    {

        var show=await _showRepository.GetShowAsync(id);

        if (show == null)
        {
            throw new NotFoundException("Cannot find the show");
        }
        return _mapper.Map<ViewShowDto>(show);
    }

    public async Task<List<ViewShowDto>> ViewAllShowsAsync()
    {
        var shows=await _showRepository.GetAllShowsAsync();

        return _mapper.Map<List<ViewShowDto>>(shows);
    }

    public async Task<ViewShowDto?> UpdateShowAsync(int id,UpdateShowDto dto)
    {
        var show=await _showRepository.GetShowAsync(id);

        if (show == null)
        {
            throw new NotFoundException("Cannnot find the show");
        }

        show.ShowDate=dto.ShowDate;
        show.ShowTime=dto.ShowTime;
        show.Price=dto.Price;
        show.CinemaId=dto.CinemaId;
        show.MovieId=dto.MovieId;

        await _showRepository.SaveChangesAsync();

        return _mapper.Map<ViewShowDto>(show);
    }

    public async Task<string?> DeleteShowAsync(int id)
    {
        var show=await _showRepository.GetShowAsync(id);

        if (show == null)
        {
            throw new NotFoundException("Cannot find the show");
        }

         _showRepository.RemoveShowAsync(show);
         

         return "Deleted sucessfully";
    }
}