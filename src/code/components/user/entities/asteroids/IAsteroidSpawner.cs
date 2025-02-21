using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceMiner.src.code.components.user.entities.asteroids
{
    public interface IAsteroidSpawner
    {
        public Node2D PlayerNode { get; set; }

        /// <summary>
        /// The max amount of asteroids
        /// </summary>
        public int MaxAsteroids { get; set; }

        /// <summary>
        /// Distance in blocks around player where asteroids wont spawn unless they have been are already spawner
        /// </summary>
        public int SafeRadiusBlocks { get; set; }
        /// <summary>
        /// Distance in blocks excluding Safe radius where asteroids can spawn
        /// </summary>
        public int MaxSpawnRadiusBlocks { get; set; }
        /// <summary>
        /// Distance in blocks excluding Spawn radius and Safe radius from player where asteroids start despawning
        /// </summary>
        public int DespawnRadiusBlocks { get; set; }
        public void Manage();
    }
}
