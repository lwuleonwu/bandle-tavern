using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LcuApiTavern;
using System.Timers;

namespace BandleTavern.Lcu
{
    public class LcuInteraction
    {
        private static bool _clientConnectionStatus = false;
        private Timer TimerFileCheck;
        private Timer TimerConnectionCheck;
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
            TimerConnectionCheck = new Timer();
            TimerConnectionCheck.Interval = 10000;
            TimerConnectionCheck.Elapsed += TimerConnectionCheckElapsed;
            TimerConnectionCheck.Start();
            ListenForLockfile();
            ClientConnectionCheck();
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
            Active.TryLockfileLoad();
        }

        private void TimerFileCheckElapsed(object sender, ElapsedEventArgs e)
        {
            TryLockfileLoad();
        }

        private void TimerConnectionCheckElapsed(object sender, ElapsedEventArgs e)
        {
            ClientConnectionCheck();
        }

        private void ClientConnectionCheck()
        {
            if (LcuApiTavern.Plugins.LolServiceStatus.V1.LcuStatus.Get() == null)
            {
                //Connection possibly lost
                ClientConnectionStatus = false;
            }
            else
            {
                ClientConnectionStatus = true;
            }
        }



        public static bool ClientConnectionStatus
        {
            get
            {
                return _clientConnectionStatus;
            }
            set
            {
                if (_clientConnectionStatus != value)
                {
                    _clientConnectionStatus = value;
                    if (_clientConnectionStatus)
                    {
                        //connection established
                        if (MainWindow.Active != null)
                        {
                            MainWindow.Active.ClientConnectionErrorVis = System.Windows.Visibility.Collapsed;
                        }
                    }
                    else
                    {
                        //connection lost
                        if (MainWindow.Active != null)
                        {
                            MainWindow.Active.ClientConnectionErrorVis = System.Windows.Visibility.Visible;
                        }
                        ListenForLockfile();
                    }
                }
            }
        }
    }
}
