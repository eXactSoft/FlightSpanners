using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace FlightSpanners.Areas.CommonArea.Services
{
    public class AppSetting : IAppSetting
    {
        private IConfiguration _configuration;

        public AppSetting(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string GetSetting(string setting)
        {
            return _configuration[setting];
        }
    }
}
