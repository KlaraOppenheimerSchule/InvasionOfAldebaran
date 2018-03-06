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
        public FrameWindowViewModel()
        {

            this.Items.Add(new MainMenuViewModel());
            this.Items.Add(new PlayViewModel());
            this.ActivateItem( this.Items.FirstOrDefault());
            
        }
        public override void TryClose(bool? dialogResult = null)
        {
            base.TryClose(dialogResult);
        }
    }
}
