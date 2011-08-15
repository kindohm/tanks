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
using System.Diagnostics;

namespace Tanks.Web.UI
{
    public partial class GameOver : UserControl
    {
        Game game;
        Score score;
        public GameOver(Game game)
        {
            this.game = game;
            InitializeComponent();
        }

        private void newGame_Click(object sender, RoutedEventArgs e)
        {
            if (this.NewGameClicked != null)
            {
                this.NewGameClicked(this, EventArgs.Empty);
            }
        }

        public event EventHandler NewGameClicked;

        private void submitScore_Click(object sender, RoutedEventArgs e)
        {
            this.submitScore.Visibility = Visibility.Collapsed;
            this.scoreForm.Visibility = Visibility.Visible;
        }

        void score_ProgressChanged(object sender, ProgressEventArgs e)
        {
            this.scoreProgress.Value = (double)e.Percent;
        }

        void score_SubmissionCompleted(object sender, EventArgs e)
        {
            this.submittedBlock.Visibility = Visibility.Visible;
            if (score.Success == false)
            {
                this.submittedBlock.FontSize = 9;
                this.submittedBlock.Foreground = new SolidColorBrush(Colors.Yellow);
                this.submittedBlock.Text = "Error submitting your score.  Sorry!  " + score.ResultMessage;
            }
        }

        private void submitButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(this.nameTextBox.Text))
            {
                this.noNameTextBlock.Visibility = Visibility.Visible;
            }
            else
            {
                this.noNameTextBlock.Visibility = Visibility.Collapsed;
                this.scoreProgress.Visibility = Visibility.Visible;
                this.scoreForm.Visibility = Visibility.Collapsed;
                score = new Score();
                score.SubmissionCompleted += new EventHandler(score_SubmissionCompleted);
                score.ProgressChanged += new EventHandler<ProgressEventArgs>(score_ProgressChanged);
                score.GameDate = DateTime.Now;
                score.UserName = this.nameTextBox.Text;
                score.UserUrl = this.urlTextBox.Text;
                score.Game = game;
                score.Location = this.locationTextBox.Text;
                score.Submit();
            }
        }

       
      
    }
}
