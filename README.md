# EasyRez - Modern Rezervasyon Yönetim Sistemi

<div align="center">

![.NET](https://img.shields.io/badge/.NET-8.0-512BD4?style=for-the-badge&logo=.net&logoColor=white)
![PostgreSQL](https://img.shields.io/badge/PostgreSQL-316192?style=for-the-badge&logo=postgresql&logoColor=white)
![Entity Framework](https://img.shields.io/badge/Entity_Framework-512BD4?style=for-the-badge&logo=.net&logoColor=white)
![Swagger](https://img.shields.io/badge/Swagger-85EA2D?style=for-the-badge&logo=swagger&logoColor=black)

**Modern, ölçeklenebilir ve kullanıcı dostu rezervasyon yönetim sistemi**

[🚀 Hızlı Başlangıç](#-hızlı-başlangıç) • [📋 Özellikler](#-özellikler) • [🏗️ Mimari](#️-mimari) • [🔧 Kurulum](#-kurulum) • [📚 Dokümantasyon](#-dokümantasyon)

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

### 1️⃣ Projeyi Klonlayın

```bash
git clone https://github.com/yourusername/EasyRez.git
cd EasyRez
```

### 2️⃣ Veritabanını Hazırlayın

```sql
-- PostgreSQL'de veritabanı oluşturun
CREATE DATABASE easyrez;
CREATE DATABASE easyrez_dev;
```

### 3️⃣ Konfigürasyon

`EasyRez.Api/appsettings.json` dosyasını düzenleyin:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Port=5432;User Id=postgres;Password=your_password;Database=easyrez"
  }
}
```

### 4️⃣ Migration ve Veritabanı

```bash
# Migration oluşturun
dotnet ef migrations add InitialCreate --project EasyRez.Infrastructure --startup-project EasyRez.Api

# Veritabanını güncelleyin
dotnet ef database update --project EasyRez.Infrastructure --startup-project EasyRez.Api
```

### 5️⃣ Uygulamayı Çalıştırın

```bash
cd EasyRez.Api
dotnet run
```

### 6️⃣ API'yi Test Edin

Tarayıcınızda [http://localhost:5000](http://localhost:5000) adresine gidin ve Swagger UI'ı kullanarak API'yi test edin.

## 📚 API Dokümantasyonu

### 🔗 Endpoint'ler

#### Customer (Müşteri) Endpoints
| Method | Endpoint | Açıklama |
|--------|----------|----------|
| `GET` | `/api/customer` | Tüm müşterileri listele |
| `GET` | `/api/customer/{id}` | Müşteri detayını getir |
| `POST` | `/api/customer` | Yeni müşteri oluştur |
| `PUT` | `/api/customer/{id}` | Müşteri bilgilerini güncelle |
| `DELETE` | `/api/customer/{id}` | Müşteri sil |

#### Service (Hizmet) Endpoints
| Method | Endpoint | Açıklama |
|--------|----------|----------|
| `GET` | `/api/service` | Tüm hizmetleri listele |
| `GET` | `/api/service/{id}` | Hizmet detayını getir |
| `GET` | `/api/service/customer/{customerId}` | Müşteriye ait hizmetleri listele |
| `POST` | `/api/service` | Yeni hizmet oluştur |
| `PUT` | `/api/service/{id}` | Hizmet bilgilerini güncelle |
| `DELETE` | `/api/service/{id}` | Hizmet sil |

#### Appointment (Randevu) Endpoints
| Method | Endpoint | Açıklama |
|--------|----------|----------|
| `GET` | `/api/appointment` | Tüm randevuları listele |
| `GET` | `/api/appointment/{id}` | Randevu detayını getir |
| `GET` | `/api/appointment/customer/{customerId}` | Müşteriye ait randevuları listele |
| `POST` | `/api/appointment` | Yeni randevu oluştur |
| `PUT` | `/api/appointment/{id}` | Randevu bilgilerini güncelle |
| `DELETE` | `/api/appointment/{id}` | Randevu sil |

### 📝 Örnek Request/Response

#### Müşteri Oluşturma
```http
POST /api/customer
Content-Type: application/json

{
  "fullName": "Ahmet Yılmaz",
  "phoneNumber": "+90 555 123 4567",
  "email": "ahmet@example.com"
}
```

#### Randevu Oluşturma
```http
POST /api/appointment
Content-Type: application/json

{
  "customerId": "123e4567-e89b-12d3-a456-426614174000",
  "serviceId": "123e4567-e89b-12d3-a456-426614174001",
  "date": "2024-01-15T10:00:00Z",
  "notes": "İlk randevu"
}
```

## 🗄️ Veritabanı Şeması

### Customer (Müşteri)
```sql
CREATE TABLE Customers (
    Id UUID PRIMARY KEY,
    FullName VARCHAR(255) NOT NULL,
    PhoneNumber VARCHAR(20) NOT NULL,
    Email VARCHAR(255),
    CreatedAt TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    UpdatedAt TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);
```

### Service (Hizmet)
```sql
CREATE TABLE Services (
    Id UUID PRIMARY KEY,
    Name VARCHAR(255) NOT NULL,
    CustomerId UUID REFERENCES Customers(Id),
    Description TEXT,
    Duration INTERVAL,
    ReminderAfter INTERVAL,
    CreatedAt TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    UpdatedAt TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);
```

### Appointment (Randevu)
```sql
CREATE TABLE Appointments (
    Id UUID PRIMARY KEY,
    CustomerId UUID REFERENCES Customers(Id),
    ServiceId UUID REFERENCES Services(Id),
    Date TIMESTAMP NOT NULL,
    Notes TEXT,
    Status VARCHAR(50) DEFAULT 'Scheduled',
    CreatedAt TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    UpdatedAt TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);
```

## 🔧 Geliştirme

### Yeni Entity Ekleme

1. **Domain Katmanı** - Entity oluşturun
```csharp
// EasyRez.Domain/Reservation/NewEntity.cs
public class NewEntity : Entity<Guid>
{
    public string Name { get; private set; }
    
    private NewEntity() { }
    
    public static NewEntity Create(string name)
    {
        return new NewEntity { Name = name };
    }
}
```

2. **Infrastructure Katmanı** - Configuration ekleyin
```csharp
// EasyRez.Infrastructure/Persistence/Configurations/Reservation/NewEntityConfig.cs
internal class NewEntityConfig : IEntityTypeConfiguration<NewEntity>
{
    public void Configure(EntityTypeBuilder<NewEntity> builder)
    {
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Name).IsRequired().HasMaxLength(255);
    }
}
```

3. **Application Katmanı** - DTO ve Handler oluşturun
```csharp
// EasyRez.Application/Reservation/Commands/NewEntityCommands/CreateNewEntityCommand.cs
public record CreateNewEntityCommand(string Name) : IRequest<ErrorOr<Guid>>;

public class CreateNewEntityCommandHandler : IRequestHandler<CreateNewEntityCommand, ErrorOr<Guid>>
{
    private readonly IRepository<NewEntity, Guid> _repository;

    public async Task<ErrorOr<Guid>> Handle(CreateNewEntityCommand request, CancellationToken cancellationToken)
    {
        var entity = NewEntity.Create(request.Name);
        await _repository.AddAsync(entity, cancellationToken);
        await _repository.SaveChangesAsync(cancellationToken);
        return entity.Id;
    }
}
```

4. **API Katmanı** - Controller ekleyin
```csharp
// EasyRez.Api/Controllers/Reservation/NewEntityController.cs
[ApiController]
[Route("api/[controller]")]
public class NewEntityController : ControllerBase
{
    private readonly ISender _sender;

    public NewEntityController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateNewEntityCommand command)
    {
        var result = await _sender.Send(command);
        return result.Match(
            id => Ok(id),
            Problem
        );
    }
}
```

5. **Migration Oluşturun**
```bash
dotnet ef migrations add AddNewEntity --project EasyRez.Infrastructure --startup-project EasyRez.Api
dotnet ef database update --project EasyRez.Infrastructure --startup-project EasyRez.Api
```

### Migration Komutları

```bash
# Yeni migration oluştur
dotnet ef migrations add MigrationName --project EasyRez.Infrastructure --startup-project EasyRez.Api

# Veritabanını güncelle
dotnet ef database update --project EasyRez.Infrastructure --startup-project EasyRez.Api

# Migration'ları listele
dotnet ef migrations list --project EasyRez.Infrastructure --startup-project EasyRez.Api

# Migration'ı geri al
dotnet ef database update PreviousMigrationName --project EasyRez.Infrastructure --startup-project EasyRez.Api

# Migration'ı kaldır
dotnet ef migrations remove --project EasyRez.Infrastructure --startup-project EasyRez.Api
```

## 🧪 Test

### Unit Testler
```bash
# Tüm testleri çalıştır
dotnet test

# Belirli bir test projesini çalıştır
dotnet test EasyRez.Tests/

# Coverage ile test çalıştır
dotnet test --collect:"XPlat Code Coverage"
```

### Integration Testler
```bash
# Integration testleri çalıştır
dotnet test EasyRez.IntegrationTests/
```

## 📦 Deployment

### Docker ile Deployment

```dockerfile
# Dockerfile
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["EasyRez.Api/EasyRez.Api.csproj", "EasyRez.Api/"]
COPY ["EasyRez.Application/EasyRez.Application.csproj", "EasyRez.Application/"]
COPY ["EasyRez.Infrastructure/EasyRez.Infrastructure.csproj", "EasyRez.Infrastructure/"]
COPY ["EasyRez.Domain/EasyRez.Domain.csproj", "EasyRez.Domain/"]
COPY ["EasyRez.Core/EasyRez.Core.csproj", "EasyRez.Core/"]
RUN dotnet restore "EasyRez.Api/EasyRez.Api.csproj"
COPY . .
WORKDIR "/src/EasyRez.Api"
RUN dotnet build "EasyRez.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "EasyRez.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "EasyRez.Api.dll"]
```

```bash
# Docker image oluştur
docker build -t easyrez .

