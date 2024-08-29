using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceMiner.src.code.components.commons.errors
{
    public class PrettyInfo
    {
        public PrettyInfoType? InfoType { get; set; }
        public string CustomInfoType { get; set; }
        public string Cause { get; set; }
        public string Description { get; set; } = string.Empty;
        public PrettyInfo(PrettyInfoType infoType, string cause, string description = "")
        {
            InfoType = infoType;
            CustomInfoType = infoType.ToString();
            Cause = cause;
            Description = description;
        }

        public PrettyInfo(string customInfoType, string cause, string description = "")
        {
            CustomInfoType = customInfoType;
            Cause = cause;
            Description = description;
        }

        public override string ToString()
        {
            string infoMessage = $"[INFO]({(InfoType == null ? CustomInfoType : InfoType)})<{Cause}>{(Description == "" ? "" : $": {Description}")}";
            return infoMessage;
        }
    }
}
