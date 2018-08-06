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
		/// <summary>
		/// Creates and displays a new instance of the AddScoreScreen which will be garbage collected after setting the highscore
		/// </summary>
		/// <param name="score">The score the player achieved</param>
        public void DisplayAddScoreScreen(int score)
        {
			// Neue Instanz von AddScore erstellen und anzeigen lassen
			var AddScore = new AddScoreViewModel(this, score);

			this.ActivateItem(AddScore);
	    }
		/// <summary>
		/// Adds the highscore into the existing highscore list in the MainMenuViewModel
		/// </summary>
		/// <param name="score"></param>
		public void SetNewHighScore(Score score)
		{
			var menu = this.Items.Single(s => s is MainMenuViewModel) as MainMenuViewModel;

			menu.AddScore(score);
		}
		/// <summary>
		/// Displays one of the conductors ViewModels and sets the appropriate theme depending on the given type in the parameter
		/// </summary>
		/// <param name="screen">The type of screen you want to have displayed</param>
		/// <param name="from">Optional type to handle special transitions</param>
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