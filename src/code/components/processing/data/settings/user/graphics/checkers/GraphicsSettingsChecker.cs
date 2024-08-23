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
                CheckScreenResolution(graphicsSettings.Resolution),
                CheckChunkDistance(graphicsSettings.ChunkDistance),
            };
            if (bools.Any(a => a == false))
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

        public bool CheckChunkDistance(int value)
        {
            if(value > ChunkDistances.MAX_CHUNK_DISTANCE || value < ChunkDistances.MIN_CHUNK_DISTANCE) { return false; }
            return true;
        }
    }
}
