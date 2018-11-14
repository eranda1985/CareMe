﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CareMe.IntegrationService
{
    public interface IIntegrationEventHandler
    {
        Task HandleEvent(IntegrationEvent @event);
    }
}
