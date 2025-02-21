using Godot;
using SpaceMiner.src.code.components.commons.other.paths.internal_paths;
using SpaceMiner.src.code.components.user.blocks.barrier_block;
using SpaceMiner.src.code.components.user.blocks.core.factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SpaceMiner.src.code.components.user.blocks.unique.barrier
{
    public class BarrierBlockFactory : BlockFactory
    {
        public override Block GetBlock(string[] idParts, int startAt, Block blocksData)
        {
            if(blocksData is BarrierBlock barrierData)
            {
                BarrierBlock prefab = ResourceLoader.Load<PackedScene>(InternalPaths.BARRIER_BLOCK).Instantiate<BarrierBlock>();
                prefab.Position = barrierData.BlockPosition;
                prefab.Owner = barrierData.Owner;
                return prefab;
            }
            else
            {
                throw new NotImplementedException("This block isn't implemented in this factory");
            }
        }
    }
}
