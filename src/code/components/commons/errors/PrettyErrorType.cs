using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceMiner.src.code.components.commons.errors
{
    public enum PrettyErrorType
    {
        Error,
        Critical,
        Failed,
        Invalid,
        Timeout,
        Unhandled,
        Corrupted,
        DependencyFailed,
        NotFound,
    }
}
