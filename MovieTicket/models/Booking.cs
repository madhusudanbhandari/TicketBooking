using MovieTicket.enums;

namespace MovieTicket.Model;

public class Booking
{
    public int Id{get;set;}
    public int TicketQuantity{get;set;}
    public decimal PricePP{get;set;}
    public decimal TotalAmount{get;set;}
    public int UserId{get;set;}
    public User? User{get;set;}
    public int ShowId{get;set;}
    public Show? Show{get;set;}


    public BookingStatus Status{get;set;}=BookingStatus.Pending;

    public ICollection<Payment> Payments{get;set;}=new List<Payment>();

}