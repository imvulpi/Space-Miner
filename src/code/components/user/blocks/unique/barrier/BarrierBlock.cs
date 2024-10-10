using Godot;
using ProtoBuf;
using SpaceMiner.src.code.components.commons.other.paths.internal_paths;
using SpaceMiner.src.code.components.processing.data.systems.prefabs;

namespace SpaceMiner.src.code.components.user.blocks.barrier_block
{
    [ProtoContract]
    public partial class BarrierBlock : Block
    {
        public override string ID { get; set; } = "spaceminer.unique.barrier";
    }
}
