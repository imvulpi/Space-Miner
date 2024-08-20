using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceMiner.src.code.components.processing.boot
{
    internal interface IMainBoot
    {
        public List<Action> BootActions { get; set; }
        public void Boot();
    }
}
