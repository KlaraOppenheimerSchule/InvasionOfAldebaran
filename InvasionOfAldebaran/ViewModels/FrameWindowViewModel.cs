using Caliburn.Micro;
using System;
using System.Linq;

namespace InvasionOfAldebaran.ViewModels
{
    public class FrameWindowViewModel : Conductor<Screen>.Collection.OneActive
    {

        public FrameWindowViewModel()
        {
            this.Items.Add(new MainMenuViewModel(this));
            this.Items.Add(new PlayViewModel(this));
            this.ActiveItem = this.Items.FirstOrDefault();
        }

	    public void SetScore(int score)
	    {

            var mainMenu = this.Items.SingleOrDefault(m => m is MainMenuViewModel) as MainMenuViewModel;

			if(mainMenu != null)
				mainMenu.setScore( score );

	    }
    }
}