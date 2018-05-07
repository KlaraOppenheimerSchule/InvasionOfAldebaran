using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace InvasionOfAldebaran.Helper
{
    public class Soundmanager
    {
        private MediaPlayer enemySoundeffect;
        private MediaPlayer friendlySoundeffect;
        private MediaPlayer mainThemeSoundeffect;
        private MediaPlayer shotSoundeffect;

        public Soundmanager()
        {
        }

        public void PlayEnemyExplosion()
        {
            Uri _uriEny = new Uri(@"../../Resources/Media/Soundeffects/hit.wav", UriKind.Relative);
            var soundEffect = new MediaPlayer();
            soundEffect.Open(_uriEny);
            soundEffect.Play();
        }

        public void PlayFriendlyExplosion()
        {
            Uri _uri = new Uri(@"../../Resources/Media/Soundeffects/explosion.wav", UriKind.Relative);
            var soundEffect = new MediaPlayer();
            soundEffect.Open(_uri);
            soundEffect.Play();
            soundEffect.Stop();
        }

        public void PlayShotSound()
        {
            var soundEffect = new MediaPlayer();
            Uri uri = new Uri(@"../../Resources/Media/Soundeffects/laser.wav", UriKind.Relative);
            soundEffect.Open(uri);
            soundEffect.Play();
        }

        public void PlayTheme()
        {
            var soundEffect = new MediaPlayer();
            soundEffect.Open(new Uri(@"../../Resources/themesong.mpeg", UriKind.Relative));
            soundEffect.Play();
        }
    }
}