using EasyRez.Domain.Jobs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EasyRez.Infrastructure.Persistence.Configurations.Jobs
{
    public class ScheduledTaskConfig : IEntityTypeConfiguration<ScheduledTask>
    {
        public void Configure(EntityTypeBuilder<ScheduledTask> builder)
        {
            // Tablo adını belirleyebilirsiniz (isteğe bağlı)
            builder.ToTable("ScheduledTasks");

            // Primary Key'i (varsa EntityBase'den gelir)
            // Eğer EntityBase'de Id yoksa burada tanımlayın:
            // builder.HasKey(t => t.Id);

            // Gerekli alanları ve uzunlukları tanımlayabilirsiniz
            builder.Property(t => t.UserId)
                .IsRequired();

            builder.Property(t => t.HttpMethod)
                .IsRequired()
                .HasMaxLength(10);

            builder.Property(t => t.Url)
                .IsRequired();

            // Payload null olabilir, o yüzden ayara gerek yok

            builder.Property(t => t.IntervalType)
                .IsRequired();
        }
    }
}