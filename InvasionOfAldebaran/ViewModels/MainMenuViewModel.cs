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
        public ICommand PlayButtonCommand { get; set; }
        public ICommand CloseButtonCommand { get; set; }
        private FrameWindowViewModel _frameModel;

        public MainMenuViewModel( FrameWindowViewModel frame_model) {
            _frameModel = frame_model;
            this.PlayButtonCommand = new RelayCommand(this.Change_Window);
            this.CloseButtonCommand = new RelayCommand(this.Close_Window);
        }


        public void Close_Window()
        {
            _frameModel.CloseItem(_frameModel);
        }
        public void Change_Window()
        {
            _frameModel.ActivateItem(_frameModel.Items[1]);
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }
    }
}
