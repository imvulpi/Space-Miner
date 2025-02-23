using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceMiner.src.code.components.user.special.player
{
    public interface IPlayerDataCoupler
    {
        PlayerData GetPlayerData(string saveName, string userUUID);
        void SavePlayerData(string saveName, string userUUID, PlayerData playerData);
    }
}
