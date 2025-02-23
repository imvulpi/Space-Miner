using Godot;
using SpaceMiner.src.code.components.commons.other.paths.internal_paths;
using SpaceMiner.src.code.components.user.blocks.barrier_block;
using SpaceMiner.src.code.components.user.blocks.core.factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceMiner.src.code.components.user.blocks.structures
{
    public class StructuresBlockFactory : BlockFactory
    {
        public override Block GetBlock(string[] idParts, int startAt, Block blocksData)
        {
            if (blocksData is OrbitalMaterialsWarehouse orbitalMaterialsWarehouse)
            {
                OrbitalMaterialsWarehouse prefab = ResourceLoader.Load<PackedScene>(InternalPaths.ORBITAL_MATERIALS_STRUCTURE).Instantiate<OrbitalMaterialsWarehouse>();
                prefab.Position = orbitalMaterialsWarehouse.BlockPosition;
                prefab.Owner = orbitalMaterialsWarehouse.Owner;
                return prefab;
            }
            else
            {
                throw new NotImplementedException("This block isn't implemented in this factory");
            }
        }
    }
}
