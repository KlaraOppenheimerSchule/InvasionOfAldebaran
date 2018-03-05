using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;

namespace InvasionOfAldebaran.ViewModels
{
    public class FrameWindowViewModel : Conductor<Screen>.Collection.OneActive
    {
	    public FrameWindowViewModel()
	    {
		    this.Items.Add(new PlayViewModel());
		    this.ActiveItem = Items.FirstOrDefault();
	    }
    }
}
