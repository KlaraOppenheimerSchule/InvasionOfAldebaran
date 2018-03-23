using Caliburn.Micro;
using InvasionOfAldebaran.Helper;
using System;
using System.Linq;
using System.Windows.Input;
using System.Windows.Media;

namespace InvasionOfAldebaran.ViewModels
{
    public class MainMenuViewModel : Screen, IScreenViewModel
    {
        public ICommand PlayButtonCommand { get; set; }
        public ICommand CloseButtonCommand { get; set; }
        public string Highscore { get; set; }

        private MediaPlayer _mainThemePlayer;

        private readonly FrameWindowViewModel _frameModel;

        public MainMenuViewModel(FrameWindowViewModel frameModel)
        {
            _mainThemePlayer = new MediaPlayer();
            _frameModel = frameModel;
            _mainThemePlayer.Open(new Uri(@"../../Resources/Media/MainMenuTheme.mp3", UriKind.Relative));
            _mainThemePlayer.Volume = 0.5;
            _mainThemePlayer.Play();

            this.PlayButtonCommand = new RelayCommand(this.ChangeWindow);
            this.CloseButtonCommand = new RelayCommand(this.CloseWindow);
            this.Highscore = "HIGHSCORE: " + Convert.ToString(frameModel.Points);
        }

        public void CloseWindow()
        {
            _frameModel.CloseItem(_frameModel);
        }

        public void ChangeWindow()
        {
            _mainThemePlayer.Stop();
            _frameModel.ActivateItem(_frameModel.Items.Single(s => s is PlayViewModel));
        }
    }
}