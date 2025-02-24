using Godot;
using ProtoBuf;
using SpaceMiner.src.code.components.processing.entities.implementations.player.spaceship;
using SpaceMiner.src.code.components.processing.entities.implementations.player.spaceship.basic_controller;
using SpaceMiner.src.code.components.processing.world.entities.player;
using SpaceMiner.src.code.components.processing.world.entities.spaceships;
using SpaceMiner.src.code.components.user.entities.asteroids;
using SpaceMiner.src.code.components.user.entities.spaceships;
using System.Collections.Generic;

namespace SpaceMiner.src.code.components.processing.data.game.spaceships.prospector
{
    /// Hydrogen (Protium-1) and liquid Oxygen (LOX + LH2) Spaceship
    [ProtoContract]
    public partial class ProspectorSpaceship : Spaceship, ISpaceship, ICargoSpaceship, IStellarBoostHolder
    {
        public ISpaceshipController<CharacterBody2D> Controller { get; set; }
        public override string ID { get; set; } = "spaceminer.prospector";
        public string SpaceshipName { get; set; } = "Prospector";
        public float MaxFuelCapacity { get; set; } = 700000; // 700k
        public float MaxSpeed { get; set; } = 450;
        public float Acceleration { get; set; } = 70;
        public float RotationSpeed { get; set; } = 9;
        public float CargoCapacity { get; set; } = 10000; // 10k (before: 300k)
        public float LoadSpeed { get; set; } = 22000; // 22k
        [Export] public CollectorModule CollectorModule { get; set; }
        [ProtoMember(3)]
        public CargoModule CargoModule { get; set; }
        public PlayerEntity BoundToEntity { get; set; }
        [ProtoMember(4)]
        public StellarBoosts StellarBoosts { get; set; }

        public override void _Ready()
        {
            StellarBoosts ??= new StellarBoosts(this);
            Init();
            StellarBoosts.ApplyBoosts();
            CollectorModule.Initialize();
            CollectorModule.Collected = (int amount, AsteroidType type) =>
            {
                CargoModule.AddCargo(CargoModule.AsteroidToCargo(type), amount);
            };
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
            CargoModule ??= new CargoModule()
            {
                MaxCapacity = (int)CargoCapacity,
            };
            CargoModule.MaxCapacity = (int)CargoCapacity;
            return;
        }

        public void Update(double delta)
        {
            Controller.Move(this, delta);
            Controller.Rotate(this, delta);
        }

        public void UpdateParameters()
        {
            if(Controller is BasicSpaceshipController basicSpaceshipController)
            {
                basicSpaceshipController.Deceleration = Acceleration * 2;
                basicSpaceshipController.Acceleration = Acceleration;
                basicSpaceshipController.MaxSpeed = MaxSpeed;
                basicSpaceshipController.RotationSpeed = RotationSpeed;
            }
            CargoModule.MaxCapacity = (int)CargoCapacity;
        }
    }
}
