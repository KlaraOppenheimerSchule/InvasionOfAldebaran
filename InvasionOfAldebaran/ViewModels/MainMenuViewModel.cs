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

        private readonly FrameWindowViewModel _frameModel;

        public MainMenuViewModel(FrameWindowViewModel frameModel)
        {
            _frameModel = frameModel;
            Soundmanager.PlayTheme(false);

            this.PlayButtonCommand = new RelayCommand(this.ChangeWindow);
            this.CloseButtonCommand = new RelayCommand(this.CloseWindow);
            this.Highscore = "HIGHSCORE: " + Convert.ToString(frameModel.Points);
        }

        #region Interface Members

        private void CloseWindow()
        {
            _frameModel.CloseItem(_frameModel);
        }

        private void ChangeWindow()
        {
            Soundmanager.PlayTheme(true);
            _frameModel.ActivateItem(_frameModel.Items.Single(s => s is PlayViewModel));
        }

        #endregion Interface Members
    }
}