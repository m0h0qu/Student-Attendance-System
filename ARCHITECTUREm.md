# Student Attendance System - Architecture Design

## 1. تحليل فكرة المشروع

### الهدف الرئيسي:
نظام يسمح للدكتور بإنشاء محاضرات مع QR Code، ويقوم الطلاب بمسح الكود لتسجيل حضورهم.

### المستخدمون:
- **Teacher (المعلم/الدكتور):** يدير الدورات والمحاضرات ويراقب الحضور
- **Student (الطالب):** يسجل حضوره عن طريق مسح QR Code

### العمليات الأساسية:
1. تسجيل الدخول للمستخدمين
2. إدارة الدورات (Courses)
3. إدارة المحاضرات (Lectures)
4. إنشاء QR Code للمحاضرات
5. تسجيل الحضور (Attendance)
6. عرض التقارير والسجلات

---

## 2. Clean Architecture - الطبقات

سأستخدم Clean Architecture مع 4 طبقات رئيسية:

### 📁 Domain Layer (الطبقة الأساسية)
**الوظيفة:** تحتوي على الكود الأساسي للمشروع والذي لا يعتمد على أي شيء آخر.

**المحتويات:**
- **Entities:** الفئات التي تمثل الجداول في قاعدة البيانات
- **Enums:** أنواع البيانات المخصصة (مثل Role, Status)
- **Interfaces:** الواجهات الأساسية للـ Repositories

**لماذا هذه الطبقة؟**
- لأنها قلب النظام ولا تعتمد على أي طبقة أخرى
- يمكن إعادة استخدامها في أي مشروع آخر
- تحتوي على قواعد الأعمال الأساسية

---

### 📁 Application Layer (طبقة التطبيق)
**الوظيفة:** تحتوي على منطق الأعمال والخدمات.

**المحتويات:**
- **DTOs (Data Transfer Objects):** فئات لنقل البيانات بين الطبقات
- **Services:** الخدمات التي تحتوي على منطق الأعمال
- **Interfaces:** واجهات الخدمات

**لماذا هذه الطبقة؟**
- لفصل منطق الأعمال عن الـ API
- لتسهيل الاختبار والصيانة
- لإعادة استخدام الخدمات في أماكن مختلفة

---

### 📁 Infrastructure Layer (طبقة البنية التحتية)
**الوظيفة:** تحتوي على كل ما يتعلق بالوصول للبيانات والخارجيات.

**المحتويات:**
- **DbContext:** الاتصال بقاعدة البيانات
- **Repositories:** تنفيذ عمليات CRUD على قاعدة البيانات
- **Configurations:** إعدادات Entity Framework

**لماذا هذه الطبقة؟**
- لفصل طريقة الوصول للبيانات عن بقية النظام
- لتسهيل تغيير قاعدة البيانات مستقبلاً
- لتطبيق مبدأ Dependency Inversion

---

### 📁 API Layer (طبقة الواجهة)
**الوظيفة:** تحتوي على الـ Controllers والـ Endpoints.

**المحتويات:**
- **Controllers:** الـ Endpoints للـ API
- **Program.cs:** نقطة البداية وإعداد Dependency Injection
- **appsettings.json:** إعدادات التطبيق

**لماذا هذه الطبقة؟**
- لفصل واجهة المستخدم عن منطق الأعمال
- لتسهيل تغيير نوع الواجهة (مثلاً إضافة MVC أو Blazor)
- لتطبيق مبدأ Separation of Concerns

---

