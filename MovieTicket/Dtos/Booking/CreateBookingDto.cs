namespace MovieTicket.Dtos;

public class CreateBookingDto
{
    public int TicketQuantity{get;set;}
    public decimal PricePP{get;set;}
    public decimal TotalAmount{get;set;}
    public int CinemaId{get;set;}
    public int MovieId{get;set;}
    public int ShowId{get;set;}

}