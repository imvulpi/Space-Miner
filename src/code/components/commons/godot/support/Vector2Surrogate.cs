using Godot;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceMiner.src.code.components.commons.godot.support
{
    [ProtoContract]
    public class Vector2Surrogate
    {
        [ProtoMember(1)]
        public float X { get; set; }

        [ProtoMember(2)]
        public float Y { get; set; }

        public static implicit operator Vector2Surrogate(Vector2 vector)
        {
            return new Vector2Surrogate { X = vector.X, Y = vector.Y };
        }

        public static implicit operator Vector2(Vector2Surrogate surrogate)
        {
            return new Vector2(surrogate.X, surrogate.Y);
        }
    }
}
