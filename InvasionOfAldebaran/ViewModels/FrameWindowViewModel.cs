using Caliburn.Micro;
using System;
using System.Linq;
using System.Windows.Media;

namespace InvasionOfAldebaran.ViewModels
{
    public class FrameWindowViewModel : Conductor<Screen>.Collection.OneActive
    {
        public string Name { get; set; }
        public int Points { get; set; }

        public FrameWindowViewModel()
        {
            string[] arguments = Environment.GetCommandLineArgs();
            //private MediaPlayer _mainThemePlayer;
            if (arguments.Length > 2)
            {
                this.Name = arguments[1];

                int.TryParse(arguments[2], out var points);
                this.Points = points;
            }
            //_mainThemePlayer = new MediaPlayer();
            //_mainThemePlayer.Open(new Uri(@"../../Resources/themesong.mpeg", UriKind.Relative));
            //_mainThemePlayer.Play();
            this.Items.Add(new MainMenuViewModel(this));
            this.Items.Add(new PlayViewModel(this));
            this.ActiveItem = this.Items.FirstOrDefault();
        }

        public void WindowClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Environment.ExitCode = this.Points;
        }

        public void PointsAchieved(int points)
        {
            this.Points = points;
        }
    }
}