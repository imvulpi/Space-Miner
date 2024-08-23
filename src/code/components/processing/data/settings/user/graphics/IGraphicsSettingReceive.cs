using SpaceMiner.src.code.components.commons.godot.project_settings.display.graphics;
using SpaceMiner.src.code.components.commons.godot.project_settings.display.window.stretch;
using SpaceMiner.src.code.components.processing.data.settings.user.graphics.checkers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Godot.DisplayServer;

namespace SpaceMiner.src.code.components.processing.data.settings.user.graphics
{
    public interface IGraphicsSettingReceive
    {
        public IGraphicsSettingsChecker Checker {get;}
        public WindowMode WindowMode { get; }
        public AspectType AspectType { get; }
        public string Resolution { get; }
        public GraphicsQuality GraphicsQuality { get; }
        public VSyncMode VSync { get; }
        public int ChunkDistance { get; }
    }
}
