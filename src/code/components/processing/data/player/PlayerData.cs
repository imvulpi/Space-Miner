using ProtoBuf;
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
        public int Balance { get; set; }
        [ProtoMember(2)]
        public string Nickname { get; set; }
        [ProtoMember(3)]
        public string UUID { get; set; }
    }
}
