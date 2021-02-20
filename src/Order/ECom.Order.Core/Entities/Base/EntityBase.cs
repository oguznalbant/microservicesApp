namespace ECom.Order.Core.Entities.Base
{
    // Abstract Entity Base Class with Generic Type
    public abstract class EntityBase<TId> : IEntityBase<TId>
    {
        // Id can be override by the implementing class with virtual, Id parameter can be set where implemented class with EntityBase
        public virtual TId Id { get; protected set; }

        int? _requestedHashCode { get; set; }

        public bool IsTransient()
        {
            return Id.Equals(default(TId));
        }

        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is EntityBase<TId>))
                return false;

            if (ReferenceEquals(this, obj))
                return true;

            if (GetType() != obj.GetType())
                return false;

            var item = (EntityBase<TId>)obj;

            if (item.IsTransient() || IsTransient())
                return false;
            else
                return item == this;
        }

        public override int GetHashCode()
        {
            if (!IsTransient())
            {
                if (!_requestedHashCode.HasValue)
                    _requestedHashCode = Id.GetHashCode() ^ 31; // XOR for random distribution (http://blogs.msdn.com/b/ericlippert/archive/2011/02/28/guidelines-and-rules-for-gethashcode.aspx)

                return _requestedHashCode.Value;
            }
            else
                return base.GetHashCode();
        }

        public static bool operator ==(EntityBase<TId> left, EntityBase<TId> right)
        {
            if (Equals(left, null))
                return Equals(right, null) ? true : false;
            else
                return left.Equals(right);
        }

        public static bool operator !=(EntityBase<TId> left, EntityBase<TId> right)
        {
            return !(left == right);
        }
    }
}
