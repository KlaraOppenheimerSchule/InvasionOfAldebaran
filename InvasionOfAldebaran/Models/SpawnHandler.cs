using InvasionOfAldebaran.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Media;

namespace InvasionOfAldebaran.Models
{
    public class SpawnHandler
    {
        private readonly double _canvasWidth;
        private readonly double _canvasHeight;
        private readonly Coords _playerSpawn;
        private readonly List<Coords> _spawnPoints;
        private Random _r = new Random();

        private DateTime _lastMissile;
        private readonly List<AnimatedObject> _missiles;
        private readonly List<Question> _questions;

        public delegate void SpawnEventHandler(List<AnimatedObject> spawns);

        public event SpawnEventHandler ObjectsSpawned;

        public SpawnHandler(double canvasWidth, double canvasHeight)
        {
            _canvasWidth = canvasWidth;
            _canvasHeight = canvasHeight;
            _playerSpawn = new Coords(_canvasWidth / 2, _canvasHeight - 65);
            _spawnPoints = new List<Coords>();

            _missiles = new List<AnimatedObject>();
            _questions = this.MakeList();

            this.PopulateSpawnPoints();
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

        public List<AnimatedObject> SpawnEnemies(Question question)
        {
            var enemies = new List<AnimatedObject>();

            List<string> aliens = question.Answers.Select(answer => answer.Alien).ToList();
            List<Coords> spawns = _spawnPoints.Select(point => new Coords(point.X, point.Y)).ToList();

            for (int i = 0; i < 4; i++)
            {
                int rSpawns = _r.Next(0, 3 - i);
                int alienRandomizer = _r.Next(0, 3 - i);
                int rSpeed = _r.Next(0, 3);
                string alien = aliens[alienRandomizer];
                string imagePath = @"../../Resources/Images/" + alien + ".png";

                enemies.Add(new Enemy(alien, imagePath, spawns[rSpawns], (Speed)rSpeed, RandomBool.Get()));

                aliens.RemoveAt(alienRandomizer);
                spawns.RemoveAt(rSpawns);
            }
            return enemies;
        }

        public Player SpawnPlayer()
        {
            string imagePath = @"../../Resources/Images/playership.png";
            return new Player(imagePath, _playerSpawn);
        }

        /// <summary>
        /// Returns the next question from the questions array, null if there a no questions left.
        /// </summary>
        /// <returns></returns>
        public Question GetQuestion()
        {
            if (_questions.Count > 0)
            {
                var question = _questions.FirstOrDefault();
                _questions.Remove(question);
                return question;
            }
            else
                return null;
        }

        public void SpawnMissile(Player player, MediaPlayer soundEffect)
        {
            _missiles.Clear();
            if (_lastMissile.AddSeconds(0.3) < DateTime.Now)
            {
                string imagePath = @"../../Resources/Images/laser.png";
                Uri uri = new Uri(@"../../Resources/Media/Soundeffects/laser.wav", UriKind.Relative);
                soundEffect.Open(uri);
                soundEffect.Play();
                var missileSpawn = new Coords((player.Coords.X + (player.Image.ActualWidth / 2)), player.Coords.Y);
                var missile = new Missile(imagePath, missileSpawn);

                _missiles.Add(missile);
                _lastMissile = DateTime.Now;
                this.ObjectsSpawned?.Invoke(_missiles);
            }
        }

        private List<Question> MakeList()
        {
            var list = new List<Question>()
            {
                new Question("Wie viel Bits hat ein Byte?",
                    new Answer("2", false),
                    new Answer("4", false),
                    new Answer("8", true),
                    new Answer("16", false),
                    Difficulty.Easy),

                new Question("Wie lang ist eine IPv4 Adresse?",
                    new Answer("16 Bit", false),
                    new Answer("32 Bit", true),
                    new Answer("64 Bit", false),
                    new Answer("128 Bit", false),
                    Difficulty.Easy),

                new Question("Wie viele Sitze hat der Bundesrat?",
                    new Answer("69", true),
                    new Answer("72", false),
                    new Answer("98", false),
                    new Answer("112", false),
                    Difficulty.Easy),

                new Question("Worüber kann man einen Monitor am PC anschließen?",
                    new Answer("USB", false),
                    new Answer("DCMI : ^)", false),
                    new Answer("HSDPA", false),
                    new Answer("DisplayPort", true),
                    Difficulty.Easy)
            };
            return list;
        }
    }
}