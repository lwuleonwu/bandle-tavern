using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace BandleTavern.Lcu.Cache
{
    public static class UserSummoner
    {
        public static LcuApiTavern.Plugins.LolSummoner.V1.CurrentSummoner ActiveSummoner { get; set; }

        /// <summary>
        /// Tries to Get / return the currently active summoner and place the data in cache.
        /// </summary>
        /// <returns>Currently Active Summoner</returns>
        public static LcuApiTavern.Plugins.LolSummoner.V1.CurrentSummoner GetActiveSummoner()
        {
            LcuApiTavern.Plugins.LolSummoner.V1.CurrentSummoner summoner = LcuApiTavern.Plugins.LolSummoner.V1.CurrentSummoner.Get();

            if (summoner != null)
            {
                LcuInteraction.ClientConnectionStatus = true;
                if (summoner.InternalName != "")
                {
                    if (Wpf.Elements.ActiveSummoner.ActiveSummoner.Active != null)
                    {
                        Wpf.Elements.ActiveSummoner.ActiveSummoner.Active.Summoner = summoner;
                    }
                    if (Timer != null)
                    {
                        Timer.Stop();
                    }
                    if ((ActiveSummoner == null) || (summoner.SummonerId != ActiveSummoner.SummonerId))
                    {
                        //Different or new summoner detected.
                    }
                    ActiveSummoner = summoner;
                }
            }

            return ActiveSummoner;
        }

        /// <summary>
        /// Checks in intervals for valid active summoner response.
        /// </summary>
        public static void ListenForSummoner()
        {
            if (GetActiveSummoner() == null)
            {
                if (Timer == null)
                {
                    Timer = new Timer();
                    Timer.Interval = 10000;
                    Timer.Elapsed += TimerElapsed;
                }
                TimerTimeoutCount = 0;
                Timer.Start();
            }
        }

        private static Timer Timer;
        private static int TimerTimeout = 60;
        private static int TimerTimeoutCount = 0;

        private static void TimerElapsed(object o, ElapsedEventArgs e)
        {
            GetActiveSummoner();
            TimerTimeoutCount++;
            if (TimerTimeout < TimerTimeoutCount)
            {
                Timer.Stop();
            }
        }
    }
}
