using Godot;
using SpaceMiner.src.code.components.processing.data.settings.user.graphics.helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Godot.DisplayServer;

namespace SpaceMiner.src.code.components.processing.data.settings.user.graphics.checkers
{
    public class VSyncHelper
    {
        private readonly string[] allowedVSyncModes = new string[] { "disabled", "enabled", "adaptive", "mailbox" };
        private readonly Dictionary<VSyncMode, string> vsyncModesStrings = new()
        {
            { VSyncMode.Disabled, "disabled" },
            { VSyncMode.Enabled, "enabled" },
            { VSyncMode.Adaptive, "adaptive" },
            { VSyncMode.Mailbox, "disabled" },
        };
        public bool CheckVSync(string mode)
        {
            mode = mode.ToLower();
            if (allowedVSyncModes.Contains(mode))
            {
                return true;
            }
            return false;
        }

        public string GetVSyncString(VSyncMode mode)
        {
            vsyncModesStrings.TryGetValue(mode, out string stringValue);
            if (CheckVSync(stringValue))
            {
                return stringValue;
            }
            else
            {
                GD.PushError("VSync mode from VSyncs dictionary not found in allowed VSyncs list.");
                return null;
            }
        }

        public VSyncMode GetVSyncMode(string mode)
        {
            mode = mode.ToLower();
            foreach ((VSyncMode enumMode, string stringMode) in vsyncModesStrings)
            {
                if (mode == stringMode)
                {
                    return enumMode;
                }
            }

            GD.PushError($"Didn't find the right VSync mode in VSyncs dictionary, Provided string mode: {mode}");
            return VSyncMode.Disabled;
        }
    }
}
