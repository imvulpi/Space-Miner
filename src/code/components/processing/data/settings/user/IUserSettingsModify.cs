using SpaceMiner.src.code.components.processing.data.settings.user.audio;
using SpaceMiner.src.code.components.processing.data.settings.user.graphics;
using SpaceMiner.src.code.components.processing.data.settings.user.misc;

namespace SpaceMiner.src.code.components.processing.data.settings.user
{
    public interface IUserSettingsModify
    {
        public string Username { get; set; }
        public GraphicsSettings GraphicsSettings { get; set; }
        public AudioSettings AudioSettings { get; set; }
        public MiscSettings MiscSettings { get; set; }
    }
}