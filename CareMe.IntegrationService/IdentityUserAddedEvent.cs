using System;
using System.Collections.Generic;
using System.Text;

namespace CareMe.IntegrationService
{
    public class IdentityUserAddedEvent: IntegrationEvent
    {
        public string Username { get; set; }
        public string UserSecret { get; set; }

        public IdentityUserAddedEvent(string userName, string secret)
        {
            Username = userName;
            UserSecret = secret;
        }
    }
}
