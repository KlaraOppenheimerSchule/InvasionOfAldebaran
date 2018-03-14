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
		private FrameWindowViewModel _frameWindowViewModel;
        private readonly DispatcherTimer _timer = new DispatcherTimer();
		private readonly Coords _playerSpawn = new Coords(250, 600);
		private DateTime _lastMissile;
	    private Direction _lastDirection;


	    public List<AnimatedObject> Objects { get; set; }
        public Player Player { get; set; }
        public Canvas Canvas { get; set; }
		public SpawnHandler Spawner { get; set; }
		
		public ICommand HandleInputCommand { get; private set; }
		
        public PlayViewModel( FrameWindowViewModel frameWindow)
		{
			this._frameWindowViewModel = frameWindow;
            this.Objects = new List<AnimatedObject>();
			
            this.Canvas = new Canvas()
            {
                Height = 700,
                Width = 500,
                Focusable = true,
                Background = Brushes.DarkGray,
            };
			this.Activated += StartGame;
			
			this.HandleInputCommand = new RelayCommand(this.HandleInput);
		}

		private void StartGame(object sender, EventArgs e)
		{
			this._timer.Interval = TimeSpan.FromSeconds(0.01);
			this.Player = new Player(_playerSpawn, 0, 0);
			this.Objects.Add(Player);
			this.Spawner = new SpawnHandler(this.Canvas.Width);
			this.Objects.AddRange(this.Spawner.SpawnEnemies());

			this._timer.Tick += AnimateObjects;
			this._timer.Start();
		}

		private void EndGame(string text)
		{
			//TextField.Text = text;
			//TextField.Visibility = Visibility.Visible;
		}

		private void AnimateObjects(object sender, EventArgs e)
        {
			if (!this.Canvas.IsFocused)
				this.Canvas.Focus();

			List<AnimatedObject> objectsToBeDeleted = new List<AnimatedObject>();

            foreach (var item in Objects)
            {
                item.Animate(_timer.Interval, Canvas);
                if (item.ReachedEnd)
                {
                    objectsToBeDeleted.Add(item);
                }
            }
            foreach (var enemy in Objects.OfType<Enemy>())
            {
                foreach (var missile in Objects.OfType<Missile>())
                {
                    if (enemy.ContainsPoint(missile.Coords.X, missile.Coords.Y))
                    {
                        objectsToBeDeleted.Add(enemy);
                        objectsToBeDeleted.Add(missile);
                    }
                }
            }
            foreach (var obj in objectsToBeDeleted)
            {
                this.Objects.Remove(obj);
            }
            this.Canvas.Children.Clear();
            foreach (var item in Objects)
            {
                item.Draw(this.Canvas);
            }
        }

	    private void HandleInput(string action)
	    {
		    switch (action)
		    {
				case null:
					break;
				case "Left":
					this.Player.Move(Direction.Left);
					break;
				case "Right":
					this.Player.Move(Direction.Right);
					break;
				case "Fire":
					if (_lastMissile.AddSeconds(0.5) <= DateTime.Now)
					{
						var missile = this.Player.Fire();
						this.Objects.Add(missile);
						_lastMissile = DateTime.Now;
					}
					break;
			}
	    }
    }
}
