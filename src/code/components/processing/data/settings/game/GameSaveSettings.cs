﻿using SpaceMiner.src.code.components.commons.godot.project_settings.game.world;
using SpaceMiner.src.code.components.commons.other.IO.json;
using SpaceMiner.src.code.components.processing.data.settings.interfaces;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SpaceMiner.src.code.components.processing.data.settings.game
{
    public class GameSaveSettings : IGameSettings
    {
        public string SaveName { get; set; }
        [JsonFallbackConverter(typeof(JsonDefaultEnumConverter<WorldType>), WorldType.Prebuild)]
        public WorldType WorldType { get; set; }
        [JsonFallbackConverter(typeof(JsonDefaultEnumConverter<GameDifficulty>), GameDifficulty.Easy)]
        public GameDifficulty GameDifficulty { get; set; }
        public DateTime LastPlayed { get; set; }
        [JsonIgnore] public string Path { get; set; }

        public GameSaveSettings() {
            // TODO: Check if name is available.
            SaveName = "new_game";
            LastPlayed = DateTime.Now;
        }

        public GameSaveSettings(string saveName, WorldType worldType, GameDifficulty gameDifficulty, DateTime lastPlayed, string path)
        {
            SaveName = saveName;
            WorldType = worldType;
            GameDifficulty = gameDifficulty;
            LastPlayed = lastPlayed;
            Path = path;
        }

        public void Load(string settingsContent)
        {
            GameSaveSettings settings = JsonSerializer.Deserialize<GameSaveSettings>(settingsContent);
            SaveName = settings.SaveName;
            WorldType = settings.WorldType;
            GameDifficulty = settings.GameDifficulty;
        }

        public string GetData()
        {
            string json = JsonSerializer.Serialize(this, new JsonSerializerOptions() { WriteIndented = true });
            return json;
        }
    }
}
