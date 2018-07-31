using Caliburn.Micro;
using System;
using System.Linq;
using System.Windows;
using InvasionOfAldebaran.Helper;
using System.Windows.Input;

namespace InvasionOfAldebaran.ViewModels
{
    public class FrameWindowViewModel : Conductor<Screen>.Collection.OneActive
    {
        public string Name { get; set; }
        public int Points { get; set; }
		public double Height { get; private set; }
		public double Width { get; private set; }

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
			this.Width = SystemParameters.WorkArea.Width;

            this.Items.Add(new MainMenuViewModel(this));
            this.Items.Add(new PlayViewModel(this));
			this.Items.Add(new IntroViewModel(this));
            this.ChangeScreen(typeof(MainMenuViewModel));
        }

        public void DisplayAddScoreScreen(int score)
        {
			// Todo: Neue Instanz von AddScore erstellen und anzeigen lassen, anschließend Highscore im MainMenu aktualisieren
			var AddScore = new AddScoreViewModel(this, score);

			this.ActivateItem(AddScore);
	    }
		public void SetNewHighScore(Score score)
		{
			var menu = this.Items.Single(s => s is MainMenuViewModel) as MainMenuViewModel;
			menu.AddScore(score);
		}

	    public void ChangeScreen(Type screen, Type from = null)
	    {
			if (screen == typeof(IntroViewModel))
			{
				Soundmanager.PlayMainMenuTheme(true);
				this.ActivateItem(this.Items.Single(s => s is IntroViewModel));
			}
			if (screen == typeof(MainMenuViewModel) && from != null && from == typeof(IntroViewModel))
		    {
				this.ActivateItem(this.Items.Single(s => s is MainMenuViewModel));
			}
			else if (screen == typeof(MainMenuViewModel))
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