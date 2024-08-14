using System;

namespace SpaceMiner.src.code.components.processing.data.settings.interfaces
{
    public interface ISettingModify
    {
        public event Action<ISetting> FileChanged;
    }
}