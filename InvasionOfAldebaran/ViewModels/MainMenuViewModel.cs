using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InvasionOfAldebaran.Shared;
using System.Threading.Tasks;
using System.Windows.Input;
using Caliburn.Micro;
using InvasionOfAldebaran.Models;

namespace InvasionOfAldebaran.ViewModels
{
	public class MainMenuViewModel : Screen
    {
        public ICommand PlayButtonCommand { get; set; }
        public ICommand CloseButtonCommand { get; set; }
        public String Highscore { get; set; }
		
        private FrameWindowViewModel _frameModel;

        public MainMenuViewModel( FrameWindowViewModel frameModel) {
            _frameModel = frameModel;

            this.PlayButtonCommand = new RelayCommand(this.Change_Window);
            this.CloseButtonCommand = new RelayCommand(this.Close_Window);
            Highscore = "HIGHSCORE: " + Convert.ToString(frameModel.Points);
        }


        public void Close_Window()
        {
            _frameModel.CloseItem(_frameModel);
        }
        public void Change_Window()
		{
			_frameModel.ActivateItem(_frameModel.Items.Single(s => s.GetType() == typeof(PlayViewModel)));

		}
		
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }
    }
}
