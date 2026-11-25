
using Microsoft.EntityFrameworkCore;
using Ticket.Api.Models;

namespace Ticket.Api.Data;

public class TicketDbContext : DbContext
{
    public TicketDbContext(DbContextOptions<TicketDbContext> options)
        : base(options)
    {
    }

    public DbSet<Event> Events => Set<Event>();

    public DbSet<User> Users => Set<User>();

    public DbSet<Booking> Bookings => Set<Booking>();
}
