using JsonEnumTest;
using SpaceMiner.src.code.components.commons.godot.json;
using SpaceMiner.src.code.components.processing.data.settings.user.misc.error_logging;

namespace SpaceMiner.src.code.components.processing.data.settings.user.misc
{
    public class MiscSettings
    {
        public MiscSettings() {
            LoggingSettings = new();
        }
        public ErrorLoggingSettings LoggingSettings { get; set; }
    }
}