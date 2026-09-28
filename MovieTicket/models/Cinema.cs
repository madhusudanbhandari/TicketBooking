namespace MovieTicket.Model;

public class Cinema
{
    public int Id{get;set;}
    public string Name{get;set;}=string.Empty;
    public string Location{get;set;}=string.Empty;    
    public string ChainName{get;set;}=string.Empty;

    public int UserId{get;set;}
    public User? User{get;set;}
    public ICollection<Movie> Movies=new List<Movie>();
    public ICollection<Show> Shows=new List<Show>();
    public ICollection<Booking> Bookings=new List<Booking>();

}