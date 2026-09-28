namespace MovieTicket.Model;

public class Show
{
    public int Id{get;set;}
    public DateOnly ShowDate{get;set;}
    public TimeOnly ShowTime{get;set;}
    public int ScreenNumber{get;set;}
    public decimal Price{get;set;}
    public int CinemaId{get;set;}
    public Cinema? Cinema{get;set;}

    public int MovieId{get;set;}
    public Movie? Movie{get;set;}

     public ICollection<Booking> Bookings=new List<Booking>();

}