using CareMe.IntegrationService;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RunningData.Api.IntegrationEventHandlers
{
    public class IdentityUserAddEventHandler : IIntegrationEventHandler
    {
        private ILogger<IdentityUserAddEventHandler> _logger;

        public IdentityUserAddEventHandler(ILogger<IdentityUserAddEventHandler> logger)
        {
            _logger = logger;
        }

        public void HandleEvent(IntegrationEvent @event)
        {
            var e = @event as IdentityUserAddedEvent;
            _logger.LogDebug("Handling RabbitMQ subscription for user {0}", e.Username);
        }
    }
}
