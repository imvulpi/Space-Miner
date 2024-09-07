using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceMiner.src.code.components.commons.errors.logging
{
    public struct PrettyLogConfig
    {
        public PrettyLogConfig(bool includeDate, int skipFramesAmount = 0)
        {
            this.IncludeDate = includeDate;
            this.SkipFramesAmount = skipFramesAmount;
        }

        public bool IncludeDate;
        public int SkipFramesAmount;
    }
}
