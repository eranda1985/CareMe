using Analytics.Model.Dto;
using Analytics.Model.Services;
using CareMe.IntegrationService;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Analytics.Api.IntegrationEventHandlers
{
	public class IdentityUserAddedEventHandler : IIntegrationEventHandler
	{
		private ILogger<IdentityUserAddedEventHandler> _logger;
		private IService<UserDataDto> _userDataService;

		public IdentityUserAddedEventHandler(
			ILogger<IdentityUserAddedEventHandler> logger,
			IService<UserDataDto> userService)
		{
			_logger = logger;
			_userDataService = userService;
		}

		public async Task HandleEvent(IntegrationEvent @event)
		{
			if (@event is IdentityUserAddedEvent e)
			{
				_ = await ((UserDataService)_userDataService).CreateUser(new UserDataDto
				{
					Secret = e.UserSecret,
					Username = e.Username
				});
			}
		}
	}
}
