namespace SpaceMiner.src.code.components.processing.data.settings.interfaces
{
    public interface ISettingReader
    {
        public void ReadSettings(ISetting setting);
        public string GetStringSettings(ISetting setting);
    }
}