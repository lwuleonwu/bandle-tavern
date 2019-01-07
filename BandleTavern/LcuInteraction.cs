using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LcuApiTavern;
using System.IO;
using System.Timers;

namespace BandleTavern
{
    public class LcuInteraction
    {
        private Timer TimerFileCheck;
        private MainWindow Master;
        private static LcuInteraction Active;

        /// <summary>
        /// League of Legends Client Directory
        /// </summary>
        public string ClientDirectory
        {
            get => LeagueClient.ClientDirectory;
            set
            {
                LeagueClient.ClientDirectory = value;
            }
        }

        /// <summary>
        /// Tries to load lockfile, and checks for a successful load.
        /// </summary>
        public void TryLockfileLoad()
        {
            if (ClientDirectory != "")
            {
                LockFile OldLockFile = LeagueClient.LockFile;

                LeagueClient.GetLockFile();
                if (OldLockFile != LeagueClient.LockFile)
                {
                    // new lockfile detected
                    TimerFileCheck.Stop();
                    Master.InitModules();
                }
            }
        }
        
        /// <summary>
        /// Assigns master and triggers timer to wait for lockfile
        /// </summary>
        /// <param name="master">MainWindow Ref</param>
        public void InitLcuApi(MainWindow master)
        {
            Master = master;
            Active = this;
            TimerFileCheck = new Timer();
            TimerFileCheck.Interval = 5000;
            TimerFileCheck.Elapsed += TimerFileCheckElapsed;
            ListenForLockfile();
        }
        /// <summary>
        /// Makes sure that lockfile timer is stopped and disposed.
        /// </summary>
        public void DeInitLcuApi()
        {
            TimerFileCheck.Stop();
            TimerFileCheck.Dispose();
        }
        /// <summary>
        /// Starts timer in order to wait for lockfile.
        /// </summary>
        public static void ListenForLockfile()
        {
            Active.TimerFileCheck.Start();
        }

        private void TimerFileCheckElapsed(object sender, ElapsedEventArgs e)
        {
            TryLockfileLoad();
        }
    }
}
