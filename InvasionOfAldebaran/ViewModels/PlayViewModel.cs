using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Threading;
using Caliburn.Micro;
using InvasionOfAldebaran.Models;

namespace InvasionOfAldebaran.ViewModels
{
    public class PlayViewModel : Screen
    {
	    private readonly DispatcherTimer _timer = new DispatcherTimer();

	    public List<AnimatedObject> Objects { get; set; }

		public Canvas Canvas { get; set; }

	    public TextBlock TextField { get; set; }

		public PlayViewModel()
	    {
			Objects = new List<AnimatedObject>();
			Canvas = new Canvas();
			_timer.Interval = TimeSpan.FromSeconds(0.01);
		    _timer.Tick += AnimateObjects;

			this.Objects.Add(new Player(this.Canvas, Canvas.Width * 0.5, Canvas.Height * 0.1, 3, 3 ));
			_timer.Start();
		}

	    void AnimateObjects(object sender, EventArgs e)
	    {
		    foreach (var item in Objects)
		    {
			    item.Animate(_timer.Interval, Canvas);
		    }

		    foreach (var player in Objects.OfType<Player>())
		    {
			    foreach (var enemy in Objects.OfType<Enemy>())
			    {
				    if (player.ContainsPoint(enemy.X, enemy.Y))
				    {
					    // Player dies
				    }
			    }
		    }
		   
			this.Canvas.Children.Clear();
		    foreach (var item in Objects)
		    {
			   item.Draw(this.Canvas);
		    }
	    }

	    private void EndGame(string text)
	    {
		    TextField.Text = text;
		    TextField.Visibility = Visibility.Visible;
	    }

	    private void WindowKeyDown(object sender, KeyEventArgs e)
	    {
		    if (Objects.Contains(//Todo: Spielervariable einfügen))
		    {
			    switch (e.Key)
			    {
				    default:
					    break;
				    case Key.A:
				    case Key.Left:
					    raumschiff.BiegeAb(true);
					    break;
				    case Key.D:
				    case Key.Right:
					    raumschiff.BiegeAb(false);
					    break;
				    case Key.W:
				    case Key.Up:
					    raumschiff.Beschleunige(true);
					    break;
				    case Key.S:
				    case Key.Down:
					    raumschiff.Beschleunige(false);
					    break;
				    case Key.Space:
					    if (lastTorpedo.AddSeconds(0.5) <= DateTime.Now)
					    {
						    spielobjekte.Add(new Photonentorpedo(raumschiff));
						    lastTorpedo = DateTime.Now;
					    }
					    break;
			    }
		    }
	    }
	}
}
