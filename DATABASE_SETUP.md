# إعداد قاعدة البيانات

## خيار 1: استخدام SQL Server LocalDB (المفضل للتطوير)

### المتطلبات:
- SQL Server LocalDB يجب أن يكون مثبتاً على جهازك
- يمكن تحميله من: https://docs.microsoft.com/en-us/sql/database-engine/configure-windows/sql-server-express-localdb

### الخطوات:

1. **تثبيت SQL Server LocalDB**
   - قم بتحميل وتثبيت SQL Server Express with LocalDB
   - تأكد من أن LocalDB يعمل:
     ```
     sqllocaldb info
     ```

2. **تحديث Connection String**
   - ملف `StudentAttendanceSystem.API/appsettings.json` يحتوي على:
     ```json
     "ConnectionStrings": {
       "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=StudentAttendanceSystemDb;Trusted_Connection=True;MultipleActiveResultSets=true"
     }
     ```

3. **إنشاء Migration**
   ```bash
   dotnet ef migrations add InitialCreate --project StudentAttendanceSystem.Infrastructure --startup-project StudentAttendanceSystem.API
   ```

4. **تطبيق Migration على قاعدة البيانات**
   ```bash
   dotnet ef database update --project StudentAttendanceSystem.Infrastructure --startup-project StudentAttendanceSystem.API
   ```

---

## خيار 2: استخدام SQL Server Express

### الخطوات:

1. **تثبيت SQL Server Express**
   - قم بتحميل وتثبيت SQL Server Express
   - تأكد من أن SQL Server Service يعمل

2. **تحديث Connection String**
   - في `StudentAttendanceSystem.API/appsettings.json`:
     ```json
     "ConnectionStrings": {
       "DefaultConnection": "Server=.\\SQLEXPRESS;Database=StudentAttendanceSystemDb;Trusted_Connection=True;MultipleActiveResultSets=true"
     }
     ```

3. **إنشاء Migration**
   ```bash
   dotnet ef migrations add InitialCreate --project StudentAttendanceSystem.Infrastructure --startup-project StudentAttendanceSystem.API
   ```

4. **تطبيق Migration على قاعدة البيانات**
   ```bash
   dotnet ef database update --project StudentAttendanceSystem.Infrastructure --startup-project StudentAttendanceSystem.API
   ```

---

## خيار 3: استخدام SQL Server (Full Version)

### الخطوات:

1. **تحديث Connection String**
   - في `StudentAttendanceSystem.API/appsettings.json`:
     ```json
     "ConnectionStrings": {
       "DefaultConnection": "Server=YOUR_SERVER_NAME;Database=StudentAttendanceSystemDb;User Id=YOUR_USERNAME;Password=YOUR_PASSWORD;MultipleActiveResultSets=true"
     }
     ```

2. **إنشاء Migration**
   ```bash
   dotnet ef migrations add InitialCreate --project StudentAttendanceSystem.Infrastructure --startup-project StudentAttendanceSystem.API
   ```

3. **تطبيق Migration على قاعدة البيانات**
   ```bash
   dotnet ef database update --project StudentAttendanceSystem.Infrastructure --startup-project StudentAttendanceSystem.API
   ```

---

## خيار 4: استخدام SQLite (بديل أبسط)

إذا كنت تواجه مشاكل مع SQL Server، يمكنك استخدام SQLite كبديل أبسط.

### الخطوات:

1. **إضافة حزمة SQLite**
   ```bash
   dotnet add StudentAttendanceSystem.Infrastructure/StudentAttendanceSystem.Infrastructure.csproj package Microsoft.EntityFrameworkCore.Sqlite --version 2.2.6
   ```

2. **تحديث Startup.cs**
   - في `StudentAttendanceSystem.API/Startup.cs`:
     ```csharp
     services.AddDbContext<AppDbContext>(options =>
         options.UseSqlite(Configuration.GetConnectionString("DefaultConnection")));
     ```

3. **تحديث Connection String**
   - في `StudentAttendanceSystem.API/appsettings.json`:
     ```json
     "ConnectionStrings": {
       "DefaultConnection": "Data Source=StudentAttendanceSystem.db"
     }
     ```

4. **حذف Migration القديم وإنشاء جديد**
   ```bash
   dotnet ef migrations remove --project StudentAttendanceSystem.Infrastructure --startup-project StudentAttendanceSystem.API
   dotnet ef migrations add InitialCreate --project StudentAttendanceSystem.Infrastructure --startup-project StudentAttendanceSystem.API
   ```

5. **تطبيق Migration على قاعدة البيانات**
   ```bash
   dotnet ef database update --project StudentAttendanceSystem.Infrastructure --startup-project StudentAttendanceSystem.API
   ```

---

## التحقق من قاعدة البيانات

بعد تطبيق Migration بنجاح، يمكنك التحقق from قاعدة البيانات باستخدام:
- SQL Server Management Studio (SSMS)
- Visual Studio Server Explorer
- أي أداة أخرى لإدارة قاعدة البيانات

الجداول التي سيتم إنشاؤها:
- Users
- Courses
- Lectures
- Attendances

---

## إزالة Migration

إذا كنت تريد إزالة Migration:
```bash
dotnet ef migrations remove --project StudentAttendanceSystem.Infrastructure --startup-project StudentAttendanceSystem.API
```

---

## إنشاء Migration جديد بعد تغيير الـ Entities

إذا قمت بتغيير الـ Entities:
```bash
dotnet ef migrations add MigrationName --project StudentAttendanceSystem.Infrastructure --startup-project StudentAttendanceSystem.API
dotnet ef database update --project StudentAttendanceSystem.Infrastructure --startup-project StudentAttendanceSystem.API
```
