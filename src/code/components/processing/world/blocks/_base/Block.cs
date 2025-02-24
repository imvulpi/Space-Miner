using Godot;
using ProtoBuf;
using SpaceMiner.src.code.components.commons.other.paths.internal_paths;
using SpaceMiner.src.code.components.user.blocks.barrier_block;

namespace SpaceMiner.src.code.components.user.blocks
{
    [ProtoContract()]
    [ProtoInclude(100, typeof(BarrierBlock))]
    [ProtoInclude(120, typeof(OrbitalMaterialsWarehouse))]
    [ProtoInclude(140, typeof(StellarForge))]
    public partial class Block : Node2D, IBlockInfo
    {
        [ProtoMember(1)]
        public virtual string ID { get; set; } = "spaceminer.unique.empty";
        [ProtoMember(2)]
        public Vector2 BlockPosition { get => Position; set => Position = value; }
        public virtual Block CreatePrefab()
        {
            return ResourceLoader.Load<PackedScene>(InternalPaths.EMPTY_BLOCK).Instantiate<Block>();
        }
    }
}
