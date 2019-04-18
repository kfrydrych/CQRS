namespace CQRS.Events
{
    public interface IHandleEvent
    {
    }

    public interface IHandleEvent<in TEvent> : IHandleEvent
        where TEvent : IEvent
    {
        void Handle(TEvent @event);
    }
}
