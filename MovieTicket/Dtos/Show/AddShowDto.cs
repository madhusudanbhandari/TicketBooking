namespace MovieTicket.Dtos;

public class AddShowDto
{
    public DateOnly ShowDate{get;set;}
    public TimeOnly ShowTime{get;set;}
    public decimal Price{get;set;}
    public int CinemaId{get;set;}
    public int MovieId{get;set;}
}