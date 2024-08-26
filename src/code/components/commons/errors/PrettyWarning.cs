using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceMiner.src.code.components.commons.errors
{
    internal class PrettyWarning
    {
        public PrettyWarningType? WarningType { get; set; }
        public string CustomWarningType { get; set; }
        public string Cause { get; set; }
        public string Description { get; set; } = string.Empty;
        public PrettyWarning(PrettyWarningType warningType, string cause, string description = "")
        {
            WarningType = warningType;
            CustomWarningType = warningType.ToString();
            Cause = cause;
            Description = description;
        }

        public PrettyWarning(string customWarningType, string cause, string description = "")
        {
            CustomWarningType = customWarningType;
            Cause = cause;
            Description = description;
        }

        public override string ToString()
        {
            string warningMessage = $"({(WarningType == null ? CustomWarningType : WarningType)})<{Cause}>{(Description == "" ? "" : $": {Description}")}";
            return warningMessage;
        }
    }
}
