# دليل تسليم المشروع للدكتور

## ما تم تنفيذه

المشروع هو **Student Attendance System** باستخدام ASP.NET Core Web API وبنية طبقات:

- `Domain`: الكيانات `Course` و`Lecture`.
- `Application`: DTOs والواجهات والخدمات.
- `Infrastructure`: `DbContext` وEntity Configurations وRepositories وMigration.
- `API`: Controllers وSwagger.

تم تجهيز جدولين للتسليم:

| الجدول | المسارات المتاحة في Swagger |
|---|---|
| Course | `POST /api/Courses`، `GET /api/Courses`، `GET /api/Courses/{id}`، `PUT /api/Courses/{id}`، `DELETE /api/Courses/{id}` |
| Lecture | `POST /api/Lectures`، `GET /api/Lectures`، `GET /api/Lectures/{id}`، `PUT /api/Lectures/{id}`، `DELETE /api/Lectures/{id}`، التفعيل والإلغاء |

## التشغيل في Visual Studio

1. افتح ملف `StudentAttendanceSystem.sln`.
2. اجعل مشروع `StudentAttendanceSystem.API` هو Startup Project.
3. تأكد من تشغيل SQL Server وأن اسم قاعدة البيانات والاتصال صحيحان في `StudentAttendanceSystem.API/appsettings.json`.
4. طبّق الـ Migration من Package Manager Console أو من الطرفية:

```bash
dotnet ef database update --project StudentAttendanceSystem.Infrastructure --startup-project StudentAttendanceSystem.API
```

5. شغّل المشروع، ثم افتح رابط Swagger الذي يظهر في المتصفح، وغالبًا يكون:

```text
https://localhost:<port>/swagger
```

## ترتيب العرض في Swagger

### أولًا: إنشاء Course

نفّذ `POST /api/Courses` بهذا المثال:

```json
{
  "courseName": "Database Systems",
  "courseCode": "DB101",
  "teacherId": 1
}
```

احتفظ بالـ `id` الناتج.

### ثانيًا: إنشاء Lecture

نفّذ `POST /api/Lectures` مع وضع رقم الـ Course الصحيح:

```json
{
  "courseId": 1,
  "lectureDate": "2026-09-15T00:00:00",
  "startTime": "09:00:00",
  "endTime": "10:30:00"
}
```

سيُنشئ النظام `QRCode` تلقائيًا.

### ثالثًا: إثبات العمليات

اعرض للدكتور تنفيذ:

1. `GET` لعرض البيانات.
2. `GET /{id}` لعرض سجل محدد.
3. `PUT /{id}` لتعديل السجل.
4. `DELETE /{id}` لحذف السجل.

وبالنسبة إلى `Lecture` يمكن عرض `activate` و`deactivate` أيضًا.

## ملاحظة مهمة

المشروع يستهدف `.NET Core 2.2`، لذلك يجب تشغيله ببيئة متوافقة مع المشروع أو ترقية جميع حزم ومشروعات الحل إلى إصدار أحدث بشكل موحّد. كما أن `appsettings.json` مضبوط حاليًا على SQL Server المحلي؛ يجب تغيير Connection String إذا كان اسم الخادم مختلفًا.
