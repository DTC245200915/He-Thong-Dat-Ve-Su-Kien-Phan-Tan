namespace Ticket.Api.DTOs;

public class CreateBookingRequest
{
    public int EventId { get; set; }

    public int UserId { get; set; }

    public int Quantity { get; set; }
}