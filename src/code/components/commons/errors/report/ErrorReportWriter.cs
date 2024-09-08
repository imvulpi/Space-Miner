using Godot;
using SpaceMiner.src.code.components.commons.errors.exceptions;
using SpaceMiner.src.code.components.commons.other.paths.external_paths;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceMiner.src.code.components.commons.errors.report
{
    public class ErrorReportWriter
    {
        /// <summary>
        /// Writes a report file at a report path constructed from user path and external paths.
        /// </summary>
        /// <param name="errorRaport">The error raport that should be included in that file.</param>
        /// <exception cref="GameException"></exception>
        public static void WriteReport(string errorRaport)
        {
            string reportsPath = Path.Join(Godot.OS.GetUserDataDir(), ExternalPaths.LOGS_DIR, ExternalPaths.REPORTS_DIR);
            string reportName = $"report_{DateTime.Now.ToString("dd_MM_yyyy-HH.mm.ss.ff")}.txt";
            string reportFullPath = Path.Join(reportsPath, reportName);
            if (reportName.IsValidFileName())
            {
                File.WriteAllText(reportFullPath, errorRaport);
                Godot.OS.ShellOpen(reportsPath);
            }
            else
            {
                throw new GameException(PrettyErrorType.Critical, "ReportMenuFail", $"\nReport name {reportName} is not a valid name! (report this)");
            }
        }
    }
}
