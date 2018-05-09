using Caliburn.Micro;
using System;
using System.Linq;

namespace InvasionOfAldebaran.ViewModels
{
    public class FrameWindowViewModel : Conductor<Screen>.Collection.OneActive
    {
        public string Name { get; set; }
        public int Points { get; set; }

        public FrameWindowViewModel()
        {
            string[] arguments = Environment.GetCommandLineArgs();

            if (arguments.Length > 2)
            {
                this.Name = arguments[1];

                int.TryParse(arguments[2], out var points);
                this.Points = points;
            }

            this.Items.Add(new MainMenuViewModel(this));
            this.Items.Add(new PlayViewModel(this));
            this.ActiveItem = this.Items.FirstOrDefault();
        }

        public void SetScore(int score)
        {
            var mainMenu = this.Items.SingleOrDefault(m => m is MainMenuViewModel) as MainMenuViewModel;

            if (mainMenu != null)
                mainMenu.SetHighscore(score);
        }
    }
}