using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;
using Caliburn.Micro;
using InvasionOfAldebaran.Models;

namespace InvasionOfAldebaran.ViewModels
{
    public sealed class PlayViewModel : NotifyPropertyChangedBase, IScreenViewModel
    {
	    private const int maxWave = 8;

		private readonly FrameWindowViewModel _frameViewModel;
		private readonly DispatcherTimer _timer = new DispatcherTimer();
	    private SpawnHandler _spawner;

		private List<AnimatedObject> _objectsToBeDeleted;
	    private List<AnimatedObject> _objects;
	    private List<AnimatedObject> _enemies;

		private Question _currentQuestion;
	    private int _currentWave;

	    public Canvas Canvas { get; private set; }
		public Question CurrentQuestion
	    {
		    get { return _currentQuestion; }
		    private set
		    {
			    if (value == null) return;

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

	        this.CurrentWave = _spawner.Update();

            foreach (var item in _objects)
            {
                item.Animate(_timer.Interval, this.Canvas);

				if (item.ReachedEnd)
					_objectsToBeDeleted.Add(item);
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
						
						//todo: Minuspunkte!!
						MessageBox.Show("Das war ein eigener!!");
					}
					else
					{
						_objectsToBeDeleted.Add(enemy);
						_enemies.Remove(enemy);
						_objectsToBeDeleted.Add(missile);
						//todo: Pluspunkte!!
					}
                }
            }
			_objectsToBeDeleted.ForEach(obj => _objects.Remove(obj));
			_objectsToBeDeleted.Clear();

	        this.Canvas.Children.Clear();
			_objects.ForEach(item => item.Draw(this.Canvas));

			//todo: Questionwechsel funktioniert noch nicht
	        if (_enemies.Count <= 0 && this.CurrentWave >= maxWave)
		        this.CurrentQuestion = _spawner.StartQuestion();
        }

		#region EventHandler

		private void StartGameEventHandler(object sender, EventArgs e)
	    {
		    _objects = new List<AnimatedObject>();
			_enemies = new List<AnimatedObject>();
		    _objectsToBeDeleted = new List<AnimatedObject>();
		    _spawner = new SpawnHandler(this.Canvas, this.Canvas.Width, this.Canvas.Height);
			_timer.Interval = TimeSpan.FromSeconds(0.005);

			_timer.Tick += this.AnimateObjects;
		    _spawner.ObjectsSpawned += this.AddObjectEventHandler;
		    _spawner.QuestionChanged += this.ChangeQuestionEventHandler;

			_spawner.SetupPlayer();
		    this.CurrentQuestion = _spawner.StartQuestion();
		    _timer.Start();
	    }
		//todo evtl gar nicht benötigt aber dann müssen die Eventhandler woanders deabonniert werden
	    private void EndGameEventHandler(object sender, EventArgs e)
	    {
		    _timer.Tick -= this.AnimateObjects;
		    _spawner.ObjectsSpawned -= this.AddObjectEventHandler;
		    _spawner.QuestionChanged -= this.ChangeQuestionEventHandler;

		    this.ChangeWindow();
	    }

		private void ChangeQuestionEventHandler(Question question)
	    {
		    this.CurrentWave = 0;
		    this.CurrentQuestion = question;
	    }

	    private void AddObjectEventHandler(List<AnimatedObject> newObjects)
	    {
		    if (newObjects.FirstOrDefault() is Enemy)
		    {
			    this.CurrentWave++;
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
