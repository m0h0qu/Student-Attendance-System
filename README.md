# Student Attendance System Using QR Code

نظام تحضير الطلاب باستخدام QR Code - مشروع Visual Programming

## 📋 نظرة عامة

نظام ويب يسمح للدكتور/المحاضر بإنشاء محاضرات مع QR Code خاص بكل محاضرة، ويقوم الطلاب بمسح QR Code لتسجيل حضورهم.

## 🏗️ البنية التقنية

### التقنيات المستخدمة
- **C#** - لغة البرمجة الرئيسية
- **ASP.NET Core Web API** - إطار العمل للـ API
- **SQL Server** - قاعدة البيانات
- **Entity Framework Core** - ORM للوصول لقاعدة البيانات
- **Clean Architecture** - نمط التصميم المعماري

### Clean Architecture Layers

المشروع مقسم إلى 4 طبقات رئيسية:

```
├── Domain Layer (الطبقة الأساسية)
│   ├── Entities (الجداول)
│   ├── Enums (أنواع البيانات المخصصة)
│   └── Interfaces (واجهات الـ Repositories)
│
├── Application Layer (طبقة التطبيق)
│   ├── DTOs (كائنات نقل البيانات)
│   ├── Interfaces (واجهات الخدمات)
│   └── Services (الخدمات ومنطق الأعمال)
│
├── Infrastructure Layer (طبقة البنية التحتية)
│   ├── Data (DbContext)
│   ├── Repositories (تنفيذ الوصول للبيانات)
│   └── Configurations (إعدادات Entity Framework)
│
└── API Layer (طبقة الواجهة)
    ├── Controllers (الـ Endpoints)
    ├── Program.cs (نقطة البداية)
    └── appsettings.json (الإعدادات)
```

## 🗄️ قاعدة البيانات

### الجداول

#### Users (المستخدمون)
- `Id` - المفتاح الأساسي
- `Name` - اسم المستخدم
- `Email` - البريد الإلكتروني (فريد)
- `Phone` - رقم الهاتف
- `Password` - كلمة المرور (مشفرة)
- `Role` - دور المستخدم (Teacher/Student)

#### Courses (الدورات)
- `Id` - المفتاح الأساسي
- `CourseName` - اسم الدورة
- `CourseCode` - كود الدورة (فريد)
- `TeacherId` - معرف المعلم

#### Lectures (المحاضرات)
- `Id` - المفتاح الأساسي
- `CourseId` - معرف الدورة
- `LectureDate` - تاريخ المحاضرة
- `StartTime` - وقت البدء
- `EndTime` - وقت الانتهاء
- `QRCode` - كود QR للمحاضرة
- `IsActive` - هل المحاضرة فعالة؟

#### Attendance (الحضور)
- `Id` - المفتاح الأساسي
- `StudentId` - معرف الطالب
- `LectureId` - معرف المحاضرة
- `AttendanceTime` - وقت تسجيل الحضور
- `Status` - حالة الحضور (Present/Absent)

### العلاقات
- User ←→ Course (One-to-Many)
- Course ←→ Lecture (One-to-Many)
- User ←→ Attendance (One-to-Many)
- Lecture ←→ Attendance (One-to-Many)

## 🚀 خطوات التشغيل

### المتطلبات
- .NET Core SDK 2.2 أو أعلى
- SQL Server (LocalDB, Express, أو Full Version)
- Visual Studio 2019 أو VS Code

### 1. استنساخ المشروع
```bash
git clone <repository-url>
cd StudentAttendanceSystem
```

### 2. إعداد قاعدة البيانات

راجع ملف `DATABASE_SETUP.md` للخيارات المتعددة.

#### الخيار السريع (SQL Server LocalDB):
```bash
# تثبيت SQL Server LocalDB إذا لم يكن مثبتاً
# ثم:
dotnet ef migrations add InitialCreate --project StudentAttendanceSystem.Infrastructure --startup-project StudentAttendanceSystem.API
dotnet ef database update --project StudentAttendanceSystem.Infrastructure --startup-project StudentAttendanceSystem.API
```

### 3. بناء المشروع
```bash
dotnet build
```

### 4. تشغيل المشروع
```bash
dotnet run --project StudentAttendanceSystem.API
```

سيعمل الـ API على: `https://localhost:5001`

## 📡 API Endpoints

### Authentication

#### تسجيل مستخدم جديد
```http
POST /api/auth/register
Content-Type: application/json

{
  "name": "Dr. Ahmed",
  "email": "ahmed@example.com",
  "phone": "1234567890",
  "password": "Password123",
  "role": 1
}
```

#### تسجيل الدخول
```http
POST /api/auth/login
Content-Type: application/json

{
  "email": "ahmed@example.com",
  "password": "Password123"
}
```

### Users

#### الحصول على جميع المستخدمين
```http
GET /api/users
```

#### الحصول على مستخدم محدد
```http
GET /api/users/{id}
```

