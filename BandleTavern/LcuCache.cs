using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BandleTavern
{
    public static class LcuCache
    {
        public static LcuApiTavern.Plugins.LolSummoner.V1.CurrentSummoner ActiveSummoner { get; set; }

        /// <summary>
        /// Tries to Get / return the currently active summoner and place the data in cache.
        /// </summary>
        /// <returns>Currently Active Summoner</returns>
        public static LcuApiTavern.Plugins.LolSummoner.V1.CurrentSummoner GetActiveSummoner()
        {
            ActiveSummoner = LcuApiTavern.Plugins.LolSummoner.V1.CurrentSummoner.Get();
            return ActiveSummoner;
        }
    }
}
