# EasyRez - Modern Rezervasyon Yönetim Sistemi

<div align="center">

![.NET](https://img.shields.io/badge/.NET-8.0-512BD4?style=for-the-badge&logo=.net&logoColor=white)
![PostgreSQL](https://img.shields.io/badge/PostgreSQL-316192?style=for-the-badge&logo=postgresql&logoColor=white)
![Entity Framework](https://img.shields.io/badge/Entity_Framework-512BD4?style=for-the-badge&logo=.net&logoColor=white)
![Swagger](https://img.shields.io/badge/Swagger-85EA2D?style=for-the-badge&logo=swagger&logoColor=black)

**Modern, ölçeklenebilir ve kullanıcı dostu rezervasyon yönetim sistemi**

</div>

---

## 📋 Özellikler

### ✨ Temel Özellikler
- 🎯 **Müşteri Yönetimi** - Müşteri bilgileri ve iletişim detayları
- 📅 **Randevu Sistemi** - Esnek randevu oluşturma ve yönetimi
- 🛠️ **Hizmet Yönetimi** - Özelleştirilebilir hizmet tanımları
- 🔔 **Hatırlatma Sistemi** - Otomatik randevu hatırlatmaları
- 📊 **Raporlama** - Detaylı analiz ve raporlar

### 🏛️ Teknik Özellikler
- ✅ **Clean Architecture** - Sürdürülebilir ve test edilebilir kod yapısı
- ✅ **CQRS Pattern** - Command/Query ayrımı ile performans optimizasyonu
- ✅ **Specification Pattern** - Karmaşık sorgular için esnek yapı
- ✅ **Domain Events** - Loose coupling ile event-driven mimari
- ✅ **Error Handling** - Kapsamlı hata yönetimi
- ✅ **Validation** - FluentValidation ile güçlü doğrulama

## 🏗️ Mimari

### Clean Architecture Katmanları

```
EasyRez/
├── 🎯 EasyRez.Domain/          # İş mantığı ve domain modelleri
├── ⚙️ EasyRez.Application/     # Use case'ler ve business logic
├── 🔧 EasyRez.Infrastructure/  # Veritabanı ve external services
├── 🌐 EasyRez.Api/            # HTTP API endpoints
└── 🔌 EasyRez.Core/           # Temel yapılar ve events
```

### Teknoloji Stack

| Katman | Teknoloji | Açıklama |
|--------|-----------|----------|
| **Framework** | .NET 8.0 | Modern, performanslı framework |
| **ORM** | Entity Framework Core | Güçlü ORM ve migration desteği |
| **Database** | PostgreSQL | Güvenilir ve ölçeklenebilir veritabanı |
| **CQRS** | MediatR | Command/Query pattern implementasyonu |
| **Mapping** | AutoMapper | Object-to-object mapping |
| **Specification** | Ardalis.Specification | Karmaşık sorgular için pattern |
| **Validation** | FluentValidation | Güçlü validation framework |
| **Error Handling** | ErrorOr | Functional error handling |
| **Documentation** | Swagger/OpenAPI | API dokümantasyonu |

## 🚀 Hızlı Başlangıç

### Gereksinimler

- [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [PostgreSQL 12+](https://www.postgresql.org/download/)
- [Visual Studio 2022](https://visualstudio.microsoft.com/) veya [VS Code](https://code.visualstudio.com/)

### Kurulum Adımları

1. **Projeyi Klonlayın**
   ```bash
   git clone https://github.com/yourusername/EasyRez.git
   cd EasyRez
   ```

2. **Veritabanını Hazırlayın**
   - PostgreSQL'de `easyrez` ve `easyrez_dev` veritabanlarını oluşturun

3. **Konfigürasyon**
   - `EasyRez.Api/appsettings.json` dosyasında connection string'i düzenleyin

4. **Migration ve Veritabanı**
   ```bash
   dotnet ef migrations add InitialCreate --project EasyRez.Infrastructure --startup-project EasyRez.Api
   dotnet ef database update --project EasyRez.Infrastructure --startup-project EasyRez.Api
   ```

5. **Uygulamayı Çalıştırın**
   ```bash
   cd EasyRez.Api
   dotnet run
   ```

6. **API'yi Test Edin**
   - Tarayıcınızda [http://localhost:5000](http://localhost:5000) adresine gidin
   - Swagger UI'ı kullanarak API'yi test edin

## 📚 API Endpoints

### Customer (Müşteri) Endpoints
- `GET /api/customer` - Tüm müşterileri listele
- `GET /api/customer/{id}` - Müşteri detayını getir
- `POST /api/customer` - Yeni müşteri oluştur
- `PUT /api/customer/{id}` - Müşteri bilgilerini güncelle
- `DELETE /api/customer/{id}` - Müşteri sil

### Service (Hizmet) Endpoints
- `GET /api/service` - Tüm hizmetleri listele
- `GET /api/service/{id}` - Hizmet detayını getir
- `GET /api/service/customer/{customerId}` - Müşteriye ait hizmetleri listele
- `POST /api/service` - Yeni hizmet oluştur
- `PUT /api/service/{id}` - Hizmet bilgilerini güncelle
- `DELETE /api/service/{id}` - Hizmet sil

### Appointment (Randevu) Endpoints
- `GET /api/appointment` - Tüm randevuları listele
- `GET /api/appointment/{id}` - Randevu detayını getir
- `GET /api/appointment/customer/{customerId}` - Müşteriye ait randevuları listele
- `POST /api/appointment` - Yeni randevu oluştur
- `PUT /api/appointment/{id}` - Randevu bilgilerini güncelle
- `DELETE /api/appointment/{id}` - Randevu sil

## 🗄️ Veritabanı Şeması

### Ana Tablolar
- **Customers** - Müşteri bilgileri (Id, FullName, PhoneNumber, Email, CreatedAt, UpdatedAt)
- **Services** - Hizmet tanımları (Id, Name, CustomerId, Description, Duration, ReminderAfter, CreatedAt, UpdatedAt)
- **Appointments** - Randevu kayıtları (Id, CustomerId, ServiceId, Date, Notes, Status, CreatedAt, UpdatedAt)

## 🔧 Geliştirme

### Yeni Entity Ekleme Süreci
1. **Domain Katmanı** - Entity oluştur
2. **Infrastructure Katmanı** - Configuration ekle
3. **Application Katmanı** - DTO ve Handler oluştur
4. **API Katmanı** - Controller ekle
5. **Migration** oluştur ve veritabanını güncelle

### Migration Komutları
- `dotnet ef migrations add MigrationName` - Yeni migration oluştur
- `dotnet ef database update` - Veritabanını güncelle
- `dotnet ef migrations list` - Migration'ları listele
- `dotnet ef migrations remove` - Son migration'ı kaldır

## 🧪 Test

### Test Komutları
```bash
dotnet test                    # Tüm testleri çalıştır
dotnet test --collect:"XPlat Code Coverage"  # Coverage ile test çalıştır
```



### Environment Variables
- `ASPNETCORE_ENVIRONMENT` - Ortam belirleme
- `ConnectionStrings__DefaultConnection` - Veritabanı bağlantısı

## 🤝 Katkıda Bulunma

1. Fork yapın
2. Feature branch oluşturun
3. Değişikliklerinizi commit edin
4. Branch'inizi push edin
5. Pull Request oluşturun

### Kod Standartları
- **C# Coding Conventions** - Microsoft'un C# kodlama standartlarını takip edin
- **Clean Code** - SOLID prensiplerini uygulayın
- **Unit Tests** - Yeni özellikler için test yazın
- **Documentation** - Kod dokümantasyonu ekleyin

## 📄 Lisans

Bu proje MIT lisansı altında lisanslanmıştır.

## 🆘 Destek

- 📧 **Email**: support@easyrez.com
- 💬 **Discord**: [EasyRez Community](https://discord.gg/easyrez)
- 📖 **Dokümantasyon**: [docs.easyrez.com](https://docs.easyrez.com)
- 🐛 **Bug Report**: [GitHub Issues](https://github.com/yourusername/EasyRez/issues)

---

<div align="center">

**⭐ Bu projeyi beğendiyseniz yıldız vermeyi unutmayın!**

Made with ❤️ by [Your Name]

</div>

