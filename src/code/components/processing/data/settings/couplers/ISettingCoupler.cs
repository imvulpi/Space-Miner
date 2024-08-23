using SpaceMiner.src.code.components.processing.data.settings.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceMiner.src.code.components.processing.data.settings.couplers
{
    public interface ISettingCoupler
    {
        public void Save(ISetting setting);
        public void Save(ISetting setting, string path);
        public void Load(ISetting setting);
        public void Load(ISetting setting, string path);
    }
}
