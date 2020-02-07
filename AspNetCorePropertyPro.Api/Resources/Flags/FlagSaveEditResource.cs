using AspNetCorePropertyPro.Api.Resources.Propertys;

namespace AspNetCorePropertyPro.Api.Resources.Flags
{
    public class FlagSaveEditResource
    {
        public string Email { get; set; }
        public string Name { get; set; }
        public string Reason { get; set; }
        public int PropertyId { get; set; }
    }
}
