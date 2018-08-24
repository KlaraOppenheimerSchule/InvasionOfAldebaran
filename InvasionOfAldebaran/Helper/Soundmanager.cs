using System;
using System.Windows.Media;

namespace InvasionOfAldebaran.Helper
{
	public static class Soundmanager
    {
        private static readonly Random r = new Random();
        private static readonly MediaPlayer enemySoundeffect = new MediaPlayer();
        private static readonly MediaPlayer friendlySoundeffect = new MediaPlayer();
        private static readonly MediaPlayer mainThemeSoundeffect = new MediaPlayer();
        private static readonly MediaPlayer gameThemeSoundeffect = new MediaPlayer();
        private static readonly MediaPlayer shotSoundeffect = new MediaPlayer();
        private static readonly MediaPlayer questionSoundeffect = new MediaPlayer();
        private static readonly MediaPlayer screamSoundeffect = new MediaPlayer();

		private static Uri hitSound = new Uri(@"../../Resources/Media/Soundeffects/hit.wav", UriKind.Relative);
		private static Uri newQuestion = new Uri(@"../../Resources/Media/Soundeffects/question.wav", UriKind.Relative);
		private static Uri explosion = new Uri(@"../../Resources/Media/Soundeffects/explosion.wav", UriKind.Relative);
		private static Uri uriScream = new Uri(@"../../Resources/Media/Soundeffects/wilhelmscream.mp3", UriKind.Relative);
		private static Uri laser = new Uri(@"../../Resources/Media/Soundeffects/laser.wav", UriKind.Relative);
		private static Uri inGameTheme = new Uri(@"../../Resources/InGameTheme.mp3", UriKind.Relative);
		private static Uri menuTheme = new Uri(@"../../Resources/MenuTheme.mp3", UriKind.Relative);

		public static void PlayEnemyExplosion()
        {
            var index = r.Next(0, 2);
			Uri sound;

			sound = index == 0 ? explosion : hitSound;

            enemySoundeffect.Open(sound);
            enemySoundeffect.Play();
        }

        public static void PlayNewQuestion()
        {
            questionSoundeffect.Open(newQuestion);
            questionSoundeffect.Play();
        }

        public static void PlayFriendlyExplosion()
        {
            
            friendlySoundeffect.Open(explosion);
            screamSoundeffect.Open(uriScream);
            friendlySoundeffect.Play();
            screamSoundeffect.Play();
        }

        public static void PlayShotSound()
        {
            
            shotSoundeffect.Open(laser);
            shotSoundeffect.Play();
        }

        public static void PlayInGameTheme(bool end)
        {
            if (!end)
            {
                mainThemeSoundeffect.Stop();
                return;
            }
            mainThemeSoundeffect.Open(inGameTheme);
            mainThemeSoundeffect.MediaEnded += InGameTheme_Ended;
            mainThemeSoundeffect.Play();
        }

        public static void PlayMainMenuTheme(bool end)
        {
            if (!end)
            {
                gameThemeSoundeffect.Stop();
                return;
            }
            gameThemeSoundeffect.Open(menuTheme);
            gameThemeSoundeffect.MediaEnded += MenuTheme_Ended;
            gameThemeSoundeffect.Play();
        }

        private static void MenuTheme_Ended(object sender, EventArgs e)
        {
            gameThemeSoundeffect.Position = TimeSpan.Zero;
            gameThemeSoundeffect.Play();
			gameThemeSoundeffect.MediaEnded -= MenuTheme_Ended;
        }

        private static void InGameTheme_Ended(object sender, EventArgs e)
        {
            mainThemeSoundeffect.Position = TimeSpan.Zero;
            mainThemeSoundeffect.Play();
			mainThemeSoundeffect.MediaEnded -= InGameTheme_Ended;
        }
    }
}