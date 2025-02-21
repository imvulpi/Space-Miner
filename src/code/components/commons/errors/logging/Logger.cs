namespace SpaceMiner.src.code.components.commons.errors.logging
{
    public static class Logger
    {
        public static IPrettyLogger PrettyLogger = new PrettyLogger();
        public const int SkipLoggerFramesAmount = 0;
        public static string CriticalLog(PrettyErrorType errorType, string reason = "unknown", string description = "", string exceptionMessage = "")
        {
            return PrettyLogger.CriticalLog(errorType, reason, description, exceptionMessage);
        }

        public static string GetFormattedMessage(string parentType, string type, string reason = "unknown", string description = "", string stackInfo = "")
        {
            return PrettyLogger.GetFormattedMessage(parentType, type, reason, description, stackInfo);
        }

        public static void Init()
        {
            PrettyLogger.Init();
        }

        public static void Init(PrettyLogConfig loggingConfig)
        {
            PrettyLogger.Init(loggingConfig);
        }

        public static string Log(PrettyLogType logType, string customMessage)
        {
            return PrettyLogger.Log(logType, customMessage);
        }

        public static string Log(PrettyLogType logType, string customType, string reason = "unknown", string description = "", bool includeStackInfo = true)
        {
            return PrettyLogger.Log(logType, customType, reason, description, includeStackInfo);
        }

        public static string Log(PrettyInfoType infoType, string reason = "unknown", string description = "", bool includeStackInfo = false)
        {
            return PrettyLogger.Log(infoType, reason, description, includeStackInfo);
        }

        public static string Log(PrettyWarningType warningType, string reason = "unknown", string description = "")
        {
            return PrettyLogger.Log(warningType, reason, description);
        }

        public static string Log(PrettyErrorType errorType, string reason = "unknown", string description = "")
        {
            return PrettyLogger.Log(errorType, reason, description);
        }
    }
}
