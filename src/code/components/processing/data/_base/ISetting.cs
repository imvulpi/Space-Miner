namespace SpaceMiner.src.code.components.processing.data.settings.interfaces
{
    public interface ISetting
    {
        public string Path { get; set; }
        public void Load(string settingsContent);
        public string GetData();
    }
}