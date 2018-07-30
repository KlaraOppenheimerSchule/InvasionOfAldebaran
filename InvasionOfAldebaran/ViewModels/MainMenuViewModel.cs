using Caliburn.Micro;
using InvasionOfAldebaran.Helper;
using System;
using System.Collections.Generic;
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
	    public List<Score> Highscore { get; set; }

        private FrameWindowViewModel _frameModel;

        public MainMenuViewModel(FrameWindowViewModel frameModel)
        {
          string[] arguments = Environment.GetCommandLineArgs();

            //if (arguments.Length > 2)
            //{
            //    this.Name = arguments[1];

            //    int.TryParse(arguments[2], out var points);
            //    this.Points = points;
            //}

            _frameModel = frameModel;

            this.PlayButtonCommand = new RelayCommand(this.ChangeWindow);
            this.CloseButtonCommand = new RelayCommand(this.CloseWindow);
			this.Highscore = new List<Score>();
			this.Highscore.Add(new Score(50, "Ein Spieler"));
        }

        public string ReturnHighscore()
        {
            return _highScore;
        }

        public void SetScore(int score, string name)
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
            _frameModel.CloseItem(_frameModel);
        }


        private void ChangeWindow()
        {
			_frameModel.ChangeScreen(typeof(PlayViewModel));
        }

        #endregion Interface Members
    }
}