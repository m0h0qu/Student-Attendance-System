# كيفية العثور على قاعدة البيانات في SQL Server Management Studio (SSMS)

## المشكلة
قاعدة البيانات تم إنشاؤها بنجاح ولكن قد لا تظهر في SSMS فوراً.

## الحل 1: تحديث قاعدة البيانات في SSMS

1. افتح **SQL Server Management Studio (SSMS)**
2. اتصل بالسيرفر:
   - **Server Name:** اكتب `.` أو `localhost`
   - **Authentication:** Windows Authentication
   - اضغط **Connect**
3. في قائمة **Object Explorer** على اليسار:
   - اضغط بزر الفأرة الأيمن على **Databases**
   - اختر **Refresh**
4. الآن ستجد قاعدة البيانات **StudentAttendanceSystemDb**

---

## الحل 2: التحقق من السيرفر الصحيح

إذا لم تجد قاعدة البيانات، تأكد من أنك متصل بالسيرفر الصحيح:

1. في SSMS، انظر إلى الاسم الموجود في أعلى Object Explorer
2. يجب أن يكون:
   - `.` (نقطة)
   - أو `localhost`
   - أو اسم جهازك

إذا كان السيرفر مختلف، قم بالاتصال بالسيرعر الصحيح:
1. اضغط **Connect** في أعلى SSMS
2. اكتب `.` أو `localhost`
3. اضغط **Connect**

---

## الحل 3: التحقق من وجود قاعدة البيانات باستخدام SQL Query

1. افتح **New Query** في SSMS
2. اكتب هذا الأمر:
```sql
SELECT name FROM sys.databases
```
3. اضغط **Execute**
4. ستجد قاعدة البيانات **StudentAttendanceSystemDb** في القائمة

---

## الحل 4: إعادة إنشاء قاعدة البيانات

إذا لم تجد قاعدة البيانات، قم بإعادة إنشائها:

1. افتح سطر الأوامر في مجلد المشروع
2. شغل هذا الأمر:
```bash
dotnet ef database update --project StudentAttendanceSystem.Infrastructure --startup-project StudentAttendanceSystem.API
```
3. بعد الانتهاء، افتح SSMS وقم بتحديث قائمة Databases

---

## كيفية رؤية الجداول والبيانات

بعد العثور على قاعدة البيانات:

1. قم بتوسيع **StudentAttendanceSystemDb**
2. قم بتوسيع **Tables**
3. ستجد 4 جداول:
   - **Users** - جدول المستخدمين
   - **Courses** - جدول الدورات
   - **Lectures** - جدول المحاضرات
   - **Attendances** - جدول الحضور

4. لرؤية البيانات:
   - اضغط بزر الفأرة الأيمن على أي جدول
   - اختر **Select Top 1000 Rows**

---

## البيانات الموجودة في الجداول

### Users (المستخدمين)
- 2 معلمين
- 3 طلاب

### Courses (الدورات)
- 3 دورات

### Lectures (المحاضرات)
- 3 محاضرات مع QR Codes فريدة

### Attendances (الحضور)
- 2 سجل حضور

---

## إذا لم تنجح كل الحلول

قد يكون السبب أن قاعدة البيانات تم إنشاؤها على سيرفر مختلف.

### الحل النهائي:

1. افتح ملف `StudentAttendanceSystem.API/appsettings.json`
2. اقرأ Connection String:
```json
"DefaultConnection": "Server=localhost;Database=StudentAttendanceSystemDb;Trusted_Connection=True;MultipleActiveResultSets=true"
```
3. في SSMS، اتصل بالسيرفر المذكور في Connection String:
   - **Server:** localhost
   - **Authentication:** Windows Authentication
4. قم بتحديث قائمة Databases

---

## ملاحظة مهمة
قاعدة البيانات تم إنشاؤها بنجاح بناءً على المخرجات التي ظهرت عند تشغيل الأمر. المشكلة فقط في العثور عليها في SSMS.
