using Godot;
using ProtoBuf;
using SpaceMiner.src.code.components.commons.other.paths.external_paths;
using SpaceMiner.src.code.components.processing.data.systems.chunking.chunks.chunk.info;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceMiner.src.code.components.user.special.player
{
    public class PlayerDataCoupler : IPlayerDataCoupler
    {
        public PlayerData GetPlayerData(string saveName, string userUUID)
        {
            string playerDataPath = OsPath.Join(ExternalPaths.SAVES_DIR, saveName, ExternalPaths.PLAYER_DATA_DIR, userUUID, ExternalPaths.PLAYER_DATA_FILE);
            CheckPath(saveName, userUUID);
            if (!File.Exists(playerDataPath)) return null;
            using (var stream = File.Open(playerDataPath, FileMode.Open, System.IO.FileAccess.Read))
            {
                return Serializer.Deserialize<PlayerData>(stream);
            }
        }

        public void SavePlayerData(string saveName, string userUUID, PlayerData playerData)
        {
            string playerDataPath = OsPath.Join(ExternalPaths.SAVES_DIR, saveName, ExternalPaths.PLAYER_DATA_DIR, userUUID, ExternalPaths.PLAYER_DATA_FILE);
            CheckPath(saveName, userUUID);
            var fs = File.Create(playerDataPath);
            fs.Dispose();
            using (var stream = File.Open(playerDataPath, FileMode.Open, System.IO.FileAccess.ReadWrite))
            {
                Serializer.Serialize<PlayerData>(stream, playerData as PlayerData);
            }
        }

        private void CheckPath(string saveName, string userUUID)
        {
            string playerDataPath = OsPath.Join(ExternalPaths.SAVES_DIR, saveName, ExternalPaths.PLAYER_DATA_DIR, userUUID);
            Directory.CreateDirectory(playerDataPath);
        }
    }
}
