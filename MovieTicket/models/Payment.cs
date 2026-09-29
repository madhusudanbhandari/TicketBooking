using MovieTicket.enums;

namespace MovieTicket.Model;

public class Payment
{
    public int Id{get;set;}
    public decimal Amount{get;set;}
    public int BookingId{get;set;}
    public Booking Booking{get;set;}=null!;   

    public PaymentStatus Status{get;set;}=PaymentStatus.Pending;
    public string Provider{get;set;}=null!;
    public string? TransactionId{get;set;}

    public DateTime CreatedAt{get;set;}
    public DateTime? PaidAt{get;set;}



}