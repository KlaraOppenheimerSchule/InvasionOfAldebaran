using Caliburn.Micro;
using InvasionOfAldebaran.Helper;
using InvasionOfAldebaran.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace InvasionOfAldebaran.ViewModels
{
    public sealed class PlayViewModel : NotifyPropertyChangedBase, IScreenViewModel
    {
        private const int maxWave = 4;

        private readonly FrameWindowViewModel _frameViewModel;
        private readonly DispatcherTimer _timer = new DispatcherTimer();
        private SpawnHandler _spawner;
        private InputHandler _inputHandler;
        private MediaPlayer _soundEffect;

        private List<AnimatedObject> _objectsToBeDeleted;
        private List<AnimatedObject> _objects;
        private List<AnimatedObject> _enemies;

        private Question _currentQuestion;
        private int _currentWave;
        private DateTime _nextpSpawn;
        private bool _spawnAllowed;

        private int _points;
        private string _message;

	    private Uri uri = new Uri(@"../../Resources/Media/Soundeffects/explosion.wav", UriKind.Relative);
	    private Uri uriEny = new Uri(@"../../Resources/Media/Soundeffects/hit.wav", UriKind.Relative);

		#region Properties

		public Canvas Canvas { get; private set; }
        public Player Player { get; private set; }

        public int Points
        {
            get { return this._points; }
            private set
            {
                this._points = value;
                this.NotifyPropertyChanged(nameof(this.Points));
            }
        }

        public string Message
        {
            get { return this._message; }
            private set
            {
                this._message = value;
                this.NotifyPropertyChanged(nameof(this.Message));
            }
        }

        public Question CurrentQuestion
        {
            get { return _currentQuestion; }
            private set
            {
                if (value == null)
                {
                    this.GameEnded?.Invoke(this.Points);
                    return;
                }
                _currentQuestion = value;
                this.NotifyPropertyChanged(nameof(this.CurrentQuestion));
            }
        }

        public int CurrentWave
        {
            get { return _currentWave; }
            private set
            {
                _currentWave = value;
                this.NotifyPropertyChanged(nameof(this.CurrentWave));
            }
        }

        #endregion Properties

        public delegate void GameEndedEventHandler(int points);

        public event GameEndedEventHandler GameEnded;

        public PlayViewModel(FrameWindowViewModel frameWindow)
        {
            _soundEffect = new MediaPlayer();
            _frameViewModel = frameWindow;
            ImageBrush backgroundImage = new ImageBrush();
            string imagePath = @"../../Resources/Images/background.jpg";
            var imageBitmap = new BitmapImage(new Uri(imagePath, UriKind.Relative));
            backgroundImage.ImageSource = imageBitmap;

            this.Canvas = new Canvas()
            {
                Height = 700,
                Width = 500,
                Focusable = true,
                Background = backgroundImage
            };
            this.Activated += this.StartGameEventHandler;
        }

        private void AnimateObjects(object sender, EventArgs e)
        {
            if (!this.Canvas.IsFocused)
                this.Canvas.Focus();

            // Handles Input for the player
            this.ApplyInputToPlayer();

            if (_currentWave >= maxWave)
                _spawnAllowed = false;

	        if (_spawnAllowed && _nextpSpawn <= DateTime.Now)
	        {
		        var enemies = _spawner.SpawnEnemies(this.CurrentQuestion);
				_objects.AddRange(enemies);
				_enemies.AddRange(enemies);
				_nextpSpawn = DateTime.Now.AddSeconds(8);
		        this.CurrentWave++;
			}
	        if (!_spawnAllowed && _enemies.Count <= 0)
	        {
		        this.CurrentWave = 0;
				this.CurrentQuestion = _spawner.GetQuestion();
				// Ends the game once the questions run out
		        if (this.CurrentQuestion == null)
		        {
			       if( MessageBox.Show("You`ve won! Now fuck off!", "Congratulations", MessageBoxButton.OKCancel).Equals(MessageBoxResult.OK))
						this.EndGame();
				}

		        _nextpSpawn = DateTime.Now.AddSeconds(5);
		        _spawnAllowed = true;
	        }

            foreach (var item in _objects)
            {
                item.Animate(_timer.Interval, this.Canvas);

                if (item.ReachedEnd)
                {
                    _objectsToBeDeleted.Add(item);

					if (item.GetType() != typeof(Enemy)) continue;

					var ship = item as Enemy;
	                if (ship.GetType() == typeof(Enemy) && !ship.AlienName.Equals(this.CurrentQuestion.CorrectAnswer.Alien))
		                this.Points--;
                }
            }

            foreach (var enemy in _objects.OfType<Enemy>())
            {
                foreach (var missile in _objects.OfType<Missile>())
                {
                    if (!enemy.IntersectsWith(missile.Coords.X, missile.Coords.Y, enemy.Image, missile.Image))
                        continue;

                    if (enemy.AlienName.Equals(this.CurrentQuestion.CorrectAnswer.Alien))
                    {
                        _soundEffect.Open(uri);
                        _soundEffect.Play();
                        _objectsToBeDeleted.Add(enemy);
                        _enemies.Remove(enemy);
                        _objectsToBeDeleted.Add(missile);
                        this.Message = "That was a friendly ship!";
                        this.Points--;
                    }
                    else
                    {
                        _soundEffect.Open(uriEny);
                        _soundEffect.Play();
                        _objectsToBeDeleted.Add(enemy);
                        _enemies.Remove(enemy);
                        _objectsToBeDeleted.Add(missile);
                        this.Message = "Good hit!";
                        this.Points++;
                    }
                }
            }
            _objectsToBeDeleted.ForEach(obj => _objects.Remove(obj));

            var eny = _objectsToBeDeleted.Where(obj => obj.GetType() == typeof(Enemy)).ToList();
            eny.ForEach(obj => _enemies.Remove(obj));

            _objectsToBeDeleted.Clear();

            this.Canvas.Children.Clear();
            _objects.ForEach(item => item.Draw(this.Canvas));
        }

        private void ApplyInputToPlayer()
        {
            if (_inputHandler.SpacePressed)
                this._spawner.SpawnMissile(this.Player, this._soundEffect);

            if (_inputHandler.LeftPressed && !_inputHandler.RightPressed)
                this.Player.Move(Direction.Left);
            else if (_inputHandler.RightPressed && !_inputHandler.LeftPressed)
                this.Player.Move(Direction.Right);
            else if (_inputHandler.LeftPressed && _inputHandler.RightPressed)
                this.Player.Move(Direction.Down);
        }

        //todo evtl gar nicht benötigt aber dann müssen die Eventhandler woanders deabonniert werden
        private void EndGame()
        {
            _timer.Tick -= this.AnimateObjects;
            _spawner.ObjectsSpawned -= this.AddObjectEventHandler;

            //todo evtl per enum ziel festlegen oder event nehmen
            this.ChangeWindow();
        }

        #region EventHandler

        private void StartGameEventHandler(object sender, EventArgs e)
        {
            // initialization
            _objects = new List<AnimatedObject>();
            _enemies = new List<AnimatedObject>();
            _objectsToBeDeleted = new List<AnimatedObject>();
            _spawner = new SpawnHandler(this.Canvas.Width, this.Canvas.Height);
            _inputHandler = new InputHandler(this.Canvas);

            _timer.Tick += this.AnimateObjects;
            _spawner.ObjectsSpawned += this.AddObjectEventHandler;

            // Setup for Gameplay
            this.Player = _spawner.SpawnPlayer();
            _objects.Add(this.Player);
            this.CurrentQuestion = _spawner.GetQuestion();
            // First Spawn after this amount of seconds
            _nextpSpawn = DateTime.Now.AddSeconds(5);
            this.CurrentWave = 0;
            _spawnAllowed = true;
            this.Points = 0;
            this.Message = "Shoot the wrong answers!";

            _timer.Interval = TimeSpan.FromSeconds(0.005);
            _timer.Start();
        }

        private void AddObjectEventHandler(List<AnimatedObject> newObjects)
        {
            if (newObjects.FirstOrDefault() is Enemy)
            {
                _objects.AddRange(newObjects);
                _enemies.AddRange(newObjects.Where(obj => obj.GetType() == typeof(Enemy)));
            }
            else
                _objects.AddRange(newObjects);
        }

        #endregion EventHandler

        #region Interface Members

        public void CloseWindow()
        {
            _frameViewModel.CloseItem(_frameViewModel);
        }

        public void ChangeWindow()
        {
            _frameViewModel.ActivateItem(_frameViewModel.Items.Single(s => s is MainMenuViewModel));
        }

        #endregion Interface Members
    }
}