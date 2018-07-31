using InvasionOfAldebaran.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;

namespace InvasionOfAldebaran.Models
{
    public class SpawnHandler
    {
        private readonly double _canvasWidth;
        private readonly double _canvasHeight;
        private readonly Coords _playerSpawn;
        private readonly List<Coords> _spawnPoints;
		private readonly List<AnimatedObject> _missiles;
		private readonly Random _r = new Random();

		private List<string> _alienNames;
		private double _spawnGap;
		private DateTime _lastMissile;
	    private int _waveCounter;
		private double _speedMultiplier = 1;
		private double _multiplierStep = 0.25;
		
		public delegate void SpawnEventHandler(List<AnimatedObject> spawns);

        public event SpawnEventHandler ObjectsSpawned;

        public SpawnHandler(double canvasWidth, double canvasHeight, int numberOfSpawns)
        {
			_canvasWidth = canvasWidth;
            _canvasHeight = canvasHeight;
            _playerSpawn = new Coords(_canvasWidth / 2, _canvasHeight - 65);
            _spawnPoints = new List<Coords>();

            _missiles = new List<AnimatedObject>();
			_alienNames = new List<string>
			{
				"alien1", "alien2", "alien3", "alien4"
			};
            _spawnGap = 0;
	        _waveCounter = 0;

            this.PopulateSpawnPoints(numberOfSpawns);
        }

        private void PopulateSpawnPoints(int spawns)
        {
            _spawnGap = (_canvasWidth - 100) / spawns + 1;
            double canvasPos = _spawnGap;

            for (int i = 0; i < spawns; i++)
            {
                Coords point = new Coords(canvasPos, 40);
                _spawnPoints.Add(point);
                canvasPos += _spawnGap;
            }
        }

        /// <summary>
        /// Spawns a full Wave of enemies and returns a list containing them
        /// </summary>
        /// <returns></returns>
        public List<AnimatedObject> SpawnEnemies()
        {
            var enemies = new List<AnimatedObject>();

			List<string> aliens = new List<string>();
			aliens.AddRange(this._alienNames);
			Shuffle(aliens);

			if (_waveCounter % 5 == 0)
				this._speedMultiplier += this._multiplierStep;

            List<Coords> spawns = _spawnPoints.Select(point => new Coords(point.X, point.Y)).ToList();
            for (int i = 0; i < 4; i++)
            {
                int rSpawns = _r.Next(0, 3 - i);
                int alienRandomizer = _r.Next(0, 3 - i);
                int rSpeed = _r.Next(0, 3);
                string alien = aliens[alienRandomizer];
                string imagePath = @"../../Resources/Images/" + alien + ".png";

                enemies.Add(new Enemy(alien, imagePath, spawns[rSpawns], (Speed)rSpeed, _speedMultiplier, _spawnGap));

                aliens.RemoveAt(alienRandomizer);
                spawns.RemoveAt(rSpawns);
            }
			this._waveCounter++;

			return enemies;
        }

        /// <summary>
        /// Spawns and sets up the Player
        /// </summary>
        /// <returns></returns>
        public Player SpawnPlayer()
        {
            string imagePath = @"../../Resources/Images/playership.png";
            return new Player(imagePath, _playerSpawn);
        }

        /// <summary>
        /// Spawns a Missile directly at the players current position
        /// </summary>
        /// <param name="player"></param>
        public void SpawnMissile(Player player)
        {
            _missiles.Clear();
            if (_lastMissile.AddSeconds(0.3) < DateTime.Now)
            {
                string imagePath = @"../../Resources/Images/laser.png";
                Soundmanager.PlayShotSound();
                var missileSpawn = new Coords((player.Coords.X + (player.Image.ActualWidth / 2)), player.Coords.Y);
                var missile = new Missile(imagePath, missileSpawn);

                _missiles.Add(missile);
                _lastMissile = DateTime.Now;
                this.ObjectsSpawned?.Invoke(_missiles);
            }
        }

		/// <summary>
		/// Fisher-Yates-Shuffle a List
		/// </summary>
		/// <param name="list">List of strings to be shuffled</param>
		private void Shuffle(List<string> list)
		{
			RNGCryptoServiceProvider provider = new RNGCryptoServiceProvider();
			int listCount = list.Count;
			while (listCount > 1)
			{
				byte[] box = new byte[1];
				do provider.GetBytes(box);
				while (!(box[0] < listCount * (Byte.MaxValue / listCount)));
				int k = (box[0] % listCount);
				listCount--;
				string value = list[k];
				list[k] = list[listCount];
				list[listCount] = value;
			}
		}
    }
}