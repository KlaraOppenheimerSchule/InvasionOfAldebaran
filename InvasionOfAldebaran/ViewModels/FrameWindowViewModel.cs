using Caliburn.Micro;
using System;
using System.Linq;
using System.Windows;
using InvasionOfAldebaran.Helper;

namespace InvasionOfAldebaran.ViewModels
{
    public class FrameWindowViewModel : Conductor<Screen>.Collection.OneActive
    {
        public string Name { get; set; }
        public int Points { get; set; }
		public double Height { get; set; }
		public double Width { get; set; }

        public FrameWindowViewModel()
        {
            string[] arguments = Environment.GetCommandLineArgs();
	        this.Name = "Invasion of Aldebaran";

            if (arguments.Length > 2)
            {
                this.Name = arguments[1];

                int.TryParse(arguments[2], out var points);
                this.Points = points;
            }
	        this.Height = SystemParameters.WorkArea.Height;

            this.Items.Add(new MainMenuViewModel(this));
            this.Items.Add(new PlayViewModel(this));
            this.ChangeScreen(typeof(MainMenuViewModel));
        }

        public void SetScore(int score)
        {
            var mainMenu = this.Items.SingleOrDefault(m => m is MainMenuViewModel) as MainMenuViewModel;

			if(mainMenu != null)
				mainMenu.SetScore( score );
	    }

	    public void ChangeScreen(Type screen)
	    {
		    
		    if (screen == typeof(MainMenuViewModel))
		    {
				Soundmanager.PlayInGameTheme(false);
				this.ActivateItem(this.Items.Single(s => s is MainMenuViewModel));
				Soundmanager.PlayMainMenuTheme(true);
			}
		    if (screen == typeof(PlayViewModel))
		    {
			    Soundmanager.PlayMainMenuTheme(false);
				this.ActivateItem(this.Items.Single(s => s is PlayViewModel));
				Soundmanager.PlayInGameTheme(true);
		    }
	    }
    }
}