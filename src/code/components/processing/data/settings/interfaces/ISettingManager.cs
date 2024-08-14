using System.Collections.Generic;

namespace SpaceMiner.src.code.components.processing.data.settings.interfaces
{
    public interface ISettingManager
    {
        public ISettingReader Reader { get; set; }
        public ISettingSaver Saver { get; set; }
        public List<(ISetting, ISettingModify)> SettingValues { get; set; }
        public void SettingsFileChanged(ISetting obj);
    }
}