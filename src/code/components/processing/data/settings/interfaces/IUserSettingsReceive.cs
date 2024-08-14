
using SpaceMiner.src.code.components.processing.data.settings.user.audio;
using SpaceMiner.src.code.components.processing.data.settings.user.graphics;
using SpaceMiner.src.code.components.processing.data.settings.user.misc;

public interface IUserSettingsReceive
{
    public string Username { get; }
    public GraphicsSettings GraphicsSettings { get; }
    public AudioSettings AudioSettings { get; }
    public MiscSettings MiscSettings { get; }
}

