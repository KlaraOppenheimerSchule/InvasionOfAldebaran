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
        public String _Name;
        public int _Points;
        public FrameWindowViewModel()
        {
            String[] arguments = Environment.GetCommandLineArgs();
            if(arguments.Length> 2)
            {   
                
                _Name = arguments[1];
                _Points = Convert.ToInt32(arguments[2]);
            }
            
            this.Items.Add(new MainMenuViewModel(this));
            this.Items.Add(new PlayViewModel(this));
            this.ActivateItem( this.Items.FirstOrDefault());

        }

        public void WindowClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Environment.ExitCode = _Points;
        }

        public void PointsAchieved( int _points )
        {
            _Points = _points;
        }
    }
}
