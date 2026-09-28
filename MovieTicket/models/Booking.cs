namespace MovieTicket.Model;

public class Booking
{
    public int Id{get;set;}
    public int TicketQuantity{get;set;}
    public decimal PricePP{get;set;}
    public decimal TotalAmount{get;set;}
    public int UserId{get;set;}
    public User? User{get;set;}
    public int CinemaId{get;set;}
    public Cinema? Cinema{get;set;}
    public int MovieId{get;set;}
    public Movie? Movie{get;set;}
    public int ShowId{get;set;}
    public Show? Show{get;set;}

}