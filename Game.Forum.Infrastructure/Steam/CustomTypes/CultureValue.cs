using System.Globalization;

namespace Game.Forum.Infrastructure.Steam.CustomTypes
{
    public class CultureValue
    {
        public string CultureShortName { get; set; }
        public string CultureName { get; set; }
        public CultureInfo CultureInfo { get; set; }
    }
}
