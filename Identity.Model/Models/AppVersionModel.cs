using System;
using NPoco;

namespace Identity.Model.Models
{
    [TableName("AppVersion")]
    public class AppVersionModel
    {
        [Column("Id")]
        public long Id { get; set; }
        [Column("VersionHash")]
        public string VersionHash { get; set; }
        [Column("VersionNumber")]
        public string VersionNumber { get; set; }
        [Column("Enabled")]
        public bool Enabled { get; set; }
        [Column("DeviceType")]
        public string DeviceType { get; set; }
    }
}
