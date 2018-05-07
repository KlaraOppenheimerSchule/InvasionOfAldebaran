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

        public static void PlayEnemyExplosion()
        {
            Uri _uriEny = new Uri(@"../../Resources/Media/Soundeffects/hit.wav", UriKind.Relative);

            enemySoundeffect.Open(_uriEny);
            enemySoundeffect.Play();
        }

        public static void PlayFriendlyExplosion()
        {
            Uri _uri = new Uri(@"../../Resources/Media/Soundeffects/explosion.wav", UriKind.Relative);
            friendlySoundeffect.Open(_uri);
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