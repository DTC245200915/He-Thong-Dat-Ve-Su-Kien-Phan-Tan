
namespace Ticket.Api.Models;

public class Event
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public DateTime StartTime { get; set; }

    public string Location { get; set; } = string.Empty;

    public int TotalTickets { get; set; }

    public int AvailableTickets { get; set; }
}
