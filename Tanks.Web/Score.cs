using System;
using System.IO;
using System.Net;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Threading;
using System.Diagnostics;
using Tanks.Web.GameObjects;

namespace Tanks.Web
{
    public class Score
    {
        public string UserName { get; set; }
        public string UserUrl { get; set; }
        public DateTime GameDate { get; set; }
        public string Location { get; set; }
        public bool Success { get; set; }
        public string ResultMessage { get; set; }
        public Game Game { get; set; }

        public void Submit()
        {
            int shots = 0; 
            int kills= 0;
            int hitsTaken = 0;
            int powerupsHit= 0; 
            int projectilesHit= 0; 
            int enemiesHit= 0; 
            int enemiesFlipped= 0; 
            int rounds= this.Game.Rounds.Count; 
            int maxHP= this.Game.CurrentRound.UserTank.MaxHitPoints;
            int rateOfFire = this.GetRateOfFire();
            int damage = this.GetDamage();
            int accuracy= this.GetAccuracy();

            foreach (Round round in Game.Rounds)
            {
                shots += round.ShotsFired;
                kills += round.Kills;
                powerupsHit += round.PowerupsHit;
                projectilesHit += round.ProjectilesHit;
                enemiesHit += round.VehiclesHit;
                enemiesFlipped += round.EnemiesFlippedOver;
                hitsTaken += round.HitsTaken;
            }

            double shotPercent = (double)enemiesHit / (double)shots;


            string query = "UserName={0}&GameDate={1}&UserUrl={2}&Score={3}&Location={4}" +
                "&Shots={5}&Kills={6}&ShotPercent={7}&PowerupsHit={8}&Hits={9}&Flipped={10}" +
                "&Rounds={11}&MaxHP={12}&RateOfFire={13}&Damage={14}&Accuracy={15}&HitsTaken={16}&ProjectilesHit={17}";
            query = string.Format(query, this.UserName, this.GameDate.ToString(), this.UserUrl, 
                this.Game.Score.ToString(), this.Location, shots.ToString(), kills.ToString(), shotPercent.ToString(),
                powerupsHit.ToString(), enemiesHit.ToString(), enemiesFlipped.ToString(), rounds.ToString(),
                maxHP.ToString(), rateOfFire.ToString(), damage.ToString(), accuracy.ToString(), hitsTaken.ToString(),
                projectilesHit.ToString());

            Uri baseUri = System.Windows.Browser.HtmlPage.Document.DocumentUri;
            int index = baseUri.AbsoluteUri.LastIndexOf("/");
            string start = baseUri.AbsoluteUri.Substring(0, index);


            Uri uri = new Uri(start + "/SubmitScore.aspx?" + query);


            WebClient client = new WebClient();
            client.DownloadProgressChanged += new DownloadProgressChangedEventHandler(client_DownloadProgressChanged);
            client.DownloadStringCompleted += new DownloadStringCompletedEventHandler(client_DownloadStringCompleted);
            client.DownloadStringAsync(uri);
        }

        int GetRateOfFire()
        {
            int count = 0;
            foreach (Powerup powerup in this.Game.Player.Powerups)
            {
                if (powerup is RateOfFirePowerup)
                {
                    count++;
                }
            }
            return count;
        }

        int GetDamage()
        {
            int count = 0;
            foreach (Powerup powerup in this.Game.Player.Powerups)
            {
                if (powerup is DamagePowerup)
                {
                    count++;
                }
            }
            return count;
        }

        int GetAccuracy()
        {
            int count = 0;
            foreach (Powerup powerup in this.Game.Player.Powerups)
            {
                if (powerup is AccuracyPowerup)
                {
                    count++;
                }
            }
            return count;
        }


        void client_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            if (this.ProgressChanged != null)
            {
                this.ProgressChanged(this, new ProgressEventArgs(e.ProgressPercentage));
            }
        }

        void client_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            try
            {
                if (e.Result.Contains("Success"))
                {
                    this.Success = true;
                }
                else
                {
                    this.Success = false;
                    int index = e.Result.IndexOf("<message>") + 9;
                    int endIndex = e.Result.IndexOf("</message>");
                    this.ResultMessage = e.Result.Substring(index, endIndex - index);

                }
            }
            catch (Exception ex)
            {
                this.Success = false;
                this.ResultMessage = "An exception occurred.  " + ex.Message;
            }

            if (this.SubmissionCompleted != null)
            {
                this.SubmissionCompleted(this, EventArgs.Empty);
            }

        }

        public event EventHandler SubmissionCompleted;
        public event EventHandler<ProgressEventArgs> ProgressChanged;
      

    }

    public class ProgressEventArgs : EventArgs{
        public ProgressEventArgs(int percent){
            this.Percent = percent;
        }

        public int Percent{get; set; }
    
    }

  

}
