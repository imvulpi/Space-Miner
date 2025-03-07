﻿using SpaceMiner.src.code.components.processing.data.settings.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceMiner.src.code.components.processing.data.settings.couplers
{
    public interface ISettingCoupler
    {
        public ISettings Save(ISettings setting);
        public ISettings Load(ISettings setting);
    }
}
