using Godot;
using GruInject.API.Attributes;
using SpaceMiner.src.code.components.processing.data.settings.user;
using System;

namespace SpaceMiner.src.code.components.processing.data.settings.user.audio
{
    [RegisterTransient]
    public class AudioSettings : IUserSettingCheckable, IAudioSettings, IAudioSettingModify, IAudioSettingsReceive
    {
        public AudioSettings() { }
        public AudioSettings(float masterVolume, float soundEffectsVolume, float musicVolume)
        {
            MasterVolume = masterVolume;
            SoundEffectsVolume = soundEffectsVolume;
            MusicVolume = musicVolume;
        }
        public float MasterVolume { get; set; }
        public float SoundEffectsVolume { get; set; }
        public float MusicVolume { get; set; }
        public bool Check()
        {
            return CheckVolumes(MasterVolume, SoundEffectsVolume, MusicVolume);
        }

        private bool CheckVolume(float volume)
        {
            if (volume < 0 || volume > 100)
            {
                return false;
            }
            return true;
        }

        private bool CheckVolumes(params float[] volumeObjects)
        {
            if (volumeObjects.Length == 0)
            {
                GD.PushWarning("USERSETTINGS - Checking volumes was called, but no values were passed. Returning true");
                return true;
            }
            bool passing = true;
            foreach (object obj in volumeObjects)
            {
                if (!CheckVolume((float)obj))
                {
                    GD.PushError($"USERSETTINGS - Checking volumes values failed, constraints broken (above 100 or below 0) [Name: {obj.GetType().Name}]");
                    passing = false;
                }
            }
            return passing;
        }
        public float GetFractional(float volume)
        {
            return volume / 100;
        }
    }
}