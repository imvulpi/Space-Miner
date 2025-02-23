using ProtoBuf;

namespace SpaceMiner.src.code.components.user.blocks.barrier_block
{
    [ProtoContract]
    public partial class BarrierBlock : Block, IOrganizedStructure
    {
        public override string ID { get; set; } = "spaceminer.unique.barrier";
    }
}
