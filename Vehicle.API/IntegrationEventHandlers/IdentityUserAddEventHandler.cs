using CareMe.IntegrationService;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vehicle.Model.Dto;
using Vehicle.Model.Services;

namespace Vehicle.Api.IntegrationEventHandlers
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
			var dto = new UserDataDto { Username = e.Username, Secret = e.UserSecret };
            var status = await ((UserdataService)_userDataService).CreateNewUser(dto);
        }
    }
}

