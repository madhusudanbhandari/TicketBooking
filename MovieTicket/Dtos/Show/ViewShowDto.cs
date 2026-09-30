
namespace MovieTicket.Dtos;

public class ViewShowDto
{
    public int Id{get;set;}
    public DateOnly ShowDate{get;set;}
    public TimeOnly ShowTime{get;set;}
    public int screenNumber{get;set;}
    public decimal Price{get;set;}
    public int CinemaId{get;set;}
    public int MovieId{get;set;}
}