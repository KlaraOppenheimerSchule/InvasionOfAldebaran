﻿using Caliburn.Micro;
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
    public sealed class PlayViewModel : NotifyPropertyChangedBase
    {
        private const int maxWave = 3;
        private const int spawnInterval = 5;
        private const int questionStartTime = 7;
	    private const int maxPoints = 100;
	    private const int friendlyFirePenalty = 3;
	    private const int enemyEscapePenalty = 1;
		private const double timerInterval = 0.014;

        private readonly FrameWindowViewModel _frameViewModel;
        private readonly DispatcherTimer _timer = new DispatcherTimer();

        private SpawnHandler _spawner;
        private InputHandler _inputHandler;

        private List<AnimatedObject> _objectsToBeDeleted;
        private List<AnimatedObject> _objects;
        private List<AnimatedObject> _enemies;

        private Question _currentQuestion;
        private int _currentWave;
        private DateTime _nextSpawn;
        private bool _spawnAllowed;

        private int _points;
        private string _message;
		private int _questionCounter;

		#region Properties

		public Canvas Canvas { get; private set; }
        public Player Player { get; private set; }

		public int QuestionCounter {
			get
			{
				return this._questionCounter;
			}
			private set
			{
				this._questionCounter = value;
				this.NotifyPropertyChanged(nameof(this.QuestionCounter));
			}
		}

		public int Points
        {
            get { return this._points; }
            private set
            {
                this._points = value;
                if (this._points < 0)
                    this._points = 0;
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
                    _currentQuestion = value;
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
	    public double PlayAreaWidth => this.Canvas.Width + 6;

	    #endregion Properties

        public delegate void GameEndedEventHandler(int points);

        public event GameEndedEventHandler GameEnded;

        public PlayViewModel(FrameWindowViewModel frameWindow)
        {
            _frameViewModel = frameWindow;
            ImageBrush backgroundImage = new ImageBrush();
            string imagePath = @"../../Resources/Images/IngameBackground.jpg";
            var imageBitmap = new BitmapImage(new Uri(imagePath, UriKind.Relative));
            backgroundImage.ImageSource = imageBitmap;

	        double canvasHeight = this._frameViewModel.Height - 164;
	        double canvasWidth = this._frameViewModel.Width / 2.55;
			AnimatedObject.CanvasHeight = canvasHeight;

			this.Canvas = new Canvas()
            {
				Height = canvasHeight,
				Width = canvasWidth,
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
            this._inputHandler.ApplyInput();

            if (_currentWave >= maxWave)
                _spawnAllowed = false;

			if (_spawnAllowed && _nextSpawn <= DateTime.Now)
            {
                var enemies = _spawner.SpawnEnemies(this.CurrentQuestion);
                _objects.AddRange(enemies);
                _enemies.AddRange(enemies);
                _nextSpawn = DateTime.Now.AddSeconds(spawnInterval);
                this.CurrentWave++;
            }
            if (!_spawnAllowed && _enemies.Count <= 0)
            {
                this.CurrentWave = 0;
                this.CurrentQuestion = _spawner.GetQuestion();
                Soundmanager.PlayNewQuestion();
                // Ends the game once the maximum question counter is reached
                if (this.CurrentQuestion == null)
                {
                    var result = MessageBox.Show("Die Invasion ist zu Ende!\n" +
                                                 "Die Aldebaranische Flotte ist geschlagen!\n" +
                                                 $"Deine Punkte: {this.Points}", "Gratulation", MessageBoxButton.OK);
                    if (result.Equals(MessageBoxResult.OK))
                        this.EndGame();
                }
				this.QuestionCounter++;
                _nextSpawn = DateTime.Now.AddSeconds(questionStartTime);
                _spawnAllowed = true;
            }

            foreach (var item in _objects)
            {
                item.Animate(_timer.Interval, this.Canvas);

                if (item.ReachedEnd)
                {
                    _objectsToBeDeleted.Add(item);

                    if (item.GetType() != typeof(Enemy))
                        continue;

                    var ship = item as Enemy;
                    if (ship?.GetType() == typeof(Enemy) &&
                        !ship.AlienName.Equals(this.CurrentQuestion?.CorrectAnswer.Alien))
                    {
                        this.Points -= enemyEscapePenalty;
                    }
                }
            }

            foreach (var enemy in _objects.OfType<Enemy>())
            {
                foreach (var missile in _objects.OfType<Missile>())
                {
                    if (!enemy.IntersectsWith(missile.Coords.X, missile.Coords.Y, enemy.Image, missile.Image))
                        continue;

                    if (enemy.AlienName.Equals(this.CurrentQuestion?.CorrectAnswer.Alien))
                    {
                        Soundmanager.PlayFriendlyExplosion();
                        _objectsToBeDeleted.Add(enemy);
                        _enemies.Remove(enemy);
                        _objectsToBeDeleted.Add(missile);
                        this.Message = "That was a friendly ship!";
                        this.Points -= friendlyFirePenalty;
                    }
                    else
                    {
                        Soundmanager.PlayEnemyExplosion();
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

        public void EndGame()
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
            _spawner = new SpawnHandler(this.Canvas.Width, this.Canvas.Height, 4);
			this.Player = _spawner.SpawnPlayer();
			_inputHandler = new InputHandler(this.Canvas, this.Player, this._spawner);

            _timer.Tick += this.AnimateObjects;
            _spawner.ObjectsSpawned += this.AddObjectEventHandler;
			_inputHandler.EscapeKeyPressed += this.EndGame;

            // Setup for Gameplay
            _objects.Add(this.Player);
            this.CurrentQuestion = _spawner.GetQuestion();
            // First Spawn after this amount of seconds
            _nextSpawn = DateTime.Now.AddSeconds(spawnInterval);
            this.CurrentWave = 0;
            _spawnAllowed = true;
            this.Points = 1;
			this.QuestionCounter = 1;
            this.Message = "Shoot the wrong answers!";

            _timer.Interval = TimeSpan.FromSeconds(timerInterval);
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

        private void CloseWindow()
        {
            _frameViewModel.CloseItem(_frameViewModel);
        }

        private void ChangeWindow()
        {
            _frameViewModel.SetScore(this.Points);
			_frameViewModel.ChangeScreen(typeof(MainMenuViewModel));
        }
    }
}