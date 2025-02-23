using Godot;
using ProtoBuf;
using SpaceMiner.src.code.components.processing.entities.implementations.player.spaceship;
using SpaceMiner.src.code.components.processing.entities.implementations.player.spaceship.basic_controller;
using SpaceMiner.src.code.components.user.entities.asteroids;
using SpaceMiner.src.code.components.user.entities.spaceships;
using SpaceMiner.src.code.components.user.special.player;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceMiner.src.code.components.processing.data.game.spaceships.prospector
{
    /// Hydrogen (Protium-1) and liquid Oxygen (LOX + LH2) Spaceship
    [ProtoContract]
    public partial class ProspectorSpaceship : Spaceship, ISpaceship, ISpaceshipData, ICargoSpaceship
    {
        public ISpaceshipController<CharacterBody2D> Controller { get; set; }
        public override string ID { get; set; } = "spaceminer.prospector";
        public string SpaceshipName { get; set; } = "Prospector";
        public float MaxFuelCapacity { get; set; } = 700000; // 700k
        public float MaxSpeed { get; set; } = 500;
        public float Acceleration { get; set; } = 70;
        public float RotationSpeed { get; set; } = 9;
        public float CargoCapacity { get; set; } = 10000; // 10k (before: 300k)
        public float LoadSpeed { get; set; } = 22000; // 22k
        [Export] public SpaceshipHUD spaceshipHUD { get; set;}
        [Export] public CollectorModule CollectorModule { get; set; }
        [Export] public CargoViewController cargoViewController { get; set; }
        [ProtoMember(3)]
        public CargoModule CargoModule { get; set; }
        public IPlayerData PlayerData { get; set; }

        public ProspectorSpaceship()
        {
        }

        public override void _Ready()
        {
            Init();
            cargoViewController.CargoModule = CargoModule;
            CollectorModule.Initialize();
            CollectorModule.Collected = (int amount, AsteroidType type) =>
            {
                CargoModule.AddCargo(CargoModule.AsteroidToCargo(type), amount);
                spaceshipHUD.CargoCapacity.Text = $"{CargoModule.CurrentCapacity}/{CargoModule.MaxCapacity}";
                if (CargoModule.CurrentCapacity >= CargoModule.MaxCapacity) {
                    spaceshipHUD.LabelRight.Text = "No space!";
                }
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
    }
}
