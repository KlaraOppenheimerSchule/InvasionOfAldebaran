using Caliburn.Micro;
using InvasionOfAldebaran.Helper;
using System;
using System.Linq;
using System.Security.RightsManagement;
using System.Windows.Input;
using System.Windows.Media;

namespace InvasionOfAldebaran.ViewModels
{
    public class MainMenuViewModel : Screen
    {
        public string Name { get; set; }
        private string _highScore;
        public ICommand PlayButtonCommand { get; set; }
        public ICommand CloseButtonCommand { get; set; }
        public int Points { get; set; }
        public int NewPoints { get; set; }
	    public string Highscore
	    {
		    get { return "HighScore: " + this.Points; }
			set { _highScore = value; }
	    }

        private FrameWindowViewModel _frameModel;

        public MainMenuViewModel(FrameWindowViewModel frameModel)
        {
          string[] arguments = Environment.GetCommandLineArgs();

            if (arguments.Length > 2)
            {
                this.Name = arguments[1];

                int.TryParse(arguments[2], out var points);
                this.Points = points;
            }

            _frameModel = frameModel;

            this.PlayButtonCommand = new RelayCommand(this.ChangeWindow);
            this.CloseButtonCommand = new RelayCommand(this.CloseWindow);
            this.Highscore = "HIGHSCORE: " + Convert.ToString(frameModel.Points);
        }

        public string ReturnHighscore()
        {
            return _highScore;
        }

        public void SetScore(int score)
        {
            this.NewPoints = score;
            if( this.Points < score )
                {
                this.Points = score;
                }

        }
		#region Interface Members

        private void CloseWindow()
        {
            this.SendPointsToTrt();

            _frameModel.CloseItem(_frameModel);
        }

        public void SendPointsToTrt()
        {
            if(this.NewPoints > 0 && this.NewPoints < 100 )
                {
                Environment.ExitCode = this.NewPoints;
                }
        }

        private void ChangeWindow()
        {
			_frameModel.ChangeScreen(typeof(PlayViewModel));
        }

        #endregion Interface Members
    }
}