## 3. هيكل المجلدات المقترح

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
│   │       └── IRepository.cs
│   │
│   ├── Application/               # طبقة التطبيق
│   │   ├── DTOs/
│   │   │   ├── UserDto.cs
│   │   │   ├── CourseDto.cs
│   │   │   ├── LectureDto.cs
│   │   │   └── AttendanceDto.cs
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
│   │   │   └── AppDbContext.cs
│   │   ├── Repositories/
│   │   │   ├── Repository.cs
│   │   │   ├── UserRepository.cs
│   │   │   ├── CourseRepository.cs
│   │   │   ├── LectureRepository.cs
│   │   │   └── AttendanceRepository.cs
│   │   └── Configurations/
│   │       ├── UserConfiguration.cs
│   │       ├── CourseConfiguration.cs
│   │       ├── LectureConfiguration.cs
│   │       └── AttendanceConfiguration.cs
│   │
│   └── API/                       # طبقة الواجهة
│       ├── Controllers/
│       │   ├── AuthController.cs
│       │   ├── UsersController.cs
│       │   ├── CoursesController.cs
│       │   ├── LecturesController.cs
│       │   └── AttendanceController.cs
│       ├── Program.cs
│       └── appsettings.json
│
└── README.md
```

---

## 4. قاعدة البيانات - الجداول والعلاقات

### الجدول 1: Users (المستخدمون)

| العمود | النوع | الوصف |
|--------|-------|-------|
| Id | int (PK) | المفتاح الأساسي |
| Name | string | اسم المستخدم |
| Email | string (Unique) | البريد الإelektronico |
| Phone | string | رقم الهاتف |
| Password | string | كلمة المرور (مشفرة) |
| Role | enum | دور المستخدم (Teacher/Student) |

**العلاقات:**
- **One-to-Many** مع Courses (كـ Teacher)
- **One-to-Many** مع Attendance (كـ Student)

---

### الجدول 2: Courses (الدورات)

| العمود | النوع | الوصف |
|--------|-------|-------|
| Id | int (PK) | المفتاح الأساسي |
| CourseName | string | اسم الدورة |
| CourseCode | string (Unique) | كود الدورة |
| TeacherId | int (FK) | معرف المعلم |

**العلاقات:**
- **Many-to-One** مع Users (Teacher)
- **One-to-Many** مع Lectures

---

### الجدول 3: Lectures (المحاضرات)

| العمود | النوع | الوصف |
|--------|-------|-------|
| Id | int (PK) | المفتاح الأساسي |
| CourseId | int (FK) | معرف الدورة |
| LectureDate | DateTime | تاريخ المحاضرة |
| StartTime | TimeSpan | وقت البدء |
| EndTime | TimeSpan | وقت الانتهاء |
| QRCode | string | كود QR للمحاضرة |
| IsActive | bool | هل المحاضرة فعالة؟ |

**العلاقات:**
- **Many-to-One** مع Courses
- **One-to-Many** مع Attendance

---

### الجدول 4: Attendance (الحضور)

| العمود | النوع | الوصف |
|--------|-------|-------|
| Id | int (PK) | المفتاح الأساسي |
| StudentId | int (FK) | معرف الطالب |
| LectureId | int (FK) | معرف المحاضرة |
| AttendanceTime | DateTime | وقت تسجيل الحضور |
| Status | enum | حالة الحضور (Present/Absent) |

**العلاقات:**
- **Many-to-One** مع Users (Student)
- **Many-to-One** مع Lectures

---

## 5. ER Diagram (مخطط العلاقات)

```
┌─────────────┐         ┌─────────────┐
│    Users    │         │   Courses   │
├─────────────┤         ├─────────────┤
│ Id (PK)     │◄────────│ Id (PK)     │
│ Name        │         │ CourseName  │
│ Email       │         │ CourseCode  │
│ Phone       │         │ TeacherId   │
│ Password    │         └──────┬──────┘
│ Role        │                │
└──────┬──────┘                │
       │                       │
       │                       │
       │                ┌──────▼──────┐
       │                │  Lectures   │
       │                ├─────────────┤
       │                │ Id (PK)     │
       │                │ CourseId    │
       │                │ LectureDate │
       │                │ StartTime   │
       │                │ EndTime     │
       │                │ QRCode      │
       │                │ IsActive    │
       │                └──────┬──────┘
       │                       │
       │                       │
       │                ┌──────▼──────┐
       │                │ Attendance  │
       │                ├─────────────┤
       └───────────────►│ Id (PK)     │
                        │ StudentId   │
                        │ LectureId   │
                        │ Attendance  │
                        │   Time      │
                        │ Status      │
                        └─────────────┘
