namespace MovieTicket.Model;

public class Movie
{
    public int Id{get;set;}
    public string Name{get;set;}=string.Empty;
    public string Genre{get;set;}=string.Empty;
    public string Director{get;set;}=string.Empty;
    public string Industry{get;set;}=string.Empty;
    public string Starcast{get;set;}=string.Empty;
    public DateOnly ReleaseDate{get;set;}

    public int CinemaId{get;set;}
    public Cinema? Cinema{get;set;}

    public ICollection<Show> Shows=new List<Show>();

}