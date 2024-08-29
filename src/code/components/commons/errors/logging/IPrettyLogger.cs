
namespace SpaceMiner.src.code.components.commons.errors.logging
{
    public interface IPrettyLogger
    {
        void Log(string customMessage);
        void Log(string customType, string reason, string description);
        void Log(PrettyInfoType infoType, string reason, string description = "");
        void Log(PrettyWarningType warningType, string reason, string description = "");
        void Log(PrettyErrorType errorType, string reason, string description = "");
    }
}
