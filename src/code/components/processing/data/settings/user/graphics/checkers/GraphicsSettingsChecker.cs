using Godot;
using SpaceMiner.src.code.components.commons.godot.project_settings.display.graphics;
using SpaceMiner.src.code.components.commons.godot.project_settings.display.window.size;
using SpaceMiner.src.code.components.commons.godot.project_settings.display.window.stretch;
using SpaceMiner.src.code.components.commons.godot.project_settings.display.window.vsync;
using SpaceMiner.src.code.components.processing.data.settings.user.graphics.helpers;
using System;
using System.Linq;
using static Godot.DisplayServer;

namespace SpaceMiner.src.code.components.processing.data.settings.user.graphics.checkers
{
    public class GraphicsSettingsChecker : IGraphicsSettingsChecker
    {
        public bool Check(GraphicsSettings graphicsSettings)
        {
            bool[] bools = new bool[] {
                CheckScreenMode(graphicsSettings.ScreenMode),
                CheckAspectType(graphicsSettings.AspectType),
                CheckScreenResolution(graphicsSettings.Resolution),
                CheckGraphicsQuality(graphicsSettings.GraphicsQuality),
                CheckVSync(graphicsSettings.VSync),
                CheckChunkDistance(graphicsSettings.ChunkDistance),
            };
            if (bools.Any(a => a == false))
            {
                return false;
            }
            return true;
        }

        public bool CheckAspectType(string value)
        {
            AspectType aspectType = new AspectTypeHelper().GetAspectType(value);
            return true;
        }

        public bool CheckGraphicsQuality(string value)
        {
            value = value.ToLower();
            try
            {
                GraphicsQuality quality = new GraphicsQualitiesHelper().GetGraphicsQuality(value);
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        public bool CheckScreenMode(string value)
        {
            value = value.ToLower();
            try {
                WindowMode screenMode = new ScreenModesHelper().GetScreenMode(value);
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        public bool CheckScreenResolution(string value)
        {
            (int width, int height) = new ScreenResolutionHelper().GetWidthAndHeight(value);
            if (width == -1 && height == -1)
            {
                return false;
            }
            return true;
        }

        public bool CheckVSync(string value)
        {
            new VsyncModesHelper().GetVSyncMode(value);
        }

        public bool CheckChunkDistance(int value)
        {
            if(value > ChunkDistances.MAX_CHUNK_DISTANCE || value < ChunkDistances.MIN_CHUNK_DISTANCE) { return false; }
            return true;
        }
    }
}
