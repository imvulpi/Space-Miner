using Godot;
using SpaceMiner.src.code.components.processing.data.settings.user.graphics.checkers;

namespace SpaceMiner.src.code.components.processing.data.settings.user.graphics
{
    public class GraphicsSettings : IUserSettingCheckable, IGraphicsSettingsModify, IGraphicsSettingReceive
    {
        public GraphicsSettings() {
            GraphicsSettingsChecker defaultChecker = new GraphicsSettingsChecker();
            Checker = defaultChecker;
        }
        public GraphicsSettings(IGraphicsSettingsChecker customChecker) { 
            Checker = customChecker;
        }
        public IGraphicsSettingsChecker Checker { get; set; }
        public string ScreenMode { get; set; }
        public string AspectType { get; set; }
        public string Resolution { get; set; }
        public string GraphicsQuality { get; set; }
        public string VSync { get; set; }
        private int _chunkDistance = 8;
        private readonly int maxChunkDistance = 32;
        private readonly int minChunkDistance = 8;
        public int ChunkDistance
        {
            get { return _chunkDistance; }
            set
            {
                if (value < maxChunkDistance && value > minChunkDistance)
                {
                    _chunkDistance = value;
                }
                else
                {
                    GD.PushError("Chunk distance not fit in the range, Defaulted chunk distance to 8");
                    _chunkDistance = 8;
                }
            }
        }

        public bool Check()
        {
            return Checker.Check(this);
        }
    }
}