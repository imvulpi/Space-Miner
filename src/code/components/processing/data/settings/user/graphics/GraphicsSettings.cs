﻿using Godot;
using SpaceMiner.src.code.components.commons.godot.project_settings.display.graphics;
using SpaceMiner.src.code.components.commons.godot.project_settings.display.window.stretch;
using SpaceMiner.src.code.components.commons.other.IO.json;
using SpaceMiner.src.code.components.processing.data.settings.user.graphics.checkers;
using System.Text.Json.Serialization;
using static Godot.DisplayServer;

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
        [JsonFallbackConverter(typeof(JsonDefaultEnumConverter<WindowMode>), WindowMode.Windowed)]
        public WindowMode WindowMode { get; set; }
        [JsonFallbackConverter(typeof(JsonDefaultEnumConverter<AspectType>), AspectType.Keep)]
        public AspectType AspectType { get; set; }
        public string Resolution { get; set; }
        [JsonFallbackConverter(typeof(JsonDefaultEnumConverter<GraphicsQuality>), GraphicsQuality.Medium)]
        public GraphicsQuality GraphicsQuality { get; set; }
        [JsonFallbackConverter(typeof(JsonDefaultEnumConverter<VSyncMode>), VSyncMode.Disabled)]
        public VSyncMode VSync { get; set; }
        public int ChunkDistance { get; set; }

        private void SetDefaultValues()
        {
            Vector2I screenSize = ScreenGetSize();
            Resolution ??= $"{screenSize.X}x{screenSize.Y}";
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