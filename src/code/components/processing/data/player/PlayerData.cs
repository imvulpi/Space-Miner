using ProtoBuf;
using SpaceMiner.src.code.components.processing.world;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceMiner.src.code.components.user.special.player
{
    [ProtoContract]
    public class PlayerData : IPlayerData
    {
        [ProtoMember(1)]
        public float Balance { get; set; }
        [ProtoMember(2)]
        public string Nickname { get; set; }
        [ProtoMember(3)]
        public string UUID { get; set; }
        [ProtoMember(4)]
        public WorldType WorldLocation { get; set; }

        public event EventHandler<float> BalanceChanged;

        public void AddBalance(float amount)
        {
            Balance += amount;
            BalanceChanged.Invoke(this, Balance);
        }

        public void RemoveBalance(float amount)
        {
            Balance -= amount;
            BalanceChanged.Invoke(this, Balance);
        }
    }
}
