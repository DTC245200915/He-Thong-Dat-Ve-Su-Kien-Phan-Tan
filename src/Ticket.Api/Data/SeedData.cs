using Ticket.Api.Models;

namespace Ticket.Api.Data;

public static class SeedData
{
    public static void Initialize(TicketDbContext db)
    {
        if (!db.Events.Any())
        {
            db.Events.AddRange(
                new Event
                {
                    Name = "Đêm nhạc sinh viên 2026",
                    Description = "Chương trình giao lưu âm nhạc",
                    StartTime = new DateTime(2026, 12, 15, 12, 0, 0, DateTimeKind.Utc),
                    Location = "Thái Nguyên",
                    TotalTickets = 200,
                    AvailableTickets = 200
                },
                new Event
                {
                    Name = "Hội thảo công nghệ AI",
                    Description = "Khám phá công nghệ trí tuệ nhân tạo",
                    StartTime = new DateTime(2026, 12, 20, 1, 0, 0, DateTimeKind.Utc),
                    Location = "ICTU - Thái Nguyên",
                    TotalTickets = 100,
                    AvailableTickets = 100
                },
                new Event
                {
                    Name = "Ngày hội việc làm IT",
                    Description = "Kết nối sinh viên và doanh nghiệp",
                    StartTime = new DateTime(2026, 12, 25, 1, 0, 0, DateTimeKind.Utc),
                    Location = "Thái Nguyên",
                    TotalTickets = 300,
                    AvailableTickets = 300
                }
            );
        }

        if (!db.Users.Any())
        {
            db.Users.AddRange(
                new User
                {
                    FullName = "Nguyễn Văn An",
                    Email = "an@example.com",
                    PhoneNumber = "0900000001"
                },
                new User
                {
                    FullName = "Trần Thị Mai",
                    Email = "mai@example.com",
                    PhoneNumber = "0900000002"
                }
            );
        }

        db.SaveChanges();
    }
}