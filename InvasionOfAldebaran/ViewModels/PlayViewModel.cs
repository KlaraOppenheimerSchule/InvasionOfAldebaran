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
using InvasionOfAldebaran.Shared;

namespace InvasionOfAldebaran.ViewModels
{
    public class PlayViewModel : Screen
    {
	    private readonly DispatcherTimer _timer = new DispatcherTimer();
        
	    public List<AnimatedObject> Objects { get; set; }

	    public Player Player;

		public Canvas Canvas { get; set; }

	    public TextBlock TextField { get; set; }

		public PlayViewModel()
	    {
            
			Objects = new List<AnimatedObject>();
            Canvas = new Canvas()
            {
                Height = 900,
                Width = 600,
                Focusable = true,
                Background = Brushes.DarkGray
            };
			_timer.Interval = TimeSpan.FromSeconds(0.01);

		    _timer.Tick += AnimateObjects;
            this.Canvas.KeyDown += this.WindowKeyDown;

			this.Player = new Player(Canvas, 300, 800, 0, 0);
			this.Objects.Add(Player);
			_timer.Start();
		}

	    void AnimateObjects(object sender, EventArgs e)
	    {
            this.Canvas.Focus();
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
		    if (Objects.Contains(this.Player))
		    {
			    switch (e.Key)
			    {
				    default:
					    break;
				    case Key.A:
				    case Key.Left:
					    this.Player.Move(Direction.Left);
					    break;
				    case Key.D:
				    case Key.Right:
					    this.Player.Move(Direction.Right);
					    break;
				    //case Key.Space:
					   // if (lastTorpedo.AddSeconds(0.5) <= DateTime.Now)
					   // {
						  //  spielobjekte.Add(new Photonentorpedo(raumschiff));
						  //  lastTorpedo = DateTime.Now;
					   // }
					   // break;
			    }
		    }
	    }
	}
}
