using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;

namespace InvasionOfAldebaran.Models
{
	public class SpawnHandler
	{
		private readonly double _canvasWidth;
		
		public List<Coords> SpawnPoints { get; private set; }

		public SpawnHandler(double canvasWidth)
		{
			this._canvasWidth = canvasWidth;
			this.SpawnPoints = new List<Coords>();
			this.PopulateSpawnPoints();
		}
		
		private void PopulateSpawnPoints()
		{
			double gap = (this._canvasWidth - 100) / 4;
			double canvasPos = 50;

			for (int i = 0; i < 4; i++)
			{
				Coords point = new Coords(canvasPos, 100);
				this.SpawnPoints.Add(point);
				canvasPos += gap;
			}
		}

		public List<AnimatedObject> SpawnEnemies()
		{
			List<Brush> colors = new List<Brush>();
			List<Coords> spawns = this.SpawnPoints;
			colors.Add(Brushes.Red);
			colors.Add(Brushes.Orange);
			colors.Add(Brushes.White);
			colors.Add(Brushes.Violet);

			List<AnimatedObject> newEnemies = new List<AnimatedObject>();

			for (int i = 0; i < 4; i++)
			{
				Random r = new Random();
				int rColor = r.Next(0, 3 - i);
				int rSpawns = r.Next(0, 3 - i);
				
				newEnemies.Add(new Enemy(colors[rColor], spawns[rSpawns], 0, 200));

				colors.RemoveAt(rColor);
				spawns.RemoveAt(rSpawns);
			}
			return newEnemies;
		}
	}
}