#### تحديث مستخدم
```http
PUT /api/users/{id}
Content-Type: application/json

{
  "name": "Dr. Ahmed Updated",
  "email": "ahmed@example.com",
  "phone": "1234567890",
  "password": "NewPassword123",
  "role": 1
}
```

#### حذف مستخدم
```http
DELETE /api/users/{id}
```

### Courses

#### إنشاء دورة جديدة
```http
POST /api/courses
Content-Type: application/json

{
  "courseName": "C# Programming",
  "courseCode": "CS101",
  "teacherId": 1
}
```

#### الحصول على جميع الدورات
```http
GET /api/courses
```

#### الحصول على دورة محددة
```http
GET /api/courses/{id}
```

#### تحديث دورة
```http
PUT /api/courses/{id}
Content-Type: application/json

{
  "courseName": "C# Programming Updated",
  "courseCode": "CS101",
  "teacherId": 1
}
```

#### حذف دورة
```http
DELETE /api/courses/{id}
```

### Lectures

#### إنشاء محاضرة جديدة
```http
POST /api/lectures
Content-Type: application/json

{
  "courseId": 1,
  "lectureDate": "2024-09-10",
  "startTime": "09:00:00",
  "endTime": "10:30:00"
}
```

#### الحصول على جميع المحاضرات
```http
GET /api/lectures
```

#### الحصول على محاضرة محددة
```http
GET /api/lectures/{id}
```

#### تفعيل المحاضرة
```http
PUT /api/lectures/{id}/activate
```

#### إلغاء تفعيل المحاضرة
```http
PUT /api/lectures/{id}/deactivate
```

### Attendance

#### مسح QR Code وتسجيل الحضور
```http
POST /api/attendance/scan
Content-Type: application/json

{
  "qrCode": "guid-here",
  "studentId": 2
}
```

#### الحصول على سجل حضور طالب
```http
GET /api/attendance/student/{studentId}
```

#### الحصول على حضور محاضرة
```http
GET /api/attendance/lecture/{lectureId}
```

## ✅ Validation

النظام يطبق Validation على جميع الـ Endpoints:

- الحقول المطلوبة لا يمكن أن تكون فارغة
- Email يجب أن يكون بصيغة صحيحة ويحتوي على @
- Phone يجب أن يحتوي على أرقام فقط
- Password يجب أن يكون 6 أحرف على الأقل
- عدم السماح بإنشاء مستخدم بنفس البريد الإلكتروني
- عدم السماح للطالب بتسجيل الحضور مرتين لنفس المحاضرة
- عدم السماح بتسجيل الحضور إذا كان الـ QR Code غير فعال
- التأكد من أن Id المطلوب موجود قبل تنفيذ العملية

## 🔐 QR Code System

### كيفية العمل:
1. عند إنشاء محاضرة جديدة، يتم إنشاء:
   - **Lecture ID** فريد
   - **QR Code** (GUID) مرتبط بالمحاضرة

2. عند مسح QR Code:
   - الطالب يرسل الكود إلى الـ API
   - الـ API يتحقق من:
     - صحة الكود
     - وجود المحاضرة
     - أن المحاضرة IsActive
     - أن الطالب موجود
     - أن الطالب لم يسجل حضوره مسبقاً

3. إذا كان كل شيء صحيح، يتم تسجيل الحضور.

## 🧪 Seed Data

عند تشغيل المشروع لأول مرة، سيتم إضافة بيانات تجريبية تلقائياً:

### المستخدمون:
- **2 معلمين:**
  - Dr. Ahmed Mohamed (ahmed.teacher@example.com / Teacher123)
  - Dr. Sara Ali (sara.teacher@example.com / Teacher123)

- **3 طلاب:**
  - Omar Hassan (omar.student@example.com / Student123)
  - Fatima Ahmed (fatima.student@example.com / Student123)
  - Khaled Omar (khaled.student@example.com / Student123)

### الدورات:
- C# Programming (CS101)
- Web Development (CS102)
- Database Systems (CS103)

### المحاضرات:
- 3 محاضرات مع QR Codes فريدة

### الحضور:
- 2 سجل حضور تجريبي

## 🎯 مبادئ SOLID المطبقة

### Single Responsibility Principle (SRP)
- كل Class له وظيفة واحدة فقط
- Controllers تتعامل مع HTTP فقط
- Services تحتوي على منطق الأعمال فقط
- Repositories تتعامل مع قاعدة البيانات فقط

### Dependency Inversion Principle (DIP)
- الطبقات العليا تعتمد على Abstractions (Interfaces)
- استخدام Dependency Injection في Startup.cs

### Interface Segregation Principle (ISP)
- إنشاء واجهات صغيرة ومحددة
- عدم إجبار الـ Classes على تنفيذ طرق لا تحتاجها

## 📁 هيكل المشروع

