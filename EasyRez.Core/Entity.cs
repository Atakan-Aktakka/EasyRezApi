using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyRez.Core
{
    public abstract class Entity<TId> : EntityBase, IEquatable<Entity<TId>>
        where TId : notnull
    {
        public TId Id { get; protected set; } = default!;

        protected Entity(TId id)
        {
            Id = id;
        }

        protected Entity()
        {
        }

        public static bool operator ==(Entity<TId> a, Entity<TId> b)
        {
            if (a is null && b is null)
            {
                return true;
            }

            if (a is null || b is null)
            {
                return false;
            }

            return a.Equals(b);
        }

        public static bool operator !=(Entity<TId> a, Entity<TId> b) => !(a == b);

        public override bool Equals(object? obj)
        {
            return obj is Entity<TId> entity && Id.Equals(entity.Id);
        }

        public bool Equals(Entity<TId>? other)
        {
            return Equals((object?)other);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

    }
}
