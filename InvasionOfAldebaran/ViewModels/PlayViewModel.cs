﻿using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;
using Caliburn.Micro;
using InvasionOfAldebaran.Helper;
using InvasionOfAldebaran.Models;

namespace InvasionOfAldebaran.ViewModels
{
    public sealed class PlayViewModel : NotifyPropertyChangedBase, IScreenViewModel
    {
	    private const int maxWave = 8;

		private readonly FrameWindowViewModel _frameViewModel;
		private readonly DispatcherTimer _timer = new DispatcherTimer();
	    private SpawnHandler _spawner;
	    private InputHandler _inputHandler;

		private List<AnimatedObject> _objectsToBeDeleted;
	    private List<AnimatedObject> _objects;
	    private List<AnimatedObject> _enemies;

		private Question _currentQuestion;
	    private int _currentWave;
	    private DateTime _nextpSpawn;
	    private bool _spawnAllowed;

	    private int _points;
	    private string _message;

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
					this.EndGame();
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

		public delegate void GameEndedEventHandler(int points);
	    public event GameEndedEventHandler GameEnded;

	    public PlayViewModel( FrameWindowViewModel frameWindow)
		{
			_frameViewModel = frameWindow;

			this.Canvas = new Canvas()
            {
                Height = 700,
                Width = 500,
                Focusable = true,
                Background = Brushes.DarkGray,
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
		        _objects.AddRange(_spawner.SpawnEnemies(this.CurrentQuestion));
				_nextpSpawn = DateTime.Now.AddSeconds(10);
		        this.CurrentWave++;
			}
	        if (!_spawnAllowed)
	        {
		        this.CurrentWave = 0;
				this.CurrentQuestion = _spawner.GetQuestion();
				// Ends the game once the questions run out
				if (this.CurrentQuestion == null)
			        this.EndGame();

		        _nextpSpawn = DateTime.Now.AddSeconds(20);
		        _spawnAllowed = true;
	        }

			foreach (var item in _objects)
            {
                item.Animate(_timer.Interval, this.Canvas);

	            if (item.ReachedEnd)
	            {
					_objectsToBeDeleted.Add(item);
		            if (item.GetType() == typeof(Enemy) && item.Color.Equals(this.CurrentQuestion.CorrectAnswer.Color))
			            this.Points++;
		            else
			            this.Points--;
	            }
					
            }

            foreach (var enemy in _objects.OfType<Enemy>())
            {
                foreach (var missile in _objects.OfType<Missile>())
                {
	                if (!enemy.ContainsPoint(missile.Coords.X, missile.Coords.Y))
						continue;

					if (enemy.Color.Equals(this.CurrentQuestion.CorrectAnswer.Color))
					{
						_objectsToBeDeleted.Add(enemy);
						_enemies.Remove(enemy);
						_objectsToBeDeleted.Add(missile);
						this.Message = "That was a friendly ship!";
						this.Points--;
					}
					else
					{
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
				this._spawner.SpawnMissile(this.Player);

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
			this.Player =_spawner.SpawnPlayer();
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

		#endregion

	    #region Interface Members

	    public void CloseWindow()
	    {
		    _frameViewModel.CloseItem(_frameViewModel);
	    }

	    public void ChangeWindow()
	    {
		    _frameViewModel.ActivateItem(_frameViewModel.Items.Single(s => s is MainMenuViewModel));
	    }

	    #endregion
	}
}
