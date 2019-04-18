namespace CQRS.Events.Default
{
    public class CommandCompletedEvent : IEvent
    {
        public CommandCompletedEvent(string commandName)
        {
            CommandName = commandName;
        }

        public string CommandName { get; }
    }
}
