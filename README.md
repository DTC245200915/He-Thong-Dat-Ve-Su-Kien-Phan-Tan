
# Đề tài 03 - Hệ thống đặt vé sự kiện phân tán

## 1. Giới thiệu

Đồ án học phần Phát triển ứng dụng phân tán.

Hệ thống cho phép người dùng tra cứu sự kiện, đặt vé
và theo dõi trạng thái yêu cầu đặt vé.

Việc xác nhận vé được xử lý bất đồng bộ thông qua
RabbitMQ và Booking Worker.

## 2. Công nghệ sử dụng

- C# / ASP.NET Core Web API (.NET 10)
- Entity Framework Core
- PostgreSQL
- RabbitMQ
- .NET Worker Service
- Console Client
- Docker Compose
- Git và GitHub

## 3. Kiến trúc hệ thống

Client -> Ticket API -> RabbitMQ -> Booking Worker
                  |                      |
                  v                      v
                PostgreSQL Database

## 4. Chức năng chính

1. Xem danh sách sự kiện và số vé còn lại.
2. Gửi yêu cầu đặt vé.
3. Tạo yêu cầu với trạng thái Pending.
4. Gửi thông điệp đặt vé vào RabbitMQ.
5. Worker kiểm tra và xác nhận hoặc từ chối vé.
6. Cập nhật số lượng vé còn lại.
7. Tra cứu trạng thái đặt vé.
8. Xem danh sách vé của người dùng.

## 5. API dự kiến

| Method | Endpoint | Chức năng |
|--------|----------|-----------|
| GET | /events | Danh sách sự kiện |
| GET | /events/{id} | Chi tiết sự kiện |
| POST | /bookings | Tạo yêu cầu đặt vé |
| GET | /bookings/{id} | Trạng thái đặt vé |
| GET | /users/{userId}/bookings | Vé của người dùng |

## 6. Cấu trúc dự án

- Ticket.Api: Cung cấp REST API.
- Booking.Worker: Xử lý xác nhận vé bất đồng bộ.
- Ticket.Client: Ứng dụng khách.

## 7. Tiến độ thực hiện

- [x] Khởi tạo Solution .NET 10.
- [x] Tạo Ticket.Api, Booking.Worker, Ticket.Client.
- [x] Khởi tạo Git và kết nối GitHub.
- [x] Cài thư viện EF Core PostgreSQL.
- [ ] Thiết kế cơ sở dữ liệu.
- [ ] Xây dựng API quản lý sự kiện.
- [ ] Xây dựng chức năng đặt vé.
- [ ] Tích hợp RabbitMQ.
- [ ] Xây dựng Booking Worker xử lý vé.
- [ ] Kiểm thử và triển khai Docker Compose.

## 8. Thông tin

Học phần: Phát triển ứng dụng phân tán

Đề tài: 03 - Hệ thống đặt vé sự kiện phân tán

Trạng thái: Đang phát triển
