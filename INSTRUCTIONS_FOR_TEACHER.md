# تعليمات تشغيل المشروع - للدكتور

## المتطلبات
- SQL Server (LocalDB أو Express أو Full Version)
- SQL Server Management Studio (SSMS)
- .NET Core SDK 2.2 أو أعلى
- Visual Studio 2019 أو VS Code

## خطوات التشغيل

### 1. فتح قاعدة البيانات في SQL Server Management Studio (SSMS)

1. افتح **SQL Server Management Studio (SSMS)**
2. في شاشة الاتصال:
   - **Server Name:** اكتب `.` أو `localhost`
   - **Authentication:** اختر **Windows Authentication**
   - اضغط **Connect**
3. ستجد قاعدة البيانات باسم **StudentAttendanceSystemDb** في قواعد البيانات
4. قم بتوسيع قاعدة البيانات لرؤية الجداول:
   - **Users** - جدول المستخدمين
   - **Courses** - جدول الدورات
   - **Lectures** - جدول المحاضرات
   - **Attendances** - جدول الحضور
5. لرؤية البيانات:
   - اضغط بزر الفأرة الأيمن على أي جدول
   - اختر **Select Top 1000 Rows**

### 2. تشغيل المشروع

افتح سطر الأوامر في مجلد المشروع وشغل:

```bash
dotnet run --project StudentAttendanceSystem.API
```

سيعمل المشروع على: `https://localhost:5001`

---

## إذا كانت قاعدة البيانات غير موجودة في SSMS

### إنشاء قاعدة البيانات:

افتح سطر الأوامر في مجلد المشروع وشغل:

```bash
dotnet ef database update --project StudentAttendanceSystem.Infrastructure --startup-project StudentAttendanceSystem.API
```

ثم افتح SSMS وستجد قاعدة البيانات **StudentAttendanceSystemDb**

---

## Connection String المستخدم

المشروع يستخدم حالياً SQL Server على localhost:

```json
"DefaultConnection": "Server=localhost;Database=StudentAttendanceSystemDb;Trusted_Connection=True;MultipleActiveResultSets=true"
```

يمكنك العثور عليه في: `StudentAttendanceSystem.API/appsettings.json`

---

## اختبار المشروع

بعد التشغيل، يمكنك اختبار الـ Endpoints باستخدام:
- **Postman** (مفضل)
- **Swagger** (يمكن إضافته)
- **curl**

### أمثلة للاختبار:

#### تسجيل مستخدم جديد (معلم):
```http
POST https://localhost:5001/api/auth/register
Content-Type: application/json

{
  "name": "Dr. Ahmed",
  "email": "ahmed@example.com",
  "phone": "1234567890",
  "password": "Teacher123",
  "role": 0
}
```

#### تسجيل الدخول:
```http
POST https://localhost:5001/api/auth/login
Content-Type: application/json

{
  "email": "ahmed@example.com",
  "password": "Teacher123"
}
```

#### الحصول على جميع المستخدمين:
```http
GET https://localhost:5001/api/users
```

#### إنشاء دورة:
```http
POST https://localhost:5001/api/courses
Content-Type: application/json

{
  "courseName": "C# Programming",
  "courseCode": "CS101",
  "teacherId": 1
}
```

#### إنشاء محاضرة:
```http
POST https://localhost:5001/api/lectures
Content-Type: application/json

{
  "courseId": 1,
  "lectureDate": "2024-09-10",
  "startTime": "09:00:00",
  "endTime": "10:30:00"
}
```

#### مسح QR Code وتسجيل الحضور:
```http
POST https://localhost:5001/api/attendance/scan
Content-Type: application/json

{
  "qrCode": "guid-here",
  "studentId": 2
}
```

---

## ملاحظة مهمة
عند تشغيل المشروع لأول مرة، سيتم إضافة بيانات تجريبية تلقائياً (Seed Data):
- 2 معلمين
- 3 طلاب
- 3 دورات
- 3 محاضرات
- 2 سجل حضور

يمكنك رؤية هذه البيانات في SSMS بفتح الجداول.

---

## للمزيد من التفاصيل
راجع ملفات:
- `README.md` - دليل شامل للمشروع
- `DATABASE_SETUP.md` - خيارات إعداد قاعدة البيانات
- `ARCHITECTURE.md` - تحليل المشروع والبنية المعمارية
