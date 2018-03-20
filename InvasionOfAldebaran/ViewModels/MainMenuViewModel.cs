using System;
using System.Linq;
using System.Windows.Input;
using Caliburn.Micro;
using InvasionOfAldebaran.Helper;

namespace InvasionOfAldebaran.ViewModels
{
	public class MainMenuViewModel : Screen, IScreenViewModel
    {
        public ICommand PlayButtonCommand { get; set; }
        public ICommand CloseButtonCommand { get; set; }
        public string Highscore { get; set; }
		
        private readonly FrameWindowViewModel _frameModel;

        public MainMenuViewModel( FrameWindowViewModel frameModel) {
            _frameModel = frameModel;

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
			_frameModel.ActivateItem(_frameModel.Items.Single(s => s is PlayViewModel));
		}
    }
}
