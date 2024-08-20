using SpaceMiner.src.code.components.processing.data.settings.user.graphics.checkers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceMiner.src.code.components.processing.data.settings.user.graphics
{
    public interface IGraphicsSettingsModify
    {
        public IGraphicsSettingsChecker Checker { get; set; }
        public string ScreenMode { get; set; }
        public string AspectType { get; set; }
        public string Resolution { get; set; }
        public string GraphicsQuality { get; set; }
        public string VSync { get; set; }
        public int ChunkDistance { get; set; }
    }
}
