using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceMiner.src.code.components.processing.data.settings.user.audio
{
    public interface IAudioSettingsReceive
    {
        public float MasterVolume { get; }
        public float SoundEffectsVolume { get; }
        public float MusicVolume { get; }
    }
}
