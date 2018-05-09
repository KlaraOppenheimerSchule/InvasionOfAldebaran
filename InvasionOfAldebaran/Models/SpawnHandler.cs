using InvasionOfAldebaran.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Windows.Media;

namespace InvasionOfAldebaran.Models
{
    public class SpawnHandler
    {
        private readonly double _canvasWidth;
        private readonly double _canvasHeight;
        private readonly Coords _playerSpawn;
        private readonly List<Coords> _spawnPoints;
        private readonly Random _r = new Random();
        private double _spawnGap;

        private DateTime _lastMissile;
        private readonly List<AnimatedObject> _missiles;
        private readonly List<Question> _questions;

        public delegate void SpawnEventHandler(List<AnimatedObject> spawns);

        public event SpawnEventHandler ObjectsSpawned;

        public SpawnHandler(double canvasWidth, double canvasHeight, int numberOfSpawns)
        {
            _canvasWidth = canvasWidth;
            _canvasHeight = canvasHeight;
            _playerSpawn = new Coords(_canvasWidth / 2, _canvasHeight - 65);
            _spawnPoints = new List<Coords>();

            _missiles = new List<AnimatedObject>();
            _questions = this.MakeList();
            _spawnGap = 0;

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
        /// <param name="question"></param>
        /// <returns></returns>
        public List<AnimatedObject> SpawnEnemies(Question question)
        {
            var enemies = new List<AnimatedObject>();

            List<string> aliens = question.Answers.Select(answer => answer.Alien).ToList();
            Shuffle(aliens);
            List<Coords> spawns = _spawnPoints.Select(point => new Coords(point.X, point.Y)).ToList();
            for (int i = 0; i < 4; i++)
            {
                int rSpawns = _r.Next(0, 3 - i);
                int alienRandomizer = _r.Next(0, 3 - i);
                int rSpeed = _r.Next(0, 3);
                string alien = aliens[alienRandomizer];
                string imagePath = @"../../Resources/Images/" + alien + ".png";

                enemies.Add(new Enemy(alien, imagePath, spawns[rSpawns], (Speed)rSpeed, _spawnGap));

                aliens.RemoveAt(alienRandomizer);
                spawns.RemoveAt(rSpawns);
            }
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
        /// Returns the next question from the questions array, null if there a no questions left.
        /// </summary>
        /// <returns></returns>
        public Question GetQuestion()
        {
            if (_questions.Count > 0)
            {
	            int index = this._r.Next(0, _questions.Count);

	            var question = _questions[index];
                _questions.Remove(question);
                return question;
            }
            else
                return null;
        }

        /// <summary>
        /// Fisher-Yates-Shuffle a List
        /// </summary>
        /// <param name="list">List of strings to be shuffled</param>
        public void Shuffle(List<string> list)
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
                    Difficulty.Easy),

	            new Question("Welche der folgenden Betriebssysteme bildet die Grundlage für Android?",
		            new Answer("OSX", false),
		            new Answer("Windows Mobile", false),
		            new Answer("Linux", true),
		            new Answer("OS/2", false),
		            Difficulty.Easy),

				new Question("Mit welchen der folgenden Hilfsmittel wird die Programmlogik dokumentiert?",
					new Answer("Compiler", false),
					new Answer("Interpreter", false),
					new Answer("Struktogrammgenerator", true),
					new Answer("Programmgenerator", false),
					Difficulty.Easy),

	            new Question("Welches Tool unterstützt bei einem Whitebox Test?",
		            new Answer("File Editor", false),
		            new Answer("Compiler", false),
		            new Answer("Debugger", true),
		            new Answer("Interpreter", false),
		            Difficulty.Hard),

				new Question("Wer war der erste Bundeskanzler",
					new Answer("Konrad Adenauer", true),
					new Answer("Ludwig Erhard", false),
					new Answer("Willy Brandt", false),
					new Answer("Friedrich Willhelm", false),
					Difficulty.Hard),

	            new Question("In welchem Fall liegt ein zweiseitiges Rechtsgeschäft vor",
		            new Answer("Anfechtung", false),
		            new Answer("Mahnung", false),
		            new Answer("Vermietung", true),
		            new Answer("Kündigung", false),
		            Difficulty.Hard),

                new Question("Auf welcher ISO/OSI-Schicht arbeitet das Protokoll „SMTP“?",
                    new Answer("1", false),
                    new Answer("3", false),
                    new Answer("4", false),
                    new Answer("7", true),
                    Difficulty.Medium),

                new Question("Aus wie vielen Schichten besteht das ISO/OSI-Schichtenmodell?",
                    new Answer("5", false),
                    new Answer("6", false),
                    new Answer("7", true),
                    new Answer("8", false),
                    Difficulty.Medium),

                new Question("Wie lautet die englische Bezeichnung der Sicherungsschicht (2) des ISO/OSI-Modells?",
                    new Answer("Data Link Layer", true),
                    new Answer("Security Layer", false),
                    new Answer("Network Layer", false),
                    new Answer("Physical Layer", false),
                    Difficulty.Medium),

                new Question("Auf welchem Port arbeitet das HTTPS-Protokoll standardmäßig?",
                    new Answer("80", false),
                    new Answer("443", true),
                    new Answer("21", false),
                    new Answer("53", false),
                    Difficulty.Easy),

                new Question("Welches Protokoll nutzt standardmäßig den Port 22?",
                    new Answer("DNS", false),
                    new Answer("SSH", true),
                    new Answer("SMTP", false),
                    new Answer("FTP", false),
                    Difficulty.Medium),

                new Question("Mit welchem Befehl lässt sich unter Windows der Weg eines IP-Pakets nachverfolgen?",
                    new Answer("ping", false),
                    new Answer("ipconfig", false),
                    new Answer("showroute", false),
                    new Answer("tracert", true),
                    Difficulty.Medium),

                new Question("Welcher HTTP-Statuscode signalisiert 'Alles OK!'?",
                    new Answer("200", true),
                    new Answer("300", false),
                    new Answer("400", false),
                    new Answer("500", false),
                    Difficulty.Medium),

                new Question("Welchen Dezimalwert stellt der Hexadezimalwert 'A1' dar?",
                    new Answer("143", false),
                    new Answer("161", true),
                    new Answer("191", false),
                    new Answer("224", false),
                    Difficulty.Hard),

                new Question("Welche IPv6-Adresse zeigt auf den 'localhost'?",
                    new Answer("2002::", false),
                    new Answer("2003::", false),
                    new Answer("::0", false),
                    new Answer("::1", true),
                    Difficulty.Medium),

                new Question("Wie lautet die Standard-Subnetzmaske eines Klasse-C-Netzes?",
                    new Answer("255.0.0.0", false),
                    new Answer("255.255.0.0", false),
                    new Answer("255.255.255.0", true),
                    new Answer("255.255.255.255", false),
                    Difficulty.Medium)
            };
            return list;
        }
    }
}