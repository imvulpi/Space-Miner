using Godot;
using Hardware.Info;
using SpaceMiner.src.code.components.commons.errors.logging;
using SpaceMiner.src.code.components.commons.other.paths.external_paths;
using System;
using System.IO;
using System.Text;

namespace SpaceMiner.src.code.components.commons.errors.report
{
    public class ErrorReportCollector
    {
        /// <summary>
        /// Gets the default (string) Error report already formatted and ready to use.<br></br>
        /// The Report contains system diagnostics and current gameplay logs content
        /// </summary>
        /// <returns>Formatted Error report</returns>
        public static string GetErrorReport()
        {
            string logContent = GetLogFileContents();
            string systemDiagnostics = GetSystemDiagnostics();
            return $"System Diagnostics:\n{systemDiagnostics}\nLogs contents\n{logContent}";
        }

        /// <summary>
        /// Gets the (string) Error report with additional current exception already formatted and ready to use.<br></br>
        /// The Report contains system diagnostics and current gameplay logs content
        /// </summary>
        /// <returns>Formatted Error report</returns>
        public static string GetErrorReport(Exception currentException)
        {
            string logContent = GetLogFileContents();
            string systemDiagnostics = GetSystemDiagnostics();
            string stringCurrentException = currentException == null ? "Exception not set." : currentException.ToString();
            return $"System Diagnostics:\n{systemDiagnostics}\nEncountered: {stringCurrentException}\n\nLogs.txt contents\n{logContent}";
        }

        /// <summary>
        /// Returns contents of a log file, otherwise a message saying no log was found.
        /// </summary>
        private static string GetLogFileContents()
        {
            Variant? godotLogPath = ProjectSettings.GetSetting("debug/file_logging/log_path");
            if (godotLogPath != null)
            {
                string logPath = ProjectSettings.GlobalizePath((string)godotLogPath);
                try
                {
                    string logContent = File.ReadAllText(logPath);
                    return logContent;
                }catch(IOException ex) // Most likely used by another process
                {
                    PrettyLogger.Log(PrettyErrorType.OperationFailed, $"{ex.GetType()}", $"{ex.Message}. Trying to recover...");
                    string tempLogPath = Path.Join(Godot.OS.GetUserDataDir(), ExternalPaths.TEMP_DIR, "temp_log.txt");
                    GD.Print(tempLogPath);
                    File.Copy(logPath, tempLogPath);
                    string logContent = File.ReadAllText(tempLogPath);
                    PrettyLogger.Log(PrettyInfoType.Success, $"{ex.GetType()}", $"Recovered and log contents retrieved.");
                    File.Delete(tempLogPath);
                    return logContent;
                }
            }
            else
            {
                return $"No log file found | log_path is null!";
            }
        }

        private static string GetSystemDiagnostics()
        {
            StringBuilder systemDiagnostics = new StringBuilder();
            systemDiagnostics.AppendLine($"General:\n");
            systemDiagnostics.AppendLine($"Operating System: {System.Environment.OSVersion}");
            systemDiagnostics.AppendLine($"OS architecture: {(System.Environment.Is64BitOperatingSystem == true ? "64 bit" : "32 bit")}\n");
            var hardwareInfo = new HardwareInfo();
            hardwareInfo.RefreshCPUList();
            hardwareInfo.RefreshMemoryList();
            systemDiagnostics.AppendLine($"CPU:");
            foreach (var cpu in hardwareInfo.CpuList)
            {
                systemDiagnostics.AppendLine($" CPU Name: {cpu.Name}");
                systemDiagnostics.AppendLine($" Number of Cores: {cpu.NumberOfCores}");
                systemDiagnostics.AppendLine($" Number of Logical Processors:  {cpu.NumberOfLogicalProcessors}");
                systemDiagnostics.Append("\n");
            }

            systemDiagnostics.AppendLine("RAM:");
            foreach (var memory in hardwareInfo.MemoryList)
            {
                systemDiagnostics.AppendLine($" Memory Capacity: {memory.Capacity / (1024 * 1024 * 1024)} GB");
            }
            return systemDiagnostics.ToString();
        }
    }
}
