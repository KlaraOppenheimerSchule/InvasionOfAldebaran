using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Caliburn.Micro;

namespace InvasionOfAldebaran.ViewModels
{
    class FrameWindowViewModel : Conductor<Screen>.Collection.OneActive
    {

        public String pName;
        public String pPoints;
        public FrameWindowViewModel()
        {
            String[] arguments = Environment.GetCommandLineArgs();
            if(arguments.Length> 2)
            {   
                
                pName = arguments[1];
                pPoints = arguments[2];
            }

            this.Items.Add(new MainMenuViewModel(this));
            this.Items.Add(new PlayViewModel());
            this.ActivateItem( this.Items.FirstOrDefault());
            
        }

        public void WindowClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            //Environment.ExitCode = pointsAchived();
        }
        
    }
}
