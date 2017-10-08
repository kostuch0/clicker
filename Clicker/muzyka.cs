using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;

namespace Clicker
{
    class Muzyka
    {
        Song lastSong,currentSong;
        public void setSong(Song song)
        {
            currentSong= song;
            if(lastSong == currentSong) { } else
            {
                MediaPlayer.Play(song);
                MediaPlayer.IsRepeating = true;
            }
            lastSong = currentSong;
        }

    }
}
