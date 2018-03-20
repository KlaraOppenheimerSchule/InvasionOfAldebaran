using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Media;
using InvasionOfAldebaran.Helper;

namespace InvasionOfAldebaran.Models
{
	public class SpawnHandler
	{
		private readonly double _canvasWidth;
		private readonly double _canvasHeight;
		private readonly Coords _playerSpawn;
		private readonly List<Coords> _spawnPoints;
		private Random _r = new Random();

		private readonly InputHandler _inputHandler;

		private DateTime _lastMissile;
		private List<AnimatedObject> _missiles;

		private DateTime _nextSpawnDate;
		private List<AnimatedObject> _nextWave;	 

		private int _currentWave = 0;
		private bool _gameEnded = true;

		public Player Player { get; set; }
		public List<Question> Questions { get; private set; }
		public Question CurrentQuestion { get; private set; }

		public SpawnHandler(Canvas canvas, double canvasWidth, double canvasHeight)
		{
			_canvasWidth = canvasWidth;
			_canvasHeight = canvasHeight;
			_playerSpawn = new Coords(_canvasWidth / 2, _canvasHeight - 50);
			_inputHandler= new InputHandler(canvas);
			_spawnPoints = new List<Coords>();

			_missiles = new List<AnimatedObject>();
			_nextWave = new List<AnimatedObject>();

			this.Questions = this.MakeList();

			this.PopulateSpawnPoints();
		}

		public int Update()
		{
			this.ApplyInputToPlayer();

			if (_gameEnded)
				return 0;

			if (_nextSpawnDate <= DateTime.Now)
			{
				this.SpawnWave();
				_nextSpawnDate = DateTime.Now.AddSeconds(10);
				_currentWave++;
				return _currentWave;
			}
			//else if (_currentWave >= maxWaves)
			//{
			//	MessageBox.Show("Neue Frage!!");
			//	this.StartQuestion();
			//	return _currentWave;
			//}
			else
				return _currentWave;
		}

		private void PopulateSpawnPoints()
		{
			double gap = (_canvasWidth - 100) / 4;
			double canvasPos = 50;

			for (int i = 0; i < 4; i++)
			{
				Coords point = new Coords(canvasPos, 100);
				_spawnPoints.Add(point);
				canvasPos += gap;
			}
		}

		public List<Enemy> SpawnEnemies(Question question)
		{
			var enemies = new List<Enemy>();

			List<Brush> colors = question.Answers.Select(answer => answer.Color).ToList();
			List<Coords> spawns = _spawnPoints.Select(point => new Coords(point.X, point.Y)).ToList();

			for (int i = 0; i < 4; i++)
			{
				int rColor = _r.Next(0, 3 - i);
				int rSpawns = _r.Next(0, 3 - i);
				int rSpeed = _r.Next(0, 3);
				
				enemies.Add(new Enemy(colors[rColor], spawns[rSpawns], (Speed)rSpeed , RandomBool.Get()));

				colors.RemoveAt(rColor);
				spawns.RemoveAt(rSpawns);
			}
			return enemies;
		}

		public Player SpawnPlayer()
		{
			return new Player(Brushes.Blue, _playerSpawn);
		}

		public delegate void SpawnEventHandler(List<AnimatedObject> spawns);
		public event SpawnEventHandler ObjectsSpawned;

		public delegate void QuestionChangedEventHandler(Question question);
		public event QuestionChangedEventHandler QuestionChanged;

		public void SetupPlayer()
		{
			_gameEnded = false;
			this.Player = SpawnPlayer();
			_nextWave.Add(this.Player);

			this.ObjectsSpawned?.Invoke(_nextWave);
			_nextWave.Clear();
		}

		public Question StartQuestion()
		{
			_currentWave = 0;
			var newQuestion = this.Questions.FirstOrDefault();

			if (this.Questions.Count > 0)
			{
				this.CurrentQuestion = newQuestion;
				this.Questions.Remove(newQuestion);

				//this.QuestionChanged?.Invoke(newQuestion);
				_nextSpawnDate = DateTime.Now.AddSeconds(5);

				return this.CurrentQuestion;
			}
			else
			{
				_gameEnded = true;
				return null;
			}
		}

		private void SpawnWave()
		{
			_nextWave.AddRange(SpawnEnemies(this.CurrentQuestion));
			this.ObjectsSpawned?.Invoke(_nextWave);
			_nextWave.Clear();
		}

		private void ApplyInputToPlayer()
		{
			if (_inputHandler.SpacePressed)
			{
				_missiles.Clear();
				if (_lastMissile.AddSeconds(0.3) < DateTime.Now)
				{
					var missile = this.Player.Fire();
					_missiles.Add(missile);
					_lastMissile = DateTime.Now;
					this.ObjectsSpawned?.Invoke(_missiles);
				}
			}

			if (_inputHandler.LeftPressed && !_inputHandler.RightPressed)
				this.Player.Move(Direction.Left);
			else if (_inputHandler.RightPressed && !_inputHandler.LeftPressed)
				this.Player.Move(Direction.Right);
			else if (_inputHandler.LeftPressed && _inputHandler.RightPressed)
				this.Player.Move(Direction.Down);
		}

		private List<Question> MakeList()
		{
			var list = new List<Question>()
			{
				new Question("blöd?",
					new Answer("ja", true),
					new Answer("nein", false),
					new Answer("evtl", false),
					new Answer("hllö", false),
					Difficulty.Easy),

				new Question("schlau?",
					new Answer("ja", true),
					new Answer("nein", false),
					new Answer("evtl", false),
					new Answer("hllö", false),
					Difficulty.Easy)
			};
			return list;
		}
	}
}
