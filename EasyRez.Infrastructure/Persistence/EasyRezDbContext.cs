using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using EasyRez.Core.Events;
using EasyRez.Core;
using Microsoft.EntityFrameworkCore;
namespace EasyRez.Infrastructure.Persistence
{
    public class EasyRezDbContext:DbContext
    {
        private readonly string? ConnectionString;

        private readonly IDomainEventDispatcher _dispatcher;
        private EntityBase[] _domainEventEntities = Array.Empty<EntityBase>();

        public EasyRezDbContext(string? connectionString)
        {
            ConnectionString = connectionString;
        }

        public EasyRezDbContext()
        {
        }

        public EasyRezDbContext(DbContextOptions<EasyRezDbContext> options)
            : base(options)
        {
        }



        public EasyRezDbContext(DbContextOptions<EasyRezDbContext> options, IDomainEventDispatcher dispatcher) : base(options)
        {
            _dispatcher = dispatcher;
        }
        public override int SaveChanges()
        {
            if (_dispatcher is not null)
            {
                PreSaveChanges();
                DispatchDomainEvents().GetAwaiter().GetResult();
            }

            var res = base.SaveChanges();
            return res;
        }


        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            if (_dispatcher is not null)
            {
                PreSaveChanges();
                await DispatchDomainEvents();
            }
            var res = await base.SaveChangesAsync(cancellationToken);

            return res;
        }

        private void PreSaveChanges()
        {
            var entries = ChangeTracker.Entries();
            _domainEventEntities = ChangeTracker.Entries<EntityBase>()
                .Select(po => po.Entity)
                .Where(po => po.DomainEvents.Any())
                .ToArray();
        }

        private async Task DispatchDomainEvents()
        {


            foreach (var entity in _domainEventEntities)
            {
                IDomainEvent dev;
                while (entity.DomainEvents.TryTake(out dev))
                {
                    await _dispatcher.Dispatch(dev);
                    await SaveEventToOutbox(dev);
                }
            }
            await base.SaveChangesAsync();
        }

        protected virtual async Task SaveEventToOutbox(IDomainEvent domainEvent)
        {
            //await base.AddAsync(OutboxItem.FromEvent(domainEvent));
        }



        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (ConnectionString != null)
                optionsBuilder.UseNpgsql(ConnectionString, x => x.UseNetTopologySuite());
        }

        private static readonly ValueConverter<DateTime, DateTime> UtcConverter = new ValueConverter<DateTime, DateTime>(convertTo => DateTime.SpecifyKind(convertTo, DateTimeKind.Utc), convertFrom => convertFrom);


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(EasyRezDbContext).Assembly);

            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                foreach (var property in entityType.GetProperties())
                {
                    if (property.ClrType == typeof(DateTime) || property.ClrType == typeof(DateTime?))
                    {
                        property.SetColumnType("timestamp without time zone");
                        property.SetValueConverter(UtcConverter);

                    }
                }
            }

        }

    }
}
