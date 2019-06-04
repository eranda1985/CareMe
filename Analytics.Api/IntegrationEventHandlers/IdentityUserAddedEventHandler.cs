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

		public IdentityUserAddedEventHandler(ILogger<IdentityUserAddedEventHandler> logger)
		{
			_logger = logger;
		}

		public Task HandleEvent(IntegrationEvent @event)
		{
			throw new NotImplementedException();
		}
	}
}