```

---

## 6. الـ Endpoints المقترحة

### Authentication
- `POST /api/auth/register` - تسجيل مستخدم جديد
- `POST /api/auth/login` - تسجيل الدخول

### Users
- `GET /api/users` - الحصول على جميع المستخدمين
- `GET /api/users/{id}` - الحصول على مستخدم محدد
- `PUT /api/users/{id}` - تحديث مستخدم
- `DELETE /api/users/{id}` - حذف مستخدم

### Courses
- `POST /api/courses` - إنشاء دورة جديدة
- `GET /api/courses` - الحصول على جميع الدورات
- `GET /api/courses/{id}` - الحصول على دورة محددة
- `PUT /api/courses/{id}` - تحديث دورة
- `DELETE /api/courses/{id}` - حذف دورة

### Lectures
- `POST /api/lectures` - إنشاء محاضرة جديدة
- `GET /api/lectures` - الحصول على جميع المحاضرات
- `GET /api/lectures/{id}` - الحصول على محاضرة محددة
- `PUT /api/lectures/{id}/activate` - تفعيل المحاضرة
- `PUT /api/lectures/{id}/deactivate` - إلغاء تفعيل المحاضرة

### Attendance
- `POST /api/attendance/scan` - مسح QR Code وتسجيل الحضور
- `GET /api/attendance/student/{studentId}` - الحصول على سجل حضور طالب
- `GET /api/attendance/lecture/{lectureId}` - الحصول على حضور محاضرة

---

## 7. مبادئ SOLID التي سيتم تطبيقها

### Single Responsibility Principle (SRP)
- كل Class له وظيفة واحدة فقط
- الـ Controllers تتعامل مع الـ HTTP فقط
- الـ Services تحتوي على منطق الأعمال فقط
- الـ Repositories تتعامل مع قاعدة البيانات فقط

### Dependency Inversion Principle (DIP)
- الطبقات العليا تعتمد على Abstractions (Interfaces) وليس Implementations
- استخدام Dependency Injection في Program.cs

### Interface Segregation Principle (ISP)
- إنشاء واجهات صغيرة ومحددة
- عدم إجبار الـ Classes على تنفيذ طرق لا تحتاجها

---

## 8. فكرة QR Code

### كيفية العمل:
1. عند إنشاء محاضرة جديدة، يتم إنشاء:
   - **Lecture ID** فريد
   - **QR Code** يحتوي على Lecture ID أو Token خاص

2. الـ QR Code سيكون:
   - نص (String) يتم تخزينه في قاعدة البيانات
   - يمكن تحويله لصورة QR باستخدام مكتبة خارجية (مثل QRCoder)

3. عند مسح الـ QR Code:
   - الطالب يرسل الكود إلى الـ API
   - الـ API يتحقق من:
     - صحة الكود
     - وجود المحاضرة
     - أن المحاضرة IsActive
     - أن الطالب موجود
     - أن الطالب لم يسجل حضوره مسبقاً

4. إذا كان كل شيء صحيح، يتم تسجيل الحضور.

---

## 9. ملخص الخطوة الأولى

### ما تم تحليله:
✅ فكرة المشروع والمستخدمين والعمليات
✅ Clean Architecture والـ 4 طبقات
✅ هيكل المجلدات المقترح
✅ الجداول الأربعة والعلاقات بينها
✅ ER Diagram
✅ الـ Endpoints المقترحة
✅ مبادئ SOLID التي سيتم تطبيقها
✅ فكرة عمل QR Code

### الخطوة التالية:
إنشاء هيكل المشروع الفعلي والـ Layers الأساسية (Domain, Application, Infrastructure, API)
