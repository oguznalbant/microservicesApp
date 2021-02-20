namespace ECom.Order.Core.Entities.Base
{
    //Base Entity Interface for Id Field generic
    public interface IEntityBase<TId>
    {
        TId Id { get; }
    }
}