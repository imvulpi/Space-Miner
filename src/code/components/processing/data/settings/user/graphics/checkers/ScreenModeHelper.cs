using Godot;
using SpaceMiner.src.code.components.commons.godot.project_settings.display.window.size;

namespace SpaceMiner.src.code.components.processing.data.settings.user.graphics.helpers
{
    public class ScreenModeHelper
    {
        public bool CheckScreenMode(string screenMode)
        {
            screenMode = screenMode.ToLower();
            if (screenMode == StringScreenModes.WINDOWED ||
                screenMode == StringScreenModes.MINIMIZED ||
                screenMode == StringScreenModes.MAXIMIZED ||
                screenMode == StringScreenModes.FULLSCREEN ||
                screenMode == StringScreenModes.EXCLUSIVE_FULLSCREEN)
            {
                return true;
            }
            return false;
        }

        public DisplayServer.WindowMode GetScreenMode(string screenMode)
        {
            screenMode = screenMode.ToLower();
            switch (screenMode)
            {
                case "fullscreen":
                    return DisplayServer.WindowMode.Fullscreen;
                case "windowed":
                    return DisplayServer.WindowMode.Windowed;
                case "maximized":
                    return DisplayServer.WindowMode.Maximized;
                case "minimized":
                    return DisplayServer.WindowMode.Minimized;
                default: return DisplayServer.WindowMode.Windowed;
            }
        }
    }
}
