using System;

namespace AspNetCorePropertyPro.Core.Models
{
    public class Tenant
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string HostName { get; set; }
        public string ConnectionString { get; set; }
    }
}