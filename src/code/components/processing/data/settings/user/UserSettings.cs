using Godot;
using GruInject.API.Attributes;
using SpaceMiner.src.code.components.commons.other.paths;
using SpaceMiner.src.code.components.processing.data.settings.interfaces;
using SpaceMiner.src.code.components.processing.data.settings.user;
using SpaceMiner.src.code.components.processing.data.settings.user.audio;
using SpaceMiner.src.code.components.processing.data.settings.user.graphics;
using SpaceMiner.src.code.components.processing.data.settings.user.misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;

[RegisterSingleton]
public class UserSettings : IEquatable<UserSettings>, ISetting, IUserSettingsModify, IUserSettingsReceive, IUserSettingCheckable
{
    public string Username { get; set; }
    public GraphicsSettings GraphicsSettings { get; set; }
    public AudioSettings AudioSettings { get; set; }
    public MiscSettings MiscSettings { get; set; }
    [JsonIgnore] public string Path { get; set; }
    public UserSettings(string path) {
        Path = path;
        Username = "Unnamed";
        GraphicsSettings = new GraphicsSettings();
        AudioSettings = new AudioSettings();
        MiscSettings = new MiscSettings();
    }

    public UserSettings() {
        Username = "Unnamed";
        GraphicsSettings = new GraphicsSettings();
        AudioSettings = new AudioSettings();
        MiscSettings = new MiscSettings();
    }

    public void Load(string settingsContent)
    {
        UserSettings settings = JsonSerializer.Deserialize<UserSettings>(settingsContent);
        if (settings != null)
        {
            Username = settings.Username;
            GraphicsSettings = settings.GraphicsSettings;
            AudioSettings = settings.AudioSettings;
            MiscSettings = settings.MiscSettings;
            Check();
        }
        else
        {
            GD.PushError("Settings were not parsed correctly by json serializer");
        }
    }

    public string GetData()
    {
        string jsonString = JsonSerializer.Serialize(this, new JsonSerializerOptions() { WriteIndented = true});
        return jsonString;
    }
    public bool Equals(UserSettings other)
    {
        if (Username == other.Username && GraphicsSettings == other.GraphicsSettings && AudioSettings == other.AudioSettings && MiscSettings == other.MiscSettings)
        {
            return true;
        }
        return false;
    }
    
    public bool Check()
    {
        bool[] bools = new bool[] {
            GraphicsSettings.Check(),
            AudioSettings.Check()
        };

        if(bools.Any(a => a == false))
        {
            return false;
        }
        return true;
    }
}
