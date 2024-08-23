using Godot;
using static Godot.DisplayServer;

namespace SpaceMiner.src.code.components.commons.godot.project_settings.display.window.vsync
{
    public class VsyncModesHelper
    {
        private const string DISABLED = "Disabled";
        private const string ENABLED = "Enabled";
        private const string ADAPTIVE = "Adaptive";
        private const string MAILBOX = "Mailbox";
        
        public VSyncMode? GetVSyncMode(string mode, bool provideDefaultValue = false)
        {
            switch (mode)
            {
                case DISABLED: return VSyncMode.Disabled;
                case ENABLED: return VSyncMode.Enabled;
                case ADAPTIVE: return VSyncMode.Adaptive;
                case MAILBOX: return VSyncMode.Mailbox;
                default:
                    GD.PushError($"VSync mode: {mode} is not conversible | <String to Enum>");
                    if (provideDefaultValue)
                    {
                        return VSyncMode.Disabled;
                    }
                    return null;
            }
        }

        public string GetVSyncMode(VSyncMode mode, bool provideDefaultValue = false)
        {
            switch (mode)
            {
                case VSyncMode.Disabled: return DISABLED;
                case VSyncMode.Enabled: return ENABLED;
                case VSyncMode.Adaptive: return ADAPTIVE;
                case VSyncMode.Mailbox: return MAILBOX;
                default:
                    GD.PushError($"VSync mode: [{mode}] is not conversible | <Enum to String>");
                    if (provideDefaultValue)
                    {
                        return DISABLED;
                    }
                    return null;
            }
        }
    }
}
