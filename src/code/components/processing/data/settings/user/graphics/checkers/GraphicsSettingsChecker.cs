using SpaceMiner.src.code.components.commons.godot.project_settings.display.graphics;
using SpaceMiner.src.code.components.commons.godot.project_settings.display.window.size;
using SpaceMiner.src.code.components.commons.godot.project_settings.display.window.stretch;
using SpaceMiner.src.code.components.processing.data.settings.user.graphics.helpers;
using System.Linq;

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
            if (value == AspectTypes.IGNORE ||
                value == AspectTypes.KEEP ||
                value == AspectTypes.KEEP_WIDTH ||
                value == AspectTypes.KEEP_HEIGHT ||
                value == AspectTypes.EXPAND)
            {
                return true;
            }
            return false;
        }

        public bool CheckGraphicsQuality(string value)
        {
            value = value.ToLower();
            if (value == GraphicsQualities.HIGH || value == GraphicsQualities.MEDIUM || value == GraphicsQualities.LOW)
            {
                return true;
            }
            return false;
        }

        public bool CheckScreenMode(string value)
        {
            value = value.ToLower();
            if (value == StringScreenModes.WINDOWED ||
                value == StringScreenModes.MINIMIZED ||
                value == StringScreenModes.MAXIMIZED ||
                value == StringScreenModes.FULLSCREEN ||
                value == StringScreenModes.EXCLUSIVE_FULLSCREEN)
            {
                return true;
            }
            return false;
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
            return new VSyncHelper().CheckVSync(value);
        }

        public bool CheckChunkDistance(int value)
        {
            if(value > ChunkDistances.MAX_CHUNK_DISTANCE || value < ChunkDistances.MIN_CHUNK_DISTANCE) { return false; }
            return true;
        }
    }
}
