using Godot;
using SpaceMiner.src.code.components.processing.data.settings.interfaces;
using SpaceMiner.src.code.components.processing.data.settings.user.audio;
using SpaceMiner.src.code.components.processing.data.settings.user.graphics;
using SpaceMiner.src.code.components.processing.data.settings.user.misc;
using System;
using System.Text.Json;
public class UserSettings : IEquatable<UserSettings>, ISetting, IUserSettingsModify, IUserSettingsReceive
{
    public string Username { get; set; }
    public GraphicsSettings GraphicsSettings { get; set; }
    public AudioSettings AudioSettings { get; set; }
    public MiscSettings MiscSettings { get; set; }
    public string Path { get; set; }
    public UserSettings(bool initializedEmpty, string path)
    {
        GraphicsSettings = new GraphicsSettings();
        AudioSettings = new AudioSettings();
        MiscSettings = new MiscSettings();
        Path = path;
    }

    public UserSettings(string path) => Path = path;
    public UserSettings() { }

    public event Action<ISetting> FileChanged;
    public void Load(string settingsContent)
    {
        UserSettings settings = JsonSerializer.Deserialize<UserSettings>(settingsContent);
        Username = settings.Username;
        GraphicsSettings = settings.GraphicsSettings;
        AudioSettings = settings.AudioSettings;
        MiscSettings = settings.MiscSettings;
    }

    public string GetData()
    {
        string jsonString = JsonSerializer.Serialize(this);
        return jsonString;
    }
    /*        public void CheckOrDefault()
            {
                Username ??= "Unnamed";
                CheckGraphicsSettings();
                CheckAudioSettings();
                CheckMiscSettings();
            }
            private void CheckAudioSettings()
            {
                AudioSettings ??= new AudioSettings();
                AudioSettings.MasterVolume = VolumeUniversalHelper.CheckVolumeUniversalOrDefault(AudioSettings.MasterVolume).Item2;
                AudioSettings.SoundEffectsVolume = VolumeUniversalHelper.CheckVolumeUniversalOrDefault(AudioSettings.SoundEffectsVolume).Item2;
                AudioSettings.MusicVolume = VolumeUniversalHelper.CheckVolumeUniversalOrDefault(AudioSettings.MusicVolume).Item2;
                // Update if more would be added
            }   
            private void CheckGraphicsSettings()
            {
                GraphicsSettings ??= new GraphicsSettings();
                Vector2 screenSize = DisplayServer.ScreenGetSize();
                GraphicsSettings.ScreenMode ??= SettingsConstants.DefaultScreenMode;
                GraphicsSettings.AspectType ??= SettingsConstants.DefaultAspectType;
                GraphicsSettings.AspectType ??= SettingsConstants.DefaultAspectType;
                GraphicsSettings.Resolution ??= ResolutionHelper.GetResolution(screenSize);
                GraphicsSettings.GraphicsQuality ??= SettingsConstants.DefaultGraphicsQuality;
                GraphicsSettings.VSync ??= SettingsConstants.DefaultVSyncMode;
                GraphicsSettings.ChunkDistance = GraphicsSettings.ChunkDistance == 0 ? SettingsConstants.DefaultChunkDistance : GraphicsSettings.ChunkDistance;
                // Update if more would be added
            }

            private void CheckMiscSettings()
            {
                MiscSettings ??= new MiscSettings();
                MiscSettings.ErrorLogging ??= SettingsConstants.DefaultErrorLogging;
            }*/
    public bool Equals(UserSettings other)
    {
        GD.Print($"{Username == other.Username} {GraphicsSettings.Equals(other.GraphicsSettings)} {AudioSettings == other.AudioSettings} {MiscSettings == other.MiscSettings}");
        if (Username == other.Username && GraphicsSettings == other.GraphicsSettings && AudioSettings == other.AudioSettings && MiscSettings == other.MiscSettings)
        {
            return true;
        }
        return false;
    }
}
