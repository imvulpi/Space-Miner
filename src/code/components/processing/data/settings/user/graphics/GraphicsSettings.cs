using Godot;
using SpaceMiner.src.code.components.commons.godot.project_settings.display.graphics;
using SpaceMiner.src.code.components.commons.godot.project_settings.display.window.size;
using SpaceMiner.src.code.components.commons.godot.project_settings.display.window.stretch;
using SpaceMiner.src.code.components.commons.godot.project_settings.display.window.vsync;
using SpaceMiner.src.code.components.processing.data.settings.user.graphics.checkers;
using SpaceMiner.src.code.components.processing.data.settings.user.graphics.helpers;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace SpaceMiner.src.code.components.processing.data.settings.user.graphics
{
    public class GraphicsSettings : IUserSettingCheckable, IGraphicsSettingsModify, IGraphicsSettingReceive
    {
        public GraphicsSettings() {
            GraphicsSettingsChecker defaultChecker = new();
            Checker = defaultChecker;
            SetDefaultValues();
        }
        public GraphicsSettings(IGraphicsSettingsChecker customChecker) { 
            Checker = customChecker;
            SetDefaultValues();
        }
        [JsonIgnore] public IGraphicsSettingsChecker Checker { get; set; }
        public string ScreenMode { get; set; }
        public string AspectType { get; set; }
        public string Resolution { get; set; }
        public string GraphicsQuality { get; set; }
        public string VSync { get; set; }
        public int ChunkDistance { get; set; }

        private void SetDefaultValues()
        {
            ScreenMode ??= "windowed";
            AspectType ??= "keep";
            Vector2I screenSize = DisplayServer.ScreenGetSize();
            Resolution ??= $"{screenSize.X}x{screenSize.Y}";
            GraphicsQuality ??= "high";
            VSync ??= "disabled";
            if (ChunkDistance == 0) {
                ChunkDistance = ChunkDistances.DEFAULT_CHUNK_DISTANCE;
            }
        }
        public bool Check()
        {
            return Checker.Check(this);
        }
    }
}