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
        public ICommand pPlayButtonCommand { get; set; }
        public ICommand pCloseButtonCommand { get; set; }
        public String pHighscore { get; set; }

        private FrameWindowViewModel pFrameModel;

        public MainMenuViewModel( FrameWindowViewModel _frameModel) {
            pFrameModel = _frameModel;
            
            this.pPlayButtonCommand = new RelayCommand(this.Change_Window);
            this.pCloseButtonCommand = new RelayCommand(this.Close_Window);
            pHighscore = "HIGHSCORE: " + Convert.ToString(pFrameModel.pPoints);
        }


        public void Close_Window()
        {
            pFrameModel.CloseItem(pFrameModel);
        }
        public void Change_Window()
        {
            pFrameModel.ActivateItem(pFrameModel.Items[1]);
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }
    }
}
