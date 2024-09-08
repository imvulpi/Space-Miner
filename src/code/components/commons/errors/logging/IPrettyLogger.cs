using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceMiner.src.code.components.commons.errors.logging
{
    public interface IPrettyLogger
    {
        void Init();
        void Init(PrettyLogConfig loggingConfig);
        string Log(PrettyLogType logType, string customMessage);
        string Log(PrettyLogType logType, string customType, string reason = "unknown", string description = "", bool includeStackInfo = true);
        string Log(PrettyInfoType infoType, string reason = "unknown", string description = "", bool includeStackInfo = false);
        string Log(PrettyWarningType warningType, string reason = "unknown", string description = "");
        string Log(PrettyErrorType errorType, string reason = "unknown", string description = "");
        string CriticalLog(PrettyErrorType errorType, string reason = "unknown", string description = "", string exceptionMessage = "");
        string GetFormattedMessage(string parentType, string type, string reason = "unknown", string description = "", string stackInfo = "");
    }
}
