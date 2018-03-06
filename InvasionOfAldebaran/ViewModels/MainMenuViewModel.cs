using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;

namespace InvasionOfAldebaran.ViewModels
{
    class MainMenuViewModel : Screen
    {
        private FrameWindowViewModel Frame_model;

        public void Change_Window()
        {
            Frame_model = new FrameWindowViewModel();
            Frame_model.Items.Add(new PlayViewModel());
            Frame_model.ActivateItem(Frame_model.Items[1]);

        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }
    }
}
