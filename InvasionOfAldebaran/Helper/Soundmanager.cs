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
        private static MediaPlayer enemySoundeffect = new MediaPlayer();
        private static MediaPlayer friendlySoundeffect = new MediaPlayer();
        private static MediaPlayer mainThemeSoundeffect = new MediaPlayer();
        private static MediaPlayer gameThemeSoundeffect = new MediaPlayer();
        private static MediaPlayer shotSoundeffect = new MediaPlayer();
        private static MediaPlayer questionSoundeffect = new MediaPlayer();
        private static MediaPlayer screamSoundeffect = new MediaPlayer();

        public static void PlayEnemyExplosion()
        {
            Uri _uriEny = new Uri(@"../../Resources/Media/Soundeffects/hit.wav", UriKind.Relative);
            enemySoundeffect.Open(_uriEny);
            enemySoundeffect.Play();
        }

        public static void PlayNewQuestion()
        {
            Uri _uriEny = new Uri(@"../../Resources/Media/Soundeffects/question.wav", UriKind.Relative);
            questionSoundeffect.Open(_uriEny);
            questionSoundeffect.Play();
        }

        public static void PlayFriendlyExplosion()
        {
            Uri Uri = new Uri(@"../../Resources/Media/Soundeffects/hit.wav", UriKind.Relative);
            Uri UriScream = new Uri(@"../../Resources/Media/Soundeffects/wilhemsscream.mp3", UriKind.Relative);
            friendlySoundeffect.Open(Uri);
            screamSoundeffect.Open(UriScream);
            screamSoundeffect.Play();
            friendlySoundeffect.Play();
        }

        public static void PlayShotSound()
        {
            Uri uri = new Uri(@"../../Resources/Media/Soundeffects/laser.wav", UriKind.Relative);
            shotSoundeffect.Open(uri);
            shotSoundeffect.Play();
        }

        public static void PlayTheme(bool end)
        {
            if (end)
            {
                mainThemeSoundeffect.Stop();
                return;
            }
            mainThemeSoundeffect.Open(new Uri(@"../../Resources/MainMenuTheme.mp3", UriKind.Relative));
            mainThemeSoundeffect.Play();
        }

        public static void GameTheme(bool end)
        {
            if (end)
            {
                gameThemeSoundeffect.Stop();
                return;
            }
            gameThemeSoundeffect.Open(new Uri(@"../../Resources/InGameTheme.mp3", UriKind.Relative));
            gameThemeSoundeffect.Play();
        }
    }
}