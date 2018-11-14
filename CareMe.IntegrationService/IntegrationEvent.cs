using System;

namespace CareMe.IntegrationService
{
    public abstract class IntegrationEvent
    {
        public Guid Id { get; }
        public DateTime CreationDate { get; }

        public IntegrationEvent()
        {
            Id = Guid.NewGuid();
            CreationDate = DateTime.UtcNow;
        }
    }
}