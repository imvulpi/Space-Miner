using Godot;
using static Godot.DisplayServer;

namespace SpaceMiner.src.code.components.commons.godot.project_settings.display.window.size
{
    public class WindowModesHelper
    {
        private const string WINDOWED = "windowed";
        private const string MINIMIZED = "minimized";
        private const string MAXIMIZED = "maximized";
        private const string FULLSCREEN = "fullscreen";
        private const string EXCLUSIVE_FULLSCREEN = "exclusive_fullscreen";
       
        public WindowMode? GetWindowMode(string mode, bool provideDefaultValue = false)
        {
            switch(mode)
            {
                case WINDOWED : return WindowMode.Windowed;
                case MINIMIZED : return WindowMode.Minimized;
                case MAXIMIZED : return WindowMode.Maximized;
                case FULLSCREEN : return WindowMode.Fullscreen;
                case EXCLUSIVE_FULLSCREEN : return WindowMode.ExclusiveFullscreen;
                default:
                    GD.PushError($"Screen mode: [{mode}] is not conversible | <String to Enum>");
                    if (provideDefaultValue)
                    {
                        return WindowMode.Windowed;
                    }
                    return null;
            }
        }

        public string GetWindowMode(WindowMode screenMode, bool provideDefaultValue = false)
        {
            switch (screenMode)
            {
                case WindowMode.Windowed: return WINDOWED;
                case WindowMode.Minimized : return MINIMIZED;
                case WindowMode.Maximized : return MAXIMIZED;
                case WindowMode.Fullscreen : return FULLSCREEN;
                case WindowMode.ExclusiveFullscreen : return EXCLUSIVE_FULLSCREEN;
                default:
                    GD.PushError($"Screen mode: [{screenMode}] is not conversible | <Enum to String>");
                    if (provideDefaultValue)
                    {
                        return WINDOWED;
                    }
                    return null;
            }
        }
    }
}
