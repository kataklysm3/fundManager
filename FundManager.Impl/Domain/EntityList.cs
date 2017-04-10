using System.Collections.Generic;

namespace FundManager.Impl.Domain
{
    public class EntityList<TEntity, TEvent> : List<TEntity>
        where TEntity : IEntityEventProvider<TEvent>
        where TEvent : IEvent
    {
        private readonly IChildEntitiesAware<TEvent> _aggregateRoot;


        public EntityList(IChildEntitiesAware<TEvent> aggregateRoot)
        {
            _aggregateRoot = aggregateRoot;
        }

        public EntityList(IChildEntitiesAware<TEvent> aggregateRoot, int capacity)
            : base(capacity)
        {
            _aggregateRoot = aggregateRoot;
        }

        public EntityList(IChildEntitiesAware<TEvent> aggregateRoot, IEnumerable<TEntity> collection)
            : base(collection)
        {
            _aggregateRoot = aggregateRoot;
        }

        public new void Add(TEntity entity)
        {
            _aggregateRoot.RegisterChildEventProvider(entity);
            base.Add(entity);
        }
    }
}