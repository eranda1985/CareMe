using System;
using System.Collections.Generic;
using System.Text;

namespace CareMe.IntegrationService
{
    public class IdentityUserAddedEvent: IntegrationEvent
    {
        public string Username { get; private set; }
        public string UserSecret { get; private set; }

        public IdentityUserAddedEvent(string userName, string secret)
        {
            Username = Username;
            UserSecret = secret;
        }
    }
}
