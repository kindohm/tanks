using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace Tanks.Web
{
    public class Scoring
    {
        public const int PointsPerVehicleHit = 10;
        public const int PointsPerProjectileHit = 50;
        public const int PointsPerPowerupHit = 100;
        public const int PointsPerKill = 500;
        public const int PointsPerShotPercent = 200;
        public const int PointsPerHitAgainst = -20;
        public const int PointsPerRound = 10000;
        public const int PointsPerFlippedEnemy = 5000;
    }
}
