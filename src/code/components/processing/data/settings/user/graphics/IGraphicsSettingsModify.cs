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
    public interface IGraphicsSettingsModify
    {
        public IGraphicsSettingsChecker Checker { get; set; }
        public WindowMode WindowMode { get; set; }
        public AspectType AspectType { get; set; }
        public string Resolution { get; set; }
        public GraphicsQuality GraphicsQuality { get; set; }
        public VSyncMode VSync { get; set; }
        public int ChunkDistance { get; set; }
    }
}
