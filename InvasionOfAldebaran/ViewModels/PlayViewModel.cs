using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Threading;
using Caliburn.Micro;
using InvasionOfAldebaran.Models;

namespace InvasionOfAldebaran.ViewModels
{
    public class PlayViewModel : Screen
    {

	    public DispatcherTimer timer = new DispatcherTimer();

	    public List<AnimatedObject> Objects = new List<AnimatedObject>();

		public Canvas Canvas { get; set; }

	    public PlayViewModel()
	    {
			timer.Interval = TimeSpan.FromSeconds(0.01);
		    timer.Tick += AnimateObjects;
		}

	    void AnimateObjects(object sender, EventArgs e)
	    {
		    foreach (var item in Objects)
		    {
			    item.Animiere(timer.Interval, spielfeld);
		    }

		    bool gameOver = false;
		    List<Asteroid> zuLöschendeAsteroiden = new List<Asteroid>();
		    List<Photonentorpedo> zuLöschendePhotonentorpedos = new List<Photonentorpedo>();
		    foreach (var a in spielobjekte.OfType<Asteroid>())
		    {
			    foreach (var p in spielobjekte.OfType<Photonentorpedo>())
			    {
				    if (a.EnthältPunkt(p.x, p.y))
				    {
					    zuLöschendePhotonentorpedos.Add(p);
					    zuLöschendeAsteroiden.Add(a);
				    }
			    }

			    if (a.EnthältPunkt(raumschiff.x, raumschiff.y))
			    {
				    gameOver = true;
				    zuLöschendeAsteroiden.Add(a);
			    }
		    }

		    spielobjekte = spielobjekte.Except(zuLöschendeAsteroiden).ToList();
		    spielobjekte = spielobjekte.Except(zuLöschendePhotonentorpedos).ToList();
		    if (gameOver == true)
		    {
			    spielEnde("Game Over");
		    }
		    else if (spielobjekte.OfType<Asteroid>().Count() == 0)
		    {
			    spielEnde("Victory");
		    }

		    spielfeld.Children.Clear();
		    foreach (var item in spielobjekte)
		    {
			    item.Zeichne(spielfeld);
		    }
	    }
	}
}
