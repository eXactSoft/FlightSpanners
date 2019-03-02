using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightSpanners.Areas.CommonArea.Services
{
    public interface IAppSetting
    {
        // Return setting value
        string GetSetting(string setting);
    }
}
