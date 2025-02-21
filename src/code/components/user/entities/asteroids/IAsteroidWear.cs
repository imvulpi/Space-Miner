using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceMiner.src.code.components.user.entities.asteroids
{
    interface IAsteroidWear
    {
        public Action AsteroidDestroyed { get; set; }
        public int Durability { get; set; }
        public void Hit(int points);
    }
}
