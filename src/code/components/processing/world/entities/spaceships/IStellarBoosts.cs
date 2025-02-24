using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceMiner.src.code.components.processing.world.entities.spaceships
{
    [ProtoContract]
    public interface IStellarBoosts
    {
        [ProtoMember(1)]
        public Dictionary<string, int> BoostsAndAmounts { get; set; }
        public void ApplyBoosts();
    }
}
