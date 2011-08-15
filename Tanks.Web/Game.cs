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
using FarseerGames.FarseerPhysics;
using System.Collections.Generic;
using Tanks.Web.Rounds;
using Tanks.Web.GameObjects;

namespace Tanks.Web
{
    public class Game
    {
        Player player;
        Player computer;
        Round currentRound = null;
        PhysicsSimulator simulator;
        List<Round> rounds;
        int score;
        int previousRoundsScore;
        bool over;

        public Game(PhysicsSimulator simulator)
        {
            this.player = new Player(simulator);
            this.computer = new Player(simulator);
            this.simulator = simulator;
            this.rounds = new List<Round>();
        }

        public List<Round> Rounds
        {
            get { return this.rounds; }
        }

        public Player Player
        {
            get { return this.player; }
        }

        public Player Computer
        {
            get { return this.computer; }
        }

        public Round CurrentRound
        {
            get { return this.currentRound; }
        }

        public bool Over
        {
            get { return this.over; }
        }

        public int Score
        {
            get
            {
                return this.score;
            }
            set
            {
                this.score = value;
                if (this.ScoreChanged != null)
                {
                    this.ScoreChanged(this, new GameEventArgs(this));
                }
            }
        }

        public Round GetNextRound()
        {
            if (this.currentRound != null)
            {
                this.UpdateScore();
            }

            if (this.currentRound == null)
            {
                this.currentRound = new Round1(this.simulator, this);
            }
            else
            {
                this.currentRound = this.currentRound.GetNextRound();
            }

            this.rounds.Add(this.currentRound);
            this.currentRound.ScoreChanged += new EventHandler<RoundEventArgs>(currentRound_ScoreChanged);
            this.currentRound.RoundOver += new EventHandler<RoundEventArgs>(currentRound_RoundOver);
            this.currentRound.PowerupAcquired += new EventHandler<PowerupAcquiredEventArgs>(currentRound_PowerupAcquired);
            this.player.ApplyAllPowerups(this.currentRound.UserTank);
            this.computer.ApplyAllPowerups(this.currentRound.Enemies);
            this.currentRound.UserTank.ResetHitPoints();

            if (this.RoundChanged != null)
            {
                this.RoundChanged(this, new GameEventArgs(this));
            }

            return this.currentRound;
        }

        void currentRound_PowerupAcquired(object sender, PowerupAcquiredEventArgs e)
        {

            if (e.Vehicle == this.currentRound.UserTank)
            {
                if (e.Powerup is BurstPowerup == false)
                {
                    this.player.Powerups.Add(e.Powerup);
                }

                Player.ApplyPowerup(this.player, this.currentRound.UserTank, e.Powerup);
            }
            else
            {
                if (e.Powerup is BurstPowerup == false)
                {
                    this.computer.Powerups.Add(e.Powerup);
                }
                Player.ApplyPowerup(this.computer, this.currentRound.Enemies, e.Powerup);
            }

        }

        void UpdateScore()
        {
            this.previousRoundsScore = this.previousRoundsScore + this.currentRound.Score;
            this.Score = this.previousRoundsScore;
        }

        void UpdateGameStatus()
        {
            if (!this.currentRound.Pass)
            {
                this.over = true;
                if (this.GameOver != null)
                {
                    this.GameOver(this, new GameEventArgs(this));
                }
            }
        }

        void currentRound_RoundOver(object sender, RoundEventArgs e)
        {
            this.UpdateGameStatus();
        }

        void currentRound_ScoreChanged(object sender, RoundEventArgs e)
        {
            this.Score = this.previousRoundsScore + e.Round.Score;
        }

        public event EventHandler<GameEventArgs> ScoreChanged;
        public event EventHandler<GameEventArgs> RoundChanged;
        public event EventHandler<GameEventArgs> GameOver;
    }
}