```
StudentAttendanceSystem/
├── src/
│   ├── Domain/                    # الطبقة الأساسية
│   │   ├── Entities/
│   │   │   ├── User.cs
│   │   │   ├── Course.cs
│   │   │   ├── Lecture.cs
│   │   │   └── Attendance.cs
│   │   ├── Enums/
│   │   │   ├── Role.cs
│   │   │   └── AttendanceStatus.cs
│   │   └── Interfaces/
│   │       ├── IRepository.cs
│   │       ├── IUserRepository.cs
│   │       ├── ICourseRepository.cs
│   │       ├── ILectureRepository.cs
│   │       └── IAttendanceRepository.cs
│   │
│   ├── Application/               # طبقة التطبيق
│   │   ├── DTOs/
│   │   │   ├── UserDto.cs
│   │   │   ├── CourseDto.cs
│   │   │   ├── LectureDto.cs
│   │   │   ├── AttendanceDto.cs
│   │   │   ├── CreateUserDto.cs
│   │   │   ├── LoginDto.cs
│   │   │   ├── CreateCourseDto.cs
│   │   │   ├── CreateLectureDto.cs
│   │   │   └── ScanQrCodeDto.cs
│   │   ├── Interfaces/
│   │   │   ├── IUserService.cs
│   │   │   ├── ICourseService.cs
│   │   │   ├── ILectureService.cs
│   │   │   └── IAttendanceService.cs
│   │   └── Services/
│   │       ├── UserService.cs
│   │       ├── CourseService.cs
│   │       ├── LectureService.cs
│   │       └── AttendanceService.cs
│   │
│   ├── Infrastructure/             # طبقة البنية التحتية
│   │   ├── Data/
│   │   │   ├── AppDbContext.cs
│   │   │   └── SeedData.cs
│   │   ├── Repositories/
│   │   │   ├── Repository.cs
│   │   │   ├── UserRepository.cs
│   │   │   ├── CourseRepository.cs
│   │   │   ├── LectureRepository.cs
│   │   │   └── AttendanceRepository.cs
│   │   ├── Configurations/
│   │   │   ├── UserConfiguration.cs
│   │   │   ├── CourseConfiguration.cs
│   │   │   ├── LectureConfiguration.cs
│   │   │   └── AttendanceConfiguration.cs
│   │   └── Migrations/
│   │
│   └── API/                       # طبقة الواجهة
│       ├── Controllers/
│       │   ├── AuthController.cs
│       │   ├── UsersController.cs
│       │   ├── CoursesController.cs
│       │   ├── LecturesController.cs
│       │   └── AttendanceController.cs
│       ├── Program.cs
│       ├── Startup.cs
│       └── appsettings.json
│
├── README.md
├── DATABASE_SETUP.md
└── ARCHITECTURE.md
```

## 🔧 الأوامر المفيدة

### بناء المشروع
```bash
dotnet build
```

### تشغيل المشروع
```bash
dotnet run --project StudentAttendanceSystem.API
```

### إنشاء Migration جديد
```bash
dotnet ef migrations add MigrationName --project StudentAttendanceSystem.Infrastructure --startup-project StudentAttendanceSystem.API
```

### تطبيق Migration على قاعدة البيانات
```bash
dotnet ef database update --project StudentAttendanceSystem.Infrastructure --startup-project StudentAttendanceSystem.API
```

### إزالة Migration
```bash
dotnet ef migrations remove --project StudentAttendanceSystem.Infrastructure --startup-project StudentAttendanceSystem.API
```

## 📝 ملاحظات مهمة

### للأمان
- كلمات المرور مشفرة باستخدام SHA256
- لا يتم إرسال كلمات المرور في الـ Responses
- Connection Strings يجب أن تكون في Environment Variables في الإنتاج

### للإنتاج
- إضافة JWT Authentication
- إضافة Rate Limiting
- إضافة Logging
- إضافة Unit Tests
- إضافة Swagger Documentation

### للتحسين
- إضافة Frontend (React/Angular/Vue)
- إضافة Real-time Notifications (SignalR)
- إضافة Email Notifications
- إضافة Reports Generation

## 👨‍💻 للمطورين

### إضافة Entity جديد:
1. إنشاء Entity في `Domain/Entities`
2. إنشاء Configuration في `Infrastructure/Configurations`
3. إنشاء Repository في `Infrastructure/Repositories`
4. إنشاء DTOs في `Application/DTOs`
5. إنشاء Interface في `Application/Interfaces`
6. إنشاء Service في `Application/Services`
7. إنشاء Controller في `API/Controllers`
8. إنشاء Migration
9. تحديث قاعدة البيانات

## 📄 الترخيص

هذا المشروع تم إنشاؤه لأغراض تعليمية لمادة Visual Programming.

## 🤝 المساهمة

للمساهمة في المشروع:
1. Fork المشروع
2. إنشاء Branch جديد
3. Commit التغييرات
4. Push إلى Branch
5. إنشاء Pull Request

## 📞 التواصل

للاستفسارات والدعم، يرجى التواصل مع:
- الدكتور المسؤول عن المادة
- فريق التطوير

---

**تم التطوير بواسطة:** طالب مادة Visual Programming
**التاريخ:** 2024
# Student-Attendance-System_Submission
# Project
# Project
# Project
