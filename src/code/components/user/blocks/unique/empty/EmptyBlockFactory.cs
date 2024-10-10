using Godot;
using SpaceMiner.src.code.components.commons.other.paths.internal_paths;
using SpaceMiner.src.code.components.user.blocks.barrier_block;
using SpaceMiner.src.code.components.user.blocks.core.factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceMiner.src.code.components.user.blocks.unique.empty
{
    internal class EmptyBlockFactory : BlockFactory
    {
        public override Block GetBlock(string[] idParts, int startAt, Block blocksData)
        {
            if (blocksData is Block blockData)
            {
                Block prefab = ResourceLoader.Load<PackedScene>(BlockPaths.EMPTY_BLOCK).Instantiate<BarrierBlock>();
                prefab.Position = blockData.BlockPosition;
                prefab.Owner = blockData.Owner;
                return prefab;
            }
            else
            {
                throw new NotImplementedException("This block isn't implemented in this factory");
            }
        }
    }
}
