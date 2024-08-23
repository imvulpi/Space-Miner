using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceMiner.src.code.components.processing.data.settings.user.audio
{
    public interface IAudioSettings
    {
        public float GetFractional(float volume);
    }
}
