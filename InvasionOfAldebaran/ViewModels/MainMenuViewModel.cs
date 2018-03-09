using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InvasionOfAldebaran.Shared;
using System.Threading.Tasks;
using System.Windows.Input;
using Caliburn.Micro;

namespace InvasionOfAldebaran.ViewModels
{
    class MainMenuViewModel : Screen
    {
        public ICommand _PlayButtonCommand { get; set; }
        public ICommand _CloseButtonCommand { get; set; }
        public String _Highscore { get; set; }
        private FrameWindowViewModel _FrameModel;

        public MainMenuViewModel( FrameWindowViewModel _frameModel) {
            _FrameModel = _frameModel;

            this._PlayButtonCommand = new RelayCommand(this.Change_Window);
            this._CloseButtonCommand = new RelayCommand(this.Close_Window);
            _Highscore = "HIGHSCORE: " + Convert.ToString(_FrameModel._Points);
        }


        public void Close_Window()
        {
            _FrameModel.CloseItem(_FrameModel);
        }
        public void Change_Window()
        {
            _FrameModel.ActivateItem(_FrameModel.Items[1]);

        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }
    }
}
