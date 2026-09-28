using AutoMapper;
using MovieTicket.Dtos;
using MovieTicket.Model;

namespace MovieTicket.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<User,RegisterUserResponseDto>();
        CreateMap<User,LoginUserResponseDto>();
        CreateMap<Cinema,ViewCinemaDto>();
        CreateMap<Movie, ViewMovieDto>();
        CreateMap<Show,ViewShowDto>();
        CreateMap<Booking,ViewBookingDto>();
    }
}