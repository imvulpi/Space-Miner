using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceMiner.src.code.components.processing.entities.interfaces.velocity
{
    public interface IVelocityEntity : IEntity
    {
        public Vector2 Velocity { get; set; }
    }
}
