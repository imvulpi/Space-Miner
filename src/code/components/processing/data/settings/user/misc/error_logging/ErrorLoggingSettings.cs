
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
