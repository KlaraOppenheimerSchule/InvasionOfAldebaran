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

        private DateTime lastMissile;


        public List<AnimatedObject> Objects { get; set; }

        public List<Coords> SpawnPoints { get; set; }

        public Player Player { get; set; }

        public Canvas Canvas { get; set; }

        public PlayViewModel()
        {
            Objects = new List<AnimatedObject>();
            SpawnPoints = new List<Coords>();
            Canvas = new Canvas()
            {
                Height = 800,
                Width = 500,
                Focusable = true,
                Background = Brushes.DarkGray
            };
            _timer.Interval = TimeSpan.FromSeconds(0.01);

            _timer.Tick += AnimateObjects;
            this.Canvas.PreviewKeyDown += this.WindowKeyDown;

            this.Player = new Player(new Coords(250, 700), 0, 0);
            this.Objects.Add(Player);

            PopulateSpawnPoints();
            SpawnEnemies();

            _timer.Start();
        }

        void AnimateObjects(object sender, EventArgs e)
        {
            if (!this.Canvas.IsFocused)
                this.Canvas.Focus();

            List<AnimatedObject> ObjectsToBeDeleted = new List<AnimatedObject>();

            foreach (var item in Objects)
            {
                item.Animate(_timer.Interval, Canvas);
                if (item.ReachedEnd)
                {
                    ObjectsToBeDeleted.Add(item);
                }
            }
            

            foreach (var enemy in Objects.OfType<Enemy>())
            {
                foreach (var missile in Objects.OfType<Missile>())
                {
                    if (enemy.ContainsPoint(missile.Coords.X, missile.Coords.Y))
                    {
                        ObjectsToBeDeleted.Add(enemy);
                        ObjectsToBeDeleted.Add(missile);
                    }
                    
                }
            }

            foreach (var obj in ObjectsToBeDeleted)
            {
                this.Objects.Remove(obj);
            }

            this.Canvas.Children.Clear();
            foreach (var item in Objects)
            {
                item.Draw(this.Canvas);
            }
        }

        private void EndGame(string text)
        {
            //TextField.Text = text;
            //TextField.Visibility = Visibility.Visible;
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
                    case Key.Space:
                        if (lastMissile.AddSeconds(0.5) <= DateTime.Now)
                        {
                            Coords missileSpawn = new Coords(this.Player.Coords.X, this.Player.Coords.Y);
                            Objects.Add(new Missile(missileSpawn, 0, 0));
                            lastMissile = DateTime.Now;
                        }
                        break;
                }
            }
        }

        private void PopulateSpawnPoints()
        {
            double canvasPos = 50;

            for (int i = 0; i < 4; i++)
            {
                Coords point = new Coords(canvasPos, 100);
                this.SpawnPoints.Add(point);
                canvasPos += 100;
            }
        }

        private void SpawnEnemies()
        {
            List<Brush> colors = new List<Brush>();
            List<Coords> spawns = this.SpawnPoints;
            colors.Add(Brushes.Red);
            colors.Add(Brushes.Orange);
            colors.Add(Brushes.White);
            colors.Add(Brushes.Violet);

            for (int i = 0; i < 4; i++)
            {
                Random r = new Random();
                int rColor = r.Next(0, 3 - i);
                int rSpawns = r.Next(0, 3 - i);

                this.Objects.Add(new Enemy(colors[rColor], spawns[rSpawns], 0, 0));

                colors.RemoveAt(rColor);
                spawns.RemoveAt(rSpawns);
            }
        }
    }
}
