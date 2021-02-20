namespace ECom.Order.Core.Entities.Base
{
    // Entity is owned a integer id prop from EntityBase abstract class, later we can say also EntityMongo:EntityBase<Bson> like that
    public abstract class Entity : EntityBase<int>
    {
    }
}
