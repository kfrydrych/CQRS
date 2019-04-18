using System.Collections.Generic;

namespace CQRS.Events.Default
{
    public class ActionImposibleEvent : IEvent
    {
        public ActionImposibleEvent(IEnumerable<string> errors)
        {
            Errors = errors;
        }

        public IEnumerable<string> Errors { get; }
    }
}
