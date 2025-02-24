using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceMiner.src.code.components.processing.world.entities.player
{
    public interface IPlayerEntityHandler
    {
        public PlayerEntity ConstructPlayer();
        public void SavePlayer(PlayerEntity player);
    }
}
