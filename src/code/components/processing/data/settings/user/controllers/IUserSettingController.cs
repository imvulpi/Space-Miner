using SpaceMiner.src.code.components.processing.data.settings.couplers;
using SpaceMiner.src.code.components.processing.data.settings.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceMiner.src.code.components.processing.data.settings.user.controllers
{
    public interface IUserSettingController
    {
        UserSettings Setting { get; set; }
        SettingCoupler SettingCoupler {  get; set; }
    }
}
