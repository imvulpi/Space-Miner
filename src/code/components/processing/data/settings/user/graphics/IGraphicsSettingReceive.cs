using SpaceMiner.src.code.components.processing.data.settings.user.graphics.checkers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceMiner.src.code.components.processing.data.settings.user.graphics
{
    public interface IGraphicsSettingReceive
    {
        public IGraphicsSettingsChecker Checker {get;}
        public string ScreenMode { get; }
        public string AspectType { get; }
        public string Resolution { get; }
        public string GraphicsQuality { get; }
        public string VSync { get; }
        public int ChunkDistance { get; }
    }
}
