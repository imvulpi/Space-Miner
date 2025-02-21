using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceMiner.src.code.components.user.entities.spaceships
{
    [ProtoContract]
    public class Cargo
    {
        [ProtoMember(1)]
        public CargoType CargoType { get; set; }
        [ProtoMember(2)]
        public int Amount { get; set; }
        [ProtoMember(3)]
        public int Weight { get; set; }
    }
}
