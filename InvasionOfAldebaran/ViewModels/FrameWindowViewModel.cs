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
    public class FrameWindowViewModel : Conductor<Screen>.Collection.OneActive
    {
        public String Name;
        public int Points;
        public FrameWindowViewModel()
        {
            String[] arguments = Environment.GetCommandLineArgs();
            if(arguments.Length> 2)
            {   
                
                Name = arguments[1];
                Points = Convert.ToInt32(arguments[2]);
            }
            
            this.Items.Add(new MainMenuViewModel(this));
            this.Items.Add(new PlayViewModel(this));
            this.ActiveItem = this.Items.FirstOrDefault();

        }
		
        public void WindowClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Environment.ExitCode = Points;
        }

        public void PointsAchieved( int points )
        {
            Points = points;
        }
    }
}
