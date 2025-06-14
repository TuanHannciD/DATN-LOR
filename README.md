# AuthDemo

Dự án demo xác thực người dùng sử dụng ASP.NET Core.

## Mô tả

Dự án này là một ứng dụng web demo về xác thực người dùng, được xây dựng bằng ASP.NET Core. Nó bao gồm các tính năng cơ bản như đăng ký, đăng nhập và quản lý người dùng.

## Yêu cầu hệ thống

- .NET 6.0 SDK trở lên
- Visual Studio 2022 hoặc Visual Studio Code
- SQL Server (LocalDB hoặc SQL Server Express)

## Cài đặt

1. Clone repository:
```bash
git clone [URL_REPOSITORY]
```

2. Di chuyển vào thư mục dự án:
```bash
cd AuthDemo
```

3. Khôi phục các package:
```bash
dotnet restore
```

4. Chạy migration để tạo database:
```bash
dotnet ef database update
```

5. Chạy ứng dụng:
```bash
dotnet run
```

## Cấu trúc dự án

- `Controllers/`: Chứa các controller xử lý request
- `Models/`: Chứa các model dữ liệu
- `Views/`: Chứa các view của ứng dụng
- `Data/`: Chứa các class liên quan đến database context
- `Migrations/`: Chứa các file migration database

## Đóng góp

Mọi đóng góp đều được hoan nghênh. Vui lòng tạo issue hoặc pull request để đóng góp.

## Giấy phép

MIT License 