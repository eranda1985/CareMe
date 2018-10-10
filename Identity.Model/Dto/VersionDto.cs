using System;
namespace Identity.Model.Dto
{
    public class VersionDto
    {
        public string VersionHash { get; set; }
        public string VersionNumber { get; set; }
        public bool Enabled { get; set; }
        public string DeviceType { get; set; }
    }
}
