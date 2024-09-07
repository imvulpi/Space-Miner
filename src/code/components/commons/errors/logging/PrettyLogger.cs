using Godot;
using SpaceMiner.src.code.components.commons.errors.exceptions;
using SpaceMiner.src.code.components.processing.data.settings.user.couplers;
using System;
using System.Diagnostics;

namespace SpaceMiner.src.code.components.commons.errors.logging
{
    /// <summary>
    /// Static logger class with pretty formatting and other.<br></br>First initialize using the Init() method and use it in the whole project.
    /// </summary>
    public class PrettyLogger : IPrettyLogger
    {
        private UserSettings userSettings = null;
        private PrettyLogConfig logConfig = new(true);
        /// <summary>
        /// Initialization of PrettyLogger<br></br>Needs to be called before using the logger or the logger might not work.
        /// </summary>
        public void Init()
        {
            try
            {
                userSettings ??= new UserSettings();
                UserSettingCoupler coupler = new UserSettingCoupler();
                coupler.Load(userSettings);
            }
            catch(Exception ex)
            {
                string errorLog = GetFormattedMessage(PrettyErrorType.Critical.ToString(), "user settings", "Loading User settings failed, could not initialize logger.");
                GD.PushError($"{errorLog} - Exception message {ex.Message}");
                throw;
            }
        }

        public void Init(PrettyLogConfig customLogConfig)
        {
            try
            {
                logConfig = customLogConfig;
                userSettings ??= new UserSettings();
                UserSettingCoupler coupler = new UserSettingCoupler();
                coupler.Load(userSettings);
            }
            catch (Exception ex)
            {
                string errorLog = GetFormattedMessage(PrettyErrorType.Critical.ToString(), "user settings", "Loading User settings failed, could not initialize logger.");
                GD.PushError($"{errorLog} - Exception message {ex.Message}");
                throw;
            }
        }


        /// <summary>
        /// Logs a message with a specified log type.
        /// </summary>
        /// <param name="logType">The type of log to be used (ex., Error, Warning, Info).</param>
        /// <param name="customMessage">The custom message to be logged.</param>
        public string Log(PrettyLogType logType, string customMessage)
        {
            CheckInitialization();

            LogGodotFormatted(logType, customMessage);
            return customMessage;
        }

        /// <summary>
        /// Logs a formatted message with a custom type.<br></br>
        /// If description is set to null it might default the description if it's implemented in logger defaults
        /// </summary>
        /// <param name="logType">The type of log to be used (ex., Error, Warning, Info).</param>
        /// <param name="customType">The custom type to be displayed</param>
        /// <param name="reason">The reason or cause of the log, (ex. a component)</param>
        public string Log(PrettyLogType logType, string customType, string reason = "unknown", string description = "", bool includeStackInfo = true)
        {
            CheckInitialization();
            switch (logType)
            {
                case PrettyLogType.Error:
                    if (userSettings.MiscSettings.LoggingSettings.LogErrors)
                    {
                        string logMessage = GetFormattedMessage("ERROR", customType, reason, description, $"{(includeStackInfo == true ? GetStackInfo() : "")}");
                        LogGodotFormatted(logType, logMessage);
                        return logMessage;
                    }
                    return "";
                case PrettyLogType.Warning:
                    if (userSettings.MiscSettings.LoggingSettings.LogErrors)
                    {
                        string logMessage = GetFormattedMessage("WARNING", customType, reason, description, $"{(includeStackInfo == true ? GetStackInfo() : "")}");
                        LogGodotFormatted(logType, logMessage);
                        return logMessage;
                    }
                    return "";
                case PrettyLogType.Info:
                    if (userSettings.MiscSettings.LoggingSettings.LogErrors)
                    {
                        string logMessage = GetFormattedMessage("INFO", customType, reason, description, $"{(includeStackInfo == true ? GetStackInfo() : "")}");
                        LogGodotFormatted(logType, logMessage);
                        return logMessage;
                    }
                    return "";
                default: throw new GameException();
            }
        }
        /// <summary>
        /// Logs a formatted info message.<br></br>
        /// If description is set to null it might default the description if it's implemented in logger defaults
        /// </summary>
        /// <param name="reason">The reason or cause of the log, (ex. a component)</param>
        public string Log(PrettyInfoType infoType, string reason = "unknown", string description = "", bool includeStackInfo = false)
        {
            CheckInitialization();

            if (userSettings.MiscSettings.LoggingSettings.LogInfo)
            {
                description = description == null ? PrettyLoggerDefaults.GetDefaultDescription(infoType) : description;

                string logMessage;
                if (includeStackInfo)
                {
                    logMessage = GetFormattedMessage("INFO", infoType.ToString(), reason, description, GetStackInfo());
                }
                else
                {
                    logMessage = GetFormattedMessage("INFO", infoType.ToString(), reason, description);
                }
                LogGodotFormatted(PrettyLogType.Info, logMessage);
                return logMessage;
            }
            return "Info logging is disabled";
        }
        /// <summary>
        /// Logs a formatted warning message.<br></br>
        /// If description is set to null it might default the description if it's implemented in logger defaults
        /// </summary>
        /// <param name="reason">The reason or cause of the log, (ex. a component)</param>
        public string Log(PrettyWarningType warningType, string reason = "unknown", string description = "")
        {
            CheckInitialization();

            if (userSettings.MiscSettings.LoggingSettings.LogWarnings)
            {
                description = description == null ? PrettyLoggerDefaults.GetDefaultDescription(warningType) : description;

                string logMessage = GetFormattedMessage("WARNING", warningType.ToString(), reason, description, GetStackInfo());
                LogGodotFormatted(PrettyLogType.Warning, logMessage);
                return logMessage;
            }
            return "Warning logging is disabled";
        }
        /// <summary>
        /// Logs a formatted error message.<br></br>
        /// If description is set to null it might default the description if it's implemented in logger defaults
        /// </summary>
        /// <param name="reason">The reason or cause of the log, (ex. a component)</param>
        public string Log(PrettyErrorType errorType, string reason = "unknown", string description = "")
        {
            CheckInitialization();

            if (userSettings.MiscSettings.LoggingSettings.LogErrors)
            {
                description = description == null ? PrettyLoggerDefaults.GetDefaultDescription(errorType) : description;

                string logMessage = GetFormattedMessage("ERROR", errorType.ToString(), reason, description, GetStackInfo());
                LogGodotFormatted(PrettyLogType.Error, logMessage);
                return logMessage;
            }
            return "Error logging is disabled";
        }

