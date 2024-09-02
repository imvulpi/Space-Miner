using JsonEnumTest;
using SpaceMiner.src.code.components.commons.godot.json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceMiner.src.code.components.processing.data.settings.user.misc.error_logging
{
    public class ErrorLoggingSettings
    {
        public bool LogErrors { get; set; }
        public bool LogWarnings { get; set; }
        public bool LogInfo { get; set; }
        public ErrorLoggingSettings() {
            
            LogErrors = true;
            LogWarnings = true;
            LogInfo = false;
        }
    }
}
