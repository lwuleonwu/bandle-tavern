using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LcuApiTavern;

namespace BandleTavern.Lcu
{
    public class LcuInteraction
    {
        private static bool _clientConnectionStatus = false;
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
        /// Assigns master and triggers timer to wait for lockfile
        /// </summary>
        /// <param name="master">MainWindow Ref</param>
        public void InitLcuApi(MainWindow master)
        {
            Master = master;
            Active = this;

            LeagueClient.OnLockFileChange += LockfileChanged;

            LeagueClient.InitDetectionTimer();
        }
        /// <summary>
        /// Makes sure that lockfile timer is stopped and disposed.
        /// </summary>
        public void DeInitLcuApi()
        {
            LeagueClient.OnLockFileChange -= LockfileChanged;
            LeagueClient.DeInitDetectionTimer();
        }

        /// <summary>
        /// Gets the lockfile that was switched to and converts to connection status.
        /// If Successful connection then initialises modules from MainWindow.
        /// </summary>
        /// <param name="e">EventArgs</param>
        private static void LockfileChanged(LockfileEventArgs e)
        {
            if (LeagueClient.LockFile != null)
            {
                ClientConnectionStatus = true;
                MainWindow.Active.InitModules();
            }
            else
            {
                ClientConnectionStatus = false;
            }
        }

        /// <summary>
        /// Keeps Track of the connection status with the client, any change in status will show or hide the 
        /// MainWindow error message to inform the user.
        /// </summary>
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
                    }
                }
            }
        }
    }
}
