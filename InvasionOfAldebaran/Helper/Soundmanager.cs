using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public static void PlayEnemyExplosion()
        {
            var index = r.Next(0, 2);
            Uri uriEny;

            uriEny = index == 0 ? new Uri(@"../../Resources/Media/Soundeffects/explosion.wav", UriKind.Relative)
                : new Uri(@"../../Resources/Media/Soundeffects/hit.wav", UriKind.Relative);

            enemySoundeffect.Open(uriEny);
            enemySoundeffect.Play();
        }

        public static void PlayNewQuestion()
        {
            Uri uriEny = new Uri(@"../../Resources/Media/Soundeffects/question.wav", UriKind.Relative);
            questionSoundeffect.Open(uriEny);
            questionSoundeffect.Play();
        }

        public static void PlayFriendlyExplosion()
        {
            Uri uri = new Uri(@"../../Resources/Media/Soundeffects/explosion.wav", UriKind.Relative);
            Uri uriScream = new Uri(@"../../Resources/Media/Soundeffects/wilhelmscream.mp3", UriKind.Relative);
            friendlySoundeffect.Open(uri);
            screamSoundeffect.Open(uriScream);
            friendlySoundeffect.Play();
            screamSoundeffect.Play();
        }

        public static void PlayShotSound()
        {
            Uri uri = new Uri(@"../../Resources/Media/Soundeffects/laser.wav", UriKind.Relative);
            shotSoundeffect.Open(uri);
            shotSoundeffect.Play();
        }

        public static void PlayInGameTheme(bool end)
        {
            if (!end)
            {
                mainThemeSoundeffect.Stop();
                return;
            }
            mainThemeSoundeffect.Open(new Uri(@"../../Resources/InGameTheme.mp3", UriKind.Relative));
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
            gameThemeSoundeffect.Open(new Uri(@"../../Resources/MenuTheme.mp3", UriKind.Relative));
            gameThemeSoundeffect.MediaEnded += MenuTheme_Ended;
            gameThemeSoundeffect.Play();
        }

        private static void MenuTheme_Ended(object sender, EventArgs e)
        {
            gameThemeSoundeffect.Position = TimeSpan.Zero;
            gameThemeSoundeffect.Play();
        }

        private static void InGameTheme_Ended(object sender, EventArgs e)
        {
            mainThemeSoundeffect.Position = TimeSpan.Zero;
            mainThemeSoundeffect.Play();
        }
    }
}