        /// <summary>
        /// Logs an error which does not require initialization of the logger, Might miss some features.<br></br>
        /// If description is set to null it might default the description if it's implemented in logger defaults
        /// </summary>
        /// <param name="errorType"></param>
        /// <param name="reason"></param>
        /// <param name="description"></param>
        public string CriticalLog(PrettyErrorType errorType, string reason = "unknown", string description = "", string exceptionMessage = "")
        {
            description = description == null ? PrettyLoggerDefaults.GetDefaultDescription(errorType) : description;
            string logMessage = GetFormattedMessage("ERROR", errorType.ToString(), reason, description);
            if (exceptionMessage != "")
            {
                GD.PushError(logMessage);
            }
            else
            {
                GD.PushError($"{logMessage} - Exception message: {exceptionMessage}");
            }
            return logMessage;
        }

        /// <summary>
        /// Formats log message in a specific way using log configs and other.
        /// </summary>
        public string GetFormattedMessage(string parentType, string type, string reason = "unknown", string description = "", string stackInfo = "")
        {
            return $"{(logConfig.IncludeDate == true ? $"[{GetCurrentDate()}]" : "")}" +
                $"[{parentType}][{type}]<{reason}>: {description}" +
                $"{(stackInfo != "" ? $"\n{stackInfo}" : "")}";
        }

        /// <summary>
        /// Checks whether the initialization was done and whether everything loaded correctly.<br></br>Will attempt to initialize when detects that it was not initialized
        /// </summary>
        private void CheckInitialization()
        {
            if (userSettings == null)
            {
                Init();
            }
        }

        ///<summary>Gets formatted stack information</summary>
        /// <returns></returns>
        private string GetStackInfo()
        {
            string stackInfos = "";
            var frames = new StackTrace().GetFrames();
            int framesCount = logConfig.SkipFramesAmount;
            for (int i = logConfig.SkipFramesAmount; i < frames.Length; i++)
            {
                if (framesCount >= frames.Length) break;
                var frame = new StackFrame(i, true);
                string stackInfo = $"   at: Method: {(frame.GetMethod() == null ? "" : frame.GetMethod().Name)}, " +
                                  $"in File: {frame.GetFileName()}, " +
                                  $"Line: {frame.GetFileLineNumber()}";
                stackInfos += $"{stackInfo}\n";
                framesCount++;
            }
            return stackInfos;
        }

        private string GetCurrentDate()
        {
            return DateTime.Now.ToString();
        }

        /// <summary>
        /// Logs using the GD log functions (writes to file, debugger or godot console).
        /// </summary>
        private void LogGodotFormatted(PrettyLogType logType, string message)
        {
            switch(logType)
            {
                case PrettyLogType.Error:
                    if (userSettings.MiscSettings.LoggingSettings.LogErrors)
                    {
                        GD.PrintErr(message);
                    }
                    break;
                case PrettyLogType.Warning:
                    if (userSettings.MiscSettings.LoggingSettings.LogWarnings)
                    {
                        GD.Print(message);
                    }
                    break;
                case PrettyLogType.Info:
                    if (userSettings.MiscSettings.LoggingSettings.LogInfo)
                    {
                        GD.Print(message);
                    }
                    break;
                default: return;
            }
        }
    }
}
