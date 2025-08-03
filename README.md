* Projeyi indirin

git clone https://github.com/codewithme13/aidcare-backend-api.git
cd aidcare-backend-api

* PostgreSQL’i başlatın

Docker üzerinden şöyle çalıştırıyorum:

docker run --name aidcare-postgres-new -e POSTGRES_PASSWORD=123456 -p 5432:5432 -d postgres:15

Sonra docker ps ile kontrol ediyorum.


* NuGet paketlerini yükleyin

dotnet restore


* Migration ve veritabanı güncelleme
Migration yoksa oluşturun:

dotnet ef migrations add InitialCreate -p AidCare.DataAccess -s AidCare.API


Sonra veritabanını update edin:


dotnet ef database update -p AidCare.DataAccess -s AidCare.API


* API’yi çalıştırın

   
dotnet run --project AidCare.API

Başarılı çalıştığında şöyle bir çıktı görmelisiniz:
Now listening on: http://localhost:5270

API Örnekleri

Kullanıcı ekleme

POST http://localhost:5270/api/users

Content-Type: application/json

Body:

{

  "tcKimlikNo": "12345678901",
  
  "firstName": "Ahmet",
  
  "lastName": "Yılmaz",
  
  "email": "ahmet@gmail.com",
  
  "phoneNumber": "05551234567",
  
  "dateOfBirth": "1990-01-01"
  
}


Kan şekeri ölçümü ekleme

POST http://localhost:5270/api/bloodglucose


Body:
{
  "userId": 1,
  
  "glucoseValue": 120.5,
  
  "measurementDate": "2025-08-03T20:00:00",
  
  "notes": "Yemek sonrası ölçüm"
  
}


Kullanıcıları listeleme

GET http://localhost:5270/api/users

Veritabanı Tabloları

--Users

Id

TcKimlikNo

FirstName

LastName

Email

PhoneNumber

DateOfBirth

CreatedDate

UpdatedDate

IsDeleted


--BloodGlucoseRecords

Id

UserId

GlucoseValue

MeasurementDate

Notes

CreatedDate

UpdatedDate

IsDeleted
