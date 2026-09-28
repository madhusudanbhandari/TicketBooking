namespace MovieTicket.Dtos;

public class UpdateShowDto
{
    public DateOnly ShowDate{get;set;}
    public TimeOnly ShowTime{get;set;}
    public decimal Price{get;set;}
    public int CinemaId{get;set;}
    public int MovieId{get;set;}
}