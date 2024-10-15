using Godot;
using ProtoBuf;
using SpaceMiner.src.code.components.processing.data.game.spaceships.prospector;
using SpaceMiner.src.code.components.user.blocks.barrier_block;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceMiner.src.code.components.processing.data.game.spaceships
{
    [ProtoContract]
    [ProtoInclude(100, typeof(ProspectorSpaceship))]
    public abstract partial class Spaceship : CharacterBody2D
    {
        public abstract string ID { get; set; }
    }
}
