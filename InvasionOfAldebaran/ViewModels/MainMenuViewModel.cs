﻿using Caliburn.Micro;
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
        private string _highScore;
        public ICommand PlayButtonCommand { get; set; }
        public ICommand CloseButtonCommand { get; set; }
        public int Points { get; set; }
	    public string Highscore
	    {
		    get { return "HighScore: " + this.Points; }
			set { this._highScore = value; }
	    }

	    //private MediaPlayer _mainThemePlayer;

        private FrameWindowViewModel _frameModel;

        public MainMenuViewModel(FrameWindowViewModel frameModel)
        {
            _frameModel = frameModel;
            Soundmanager.GameTheme(false);

            this.PlayButtonCommand = new RelayCommand(this.ChangeWindow);
            this.CloseButtonCommand = new RelayCommand(this.CloseWindow);
            this.Highscore = "HIGHSCORE: " + Convert.ToString(frameModel.Points);
        }

        public string returnHighscore()
        {
            return _highScore;
        }

        public void setHighscore(int score)
        {
            this.Points = score;
        }
		#region Interface Members

        private void CloseWindow()
        {
            sendPointsToTRT();
            
            _frameModel.CloseItem(_frameModel);
        }

        public void sendPointsToTRT( )
        {
            Environment.ExitCode = this.Points;
        }

        private void ChangeWindow()
        {
            Soundmanager.GameTheme(true);
            _frameModel.ActivateItem(_frameModel.Items.Single(s => s is PlayViewModel));
        }

        #endregion Interface Members
    }
}