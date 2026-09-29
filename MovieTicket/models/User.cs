using MovieTicket.enums;

namespace MovieTicket.Model;

public class User
{
    public int Id{get;set;}
    public string Name{get;set;}=string.Empty;
    public int Age{get;set;}
    public string Gender{get;set;}=string.Empty;

    public string Email{get;set;}=string.Empty;
    public string Password{get;set;}=string.Empty;
    public UserRole Role{get;set;}

    public ICollection<Cinema> Cinemas{get;set;}=new List<Cinema>();
    public ICollection<Booking> Bookings{get;set;}=new List<Booking>();
}