# Container çalıştır
docker run -p 8080:80 easyrez
```

### Environment Variables

```bash
# Production
ASPNETCORE_ENVIRONMENT=Production
ConnectionStrings__DefaultConnection=your_production_connection_string

# Development
ASPNETCORE_ENVIRONMENT=Development
ConnectionStrings__DefaultConnection=your_development_connection_string
```

## 🤝 Katkıda Bulunma

1. Fork yapın
2. Feature branch oluşturun (`git checkout -b feature/amazing-feature`)
3. Değişikliklerinizi commit edin (`git commit -m 'Add amazing feature'`)
4. Branch'inizi push edin (`git push origin feature/amazing-feature`)
5. Pull Request oluşturun

### Kod Standartları

- **C# Coding Conventions** - Microsoft'un C# kodlama standartlarını takip edin
- **Clean Code** - SOLID prensiplerini uygulayın
- **Unit Tests** - Yeni özellikler için test yazın
- **Documentation** - Kod dokümantasyonu ekleyin

## 📄 Lisans

Bu proje MIT lisansı altında lisanslanmıştır. Detaylar için [LICENSE](LICENSE) dosyasına bakın.

## 🆘 Destek

- 📧 **Email**: support@easyrez.com
- 💬 **Discord**: [EasyRez Community](https://discord.gg/easyrez)
- 📖 **Dokümantasyon**: [docs.easyrez.com](https://docs.easyrez.com)
- 🐛 **Bug Report**: [GitHub Issues](https://github.com/yourusername/EasyRez/issues)

## 🙏 Teşekkürler

Bu proje aşağıdaki açık kaynak projelerden ilham almıştır:

- [Clean Architecture Template](https://github.com/amantinband/clean-architecture)
- [Ardalis.Specification](https://github.com/ardalis/Specification)
- [ErrorOr](https://github.com/amantinband/error-or)

---

<div align="center">

**⭐ Bu projeyi beğendiyseniz yıldız vermeyi unutmayın!**

Made with ❤️ by [Your Name]

</div>

