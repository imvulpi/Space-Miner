namespace SpaceMiner.src.code.components.processing.data.settings.interfaces
{
    public interface ISettings
    {
        public string Path { get; set; }
        public void Load(string settingsContent);
        public string GetData();
    }
}