using Microsoft.EntityFrameworkCore;
using Ticket.Api.Data;
using Ticket.Api.DTOs;
using Ticket.Api.Models;

var builder = WebApplication.CreateBuilder(args);

// Đăng ký OpenAPI
builder.Services.AddOpenApi();

// Kết nối PostgreSQL
builder.Services.AddDbContext<TicketDbContext>(options =>
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();

    // Khởi tạo dữ liệu mẫu
    using var scope = app.Services.CreateScope();

    var db = scope.ServiceProvider
        .GetRequiredService<TicketDbContext>();

    SeedData.Initialize(db);
}

// Kiểm tra trạng thái API
app.MapGet("/", () => Results.Ok(new
{
    message = "Ticket API is running",
    status = "OK"
}));

// Kiểm tra kết nối database
app.MapGet("/db-check", async (TicketDbContext db) =>
{
    return Results.Ok(new
    {
        database = "PostgreSQL",
        connected = await db.Database.CanConnectAsync()
    });
});

// Lấy danh sách sự kiện
app.MapGet("/events", async (TicketDbContext db) =>
{
    var events = await db.Events.ToListAsync();

    return Results.Ok(events);
});

// Lấy chi tiết sự kiện theo ID
app.MapGet("/events/{id:int}", async (int id, TicketDbContext db) =>
{
    var item = await db.Events.FindAsync(id);

    if (item == null)
    {
        return Results.NotFound(new
        {
            message = "Event not found"
        });
    }

    return Results.Ok(item);
});
// API đặt vé sự kiện
app.MapPost("/bookings", async (
    CreateBookingRequest request,
    TicketDbContext db) =>
{
    // Kiểm tra số lượng vé
    if (request.Quantity <= 0)
    {
        return Results.BadRequest(new
        {
            message = "Quantity must be greater than 0"
        });
    }

    // Kiểm tra sự kiện
    var eventItem = await db.Events.FindAsync(request.EventId);

    if (eventItem == null)
    {
        return Results.NotFound(new
        {
            message = "Event not found"
        });
    }

    // Kiểm tra người dùng
    var user = await db.Users.FindAsync(request.UserId);

    if (user == null)
    {
        return Results.NotFound(new
        {
            message = "User not found"
        });
    }

    // Tạo yêu cầu đặt vé
    var booking = new Booking
    {
        EventId = request.EventId,
        UserId = request.UserId,
        Quantity = request.Quantity,
        Status = "Pending",
        CreatedAt = DateTime.UtcNow
    };

    db.Bookings.Add(booking);
    await db.SaveChangesAsync();

    return Results.Created($"/bookings/{booking.Id}", new
    {
        booking.Id,
        booking.EventId,
        booking.UserId,
        booking.Quantity,
        booking.Status,
        booking.CreatedAt
    });
});

// Tra cứu trạng thái đặt vé
app.MapGet("/bookings/{id:int}", async (
    int id, TicketDbContext db) =>
{
    var booking = await db.Bookings.FindAsync(id);

    if (booking == null)
    {
        return Results.NotFound(new
        {
            message = "Booking not found"
        });
    }

    return Results.Ok(new
    {
        booking.Id,
        booking.EventId,
        booking.UserId,
        booking.Quantity,
        booking.Status,
        booking.CreatedAt
    });
});

// Lấy danh sách đặt vé theo người dùng
app.MapGet("/users/{userId:int}/bookings", async (
    int userId, TicketDbContext db) =>
{
    var user = await db.Users.FindAsync(userId);

    if (user == null)
    {
        return Results.NotFound(new
        {
            message = "User not found"
        });
    }

    var bookings = await db.Bookings
        .Where(b => b.UserId == userId)
        .OrderByDescending(b => b.CreatedAt)
        .Select(b => new
        {
            b.Id,
            b.EventId,
            EventName = b.Event.Name,
            b.UserId,
            b.Quantity,
            b.Status,
            b.CreatedAt
        })
        .ToListAsync();

    return Results.Ok(bookings);
});

app.Run();