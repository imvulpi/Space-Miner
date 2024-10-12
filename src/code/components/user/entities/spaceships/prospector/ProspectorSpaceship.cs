using Godot;
using SpaceMiner.src.code.components.processing.entities.implementations.player.spaceship;
using SpaceMiner.src.code.components.processing.entities.implementations.player.spaceship.basic_controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceMiner.src.code.components.processing.data.game.spaceships.prospector
{
    /// Hydrogen (Protium-1) and liquid Oxygen (LOX + LH2) Spaceship
    public partial class ProspectorSpaceship : Spaceship, ISpaceship, ISpaceshipData, IProspectorSpaceship
    {
        public ISpaceshipController<CharacterBody2D> Controller { get; set; }
        public override string ID { get; set; } = "spaceminer.prospector";
        public string SpaceshipName { get; set; } = "Prospector";
        public float MaxFuelCapacity { get; set; } = 700000; // 700k
        public float MaxSpeed { get; set; } = 600;
        public float Acceleration { get; set; } = 70;
        public float RotationSpeed { get; set; } = 9;
        public float CargoCapacity { get; set; } = 300000; // 300k
        public float LoadSpeed { get; set; } = 22000; // 22k

        private float CurrentCargo = 0;

        public override void _Ready()
        {
            Init();
        }

        public override void _PhysicsProcess(double delta)
        {
            Update(delta);
        }

        public void Init()
        {
            Controller ??= new BasicSpaceshipController()
                {
                    Acceleration = Acceleration,
                    RotationSpeed = RotationSpeed,
                    Deceleration = Acceleration*2,
                    MaxSpeed = MaxSpeed,
                };
            return;
        }

        public void Update(double delta)
        {
            Controller.Move(this, delta);
            Controller.Rotate(this, delta);
        }
    }
}
