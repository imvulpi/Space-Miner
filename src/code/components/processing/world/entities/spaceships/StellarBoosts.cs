using Godot;
using ProtoBuf;
using SpaceMiner.src.code.components.processing.data.game.spaceships;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceMiner.src.code.components.processing.world.entities.spaceships
{
    [ProtoContract]
    public class StellarBoosts : IStellarBoosts
    {
        public StellarBoosts()
        {

        }
        public StellarBoosts(Spaceship spaceship)
        {
            Spaceship = spaceship;
        }

        [ProtoMember(1)]
        public Dictionary<string, int> BoostsAndAmounts { get; set; } = new();
        public Spaceship Spaceship { get; set; }

        public void ApplyBoosts()
        {
            if (Spaceship is ISpaceship spaceshipInfo)
            {
                foreach ((var k, var v) in BoostsAndAmounts)
                {
                    switch (k)
                    {
                        case "max_speed_plus_10":
                            spaceshipInfo.MaxSpeed += 10;
                            break;
                        default:
                            break;
                    }
                }
                spaceshipInfo.UpdateParameters();
            }
        }
    }
}
