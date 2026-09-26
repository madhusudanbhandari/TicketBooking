namespace MovieTicket.Model;

public class Show
{
    public int Id{get;set;}
    public DateTime ShowTime{get;set;}

    public int CinemaId{get;set;}
    public Cinema? Cinema{get;set;}

    public int MovieId{get;set;}
    public Movie? Movie{get;set;}
}