
namespace Ticket.Api.Models;

public class Booking
{
    public int Id { get; set; }

    public int EventId { get; set; }

    public int UserId { get; set; }

    public int Quantity { get; set; }

    public string Status { get; set; } = "Pending";

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    // Navigation properties
    public Event Event { get; set; } = null!;

    public User User { get; set; } = null!;
}
