using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Tanks.Web.GameObjects;

namespace Tanks.Web.UI
{
    public partial class ScoringSummary : UserControl
    {
        public ScoringSummary()
        {
            InitializeComponent();
            this.Loaded += new RoutedEventHandler(ScoringSummary_Loaded);
        }

        void ScoringSummary_Loaded(object sender, RoutedEventArgs e)
        {
            this.closeButton.Click += new RoutedEventHandler(closeButton_Click);
        }

        void closeButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.Closed != null)
            {
                this.Closed(this, EventArgs.Empty);
            }
        }

        public void Load(Round round)
        {
            this.titleText.Text = "Round Scoring Summary";
            this.shotsFired.Text = round.ShotsFired.ToString();
            this.shotsHit.Text = round.VehiclesHit.ToString();
            this.shotsHitScore.Text = (round.VehiclesHit * Scoring.PointsPerVehicleHit).ToString();
            float percentage = (float)round.VehiclesHit / (float)round.ShotsFired * 100f;
            this.shotPercentage.Text = percentage.ToString("#0.00");
            this.shotPercentageScore.Text = ((int)percentage * Scoring.PointsPerShotPercent).ToString();
            this.kills.Text = round.Kills.ToString();
            this.killsScore.Text = (round.Kills * Scoring.PointsPerKill).ToString();
            this.hitsTaken.Text= round.HitsTaken.ToString();
            this.hitsTakenScore.Text = (round.HitsTaken * (Scoring.PointsPerHitAgainst)).ToString();
            this.powerupsHit.Text = round.PowerupsHit.ToString();
            this.powerupsHitScore.Text = (round.PowerupsHit * (Scoring.PointsPerPowerupHit)).ToString();
            this.projectilesHit.Text = round.ProjectilesHit.ToString();
            this.projectilesHitScore.Text = (round.ProjectilesHit * (Scoring.PointsPerProjectileHit)).ToString();
            this.enemiesFlipped.Text = round.EnemiesFlippedOver.ToString();
            this.enemiesFlippedScore.Text = (round.EnemiesFlippedOver * (Scoring.PointsPerFlippedEnemy)).ToString();
            if (round.Pass)
            {
                this.bonusScore.Text = Scoring.PointsPerRound.ToString();
            }
            else
            {
                this.bonusScore.Text = "0";
            }

            this.totalScore.Text = round.Score.ToString();
        }

        public event EventHandler Closed;

    }
}
