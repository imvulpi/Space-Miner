using DryIoc;
using Godot;
using Godot.Collections;
using GruInject.Core.Injection;
using SpaceMiner.src.code.components.commons.other.DI;
using SpaceMiner.src.code.components.user.entities.asteroids;
using SpaceMiner.src.code.components.user.entities.entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SpaceMiner.src.code.components.user.entities.spaceships
{
    public partial class CollectorModule : Node2D, ICollectorModule
    {
        [Export] public Area2D ScanArea { get; set; }
        [Export] public SpaceshipHUD SpaceshipHUD {get; set;}
        public AsteroidType[] CollectTypes { get; set; } = new AsteroidType[] { AsteroidType.Rocky, AsteroidType.Bronze };
        public Action<int, AsteroidType> Collected;
        private Timer timer;
        private bool collecting = false;
        private AsteroidEntity asteroidBeingCollected = null;
        private System.Collections.Generic.Dictionary<AsteroidType, int> CollectSpeedS { get; set; } = new()
        {
            { AsteroidType.Rocky, 1000 },
            { AsteroidType.Bronze, 500 },
        };
        public void Initialize()
        {
            if (SpaceshipHUD == null && DIContainer.Container.IsRegistered<SpaceshipHUD>())
            {
                SpaceshipHUD = DIContainer.Container.Resolve<SpaceshipHUD>();
            }
            timer ??= new Timer()
            {
                WaitTime = 1
            };
            AddChild(timer);
            ScanArea.BodyEntered += ScanArea_BodyEntered;
        }

        public override void _PhysicsProcess(double delta)
        {
            Array<Node2D> overlappingNodes = ScanArea.GetOverlappingBodies();
            bool foundAsteroid = false;
            foreach(Node2D overlappingNode in overlappingNodes)
            {
                if (overlappingNode.GetParent() is AsteroidEntity asteroid && (asteroidBeingCollected == null || asteroid == asteroidBeingCollected))
                {
                    foundAsteroid = true;
                    asteroidBeingCollected = asteroid;
                    if (collecting) break;
                    SpaceshipHUD.LabelMiddle.Text = $"Asteroid {asteroid.AsteroidType}";
                    if (Input.IsActionJustPressed("Collect"))
                    {
                        GD.Print($"Collecting {asteroid.AsteroidType} {asteroid.Name}");
                        if (CollectTypes.Contains(asteroid.AsteroidType))
                        {
                            collecting = true;
                            SpaceshipHUD.HintLabel.Text = "Collecting...";
                            timer.Start();
                              
                            timer.Timeout += () =>
                            {
                                int collectHit = CollectSpeedS.GetValueOrDefault(asteroid.AsteroidType, 500);
                                Collected?.Invoke(collectHit, asteroid.AsteroidType);
                                if (asteroidBeingCollected is IAsteroidWear asteroidWear)
                                {
                                    asteroidWear.Hit(collectHit);
                                    SpaceshipHUD.LabelRight.Text = $"Durability: {asteroidWear.Durability}";
                                }
                            };
                            if(asteroid is IAsteroidWear asteroidWear)
                            {
                                asteroidWear.AsteroidDestroyed = () =>
                                {
                                    asteroidBeingCollected = null;
                                    collecting = false;
                                };
                            }
                        }
                        else
                        {
                            SpaceshipHUD.LabelRight.Text = "Can't collect this! (Not enough power)"; 
                        }
                    }
                    else
                    {
                        SpaceshipHUD.HintLabel.Text = "Press E to collect";
                    }
                }
                else
                {
                    SpaceshipHUD.LabelMiddle.Text = "";
                }
            }

            if (!foundAsteroid) asteroidBeingCollected = null;

            if (asteroidBeingCollected == null)
            {
                collecting = false;
                SpaceshipHUD.HintLabel.Text = "";
                SpaceshipHUD.LabelRight.Text = "";
                timer.Stop();
            }
        }

        private void ScanArea_BodyEntered(Node2D body)
        {
            GD.Print($"Collector module body entered {body.Name} | {body.GetParent().Name}");
        }
    }
}
