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
            };
            if (bools.Any(a => a == false))
            {
                return false;
            }
            return true;
        }

        public bool CheckAspectType(string value)
        {
            if (value == AspectType.IGNORE ||
                value == AspectType.KEEP ||
                value == AspectType.KEEP_WIDTH ||
                value == AspectType.KEEP_HEIGHT ||
                value == AspectType.EXPAND)
            {
                return true;
            }
            return false;
        }

        public bool CheckGraphicsQuality(string value)
        {
            value = value.ToLower();
            if (value == GraphicsQuality.HIGH || value == GraphicsQuality.MEDIUM || value == GraphicsQuality.LOW)
            {
                return true;
            }
            return false;
        }

        public bool CheckScreenMode(string value)
        {
            value = value.ToLower();
            if (value == StringScreenMode.WINDOWED ||
                value == StringScreenMode.MINIMIZED ||
                value == StringScreenMode.MAXIMIZED ||
                value == StringScreenMode.FULLSCREEN ||
                value == StringScreenMode.EXCLUSIVE_FULLSCREEN)
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
    }
}
