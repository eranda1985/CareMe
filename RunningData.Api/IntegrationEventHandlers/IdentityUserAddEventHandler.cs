using CareMe.IntegrationService;
using Microsoft.Extensions.Logging;
using RunningData.Model.Dto;
using RunningData.Model.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RunningData.Api.IntegrationEventHandlers
{
    public class IdentityUserAddEventHandler : IIntegrationEventHandler
    {
        private ILogger<IdentityUserAddEventHandler> _logger;
        private IService<UserDataDto> _userDataService;

        public IdentityUserAddEventHandler(ILogger<IdentityUserAddEventHandler> logger, IService<UserDataDto> userService)
        {
            _logger = logger;
            _userDataService = userService;
        }

        public async Task HandleEvent(IntegrationEvent @event)
        {
            var e = @event as IdentityUserAddedEvent;
			_logger.LogDebug("Handling RabbitMQ subscription for user {0} and secret {1}", e.Username, e.UserSecret);
            var dto = new UserDataDto { Username = e.Username, Secret = e.UserSecret };
            var status = await ((UserDataService)_userDataService).AddNewUser(dto);
        }
    }
}

