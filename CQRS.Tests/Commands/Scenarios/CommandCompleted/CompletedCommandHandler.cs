using CQRS.Commands;
using System;

namespace CQRS.Tests.Commands.Scenarios.CommandCompleted
{
    public class CompletedCommandHandler : IHandleCommand<CompletedCommand>
    {
        public void Handle(CompletedCommand command)
        {
            Console.WriteLine(nameof(CompletedCommand));
        }
    }
}
