using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceMiner.src.code.components.processing.data.settings.user.audio
{
    public interface IAudioSettingModify
    {
        public float MasterVolume { get; set; }
        public float SoundEffectsVolume { get; set; }
        public float MusicVolume { get; set; }
    }
}
