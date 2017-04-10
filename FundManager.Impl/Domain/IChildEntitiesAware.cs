namespace FundManager.Impl.Domain
{
    public interface IChildEntitiesAware<TEvent>
        where TEvent:IEvent
    {
        void RegisterChildEventProvider(IEntityEventProvider<TEvent> entityEventProvider);
    }
}