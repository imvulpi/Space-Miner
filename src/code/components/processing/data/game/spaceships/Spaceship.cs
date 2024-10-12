using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceMiner.src.code.components.processing.data.game.spaceships
{
    public abstract partial class Spaceship : CharacterBody2D
    {
        public abstract string ID { get; set; }
    }
}
