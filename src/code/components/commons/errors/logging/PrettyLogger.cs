﻿using Godot;
using SpaceMiner.src.code.components.commons.errors.exceptions;
using SpaceMiner.src.code.components.processing.data.settings.user.couplers;
using System;
using System.Diagnostics;

namespace SpaceMiner.src.code.components.commons.errors.logging
{
    /// <summary>
    /// Static logger class with pretty formatting and other.<br></br>First initialize using the Init() method and use it in the whole project.
    /// </summary>
    public class PrettyLogger
    {
        private static UserSettings userSettings = null;
        private static PrettyLogConfig logConfig = new(true);
        /// <summary>
        /// Initialization of PrettyLogger<br></br>Needs to be called before using the logger or the logger might not work.
        /// </summary>
        public static void Init()
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

        public static void Init(PrettyLogConfig customLogConfig)
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
        public static string Log(PrettyLogType logType, string customMessage)
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
        public static string Log(PrettyLogType logType, string customType, string reason, string description = null, bool includeStackInfo = true)
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
        public static string Log(PrettyInfoType infoType, string reason, string description = null, bool includeStackInfo = false)
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
        public static string Log(PrettyWarningType warningType, string reason, string description = null)
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
        public static string Log(PrettyErrorType errorType, string reason, string description = null)
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
        public static string CriticalLog(PrettyErrorType errorType, string reason, string description = null, string exceptionMessage = "")
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
        public static string GetFormattedMessage(string parentType, string type, string reason, string description = "", string stackInfo = "")
        {
            return $"{(logConfig.includeDate == true ? $"[{GetCurrentDate()}]" : "")}" +
                $"[{parentType}][{type}]<{reason}>: {description}" +
                $"{(stackInfo != "" ? $"\n    at {stackInfo}" : "")}";
        }

        /// <summary>
        /// Checks whether the initialization was done and whether everything loaded correctly.<br></br>Will attempt to initialize when detects that it was not initialized
        /// </summary>
        private static void CheckInitialization()
        {
            if (userSettings == null)
            {
                Init();
            }
        }

        ///<summary>Gets formatted stack information</summary>
        /// <param name="methodDepth">How many methods boefore the current method (this one) should be displayed.<br></br>When used inside Log methods it will be 2.</param>
        /// <returns></returns>
        private static string GetStackInfo(int methodDepth = 2)
        {
            var frame = new StackFrame(methodDepth, true);
            string stackInfo = $"Method: {frame.GetMethod().Name}, " +
                              $"File: {frame.GetFileName()}, " +
                              $"Line: {frame.GetFileLineNumber()}";
            return stackInfo;
        }

        private static string GetCurrentDate()
        {
            return DateTime.Now.ToString();
        }

        /// <summary>
        /// Logs using the GD log functions (writes to file, debugger or godot console).
        /// </summary>
        private static void LogGodotFormatted(PrettyLogType logType, string message)
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
