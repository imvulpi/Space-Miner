using Godot;
using SpaceMiner.src.code.components.user.entities.entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceMiner.src.code.components.user.entities.asteroids
{
    public class AsteroidCreator
    {
        public AsteroidEntity Create(AsteroidEntity entity, AsteroidType asteroidType)
        {
            SetAsteroidSprite(ref entity, asteroidType);
            SetAsteroidBody(ref entity, asteroidType);
            return entity;
        }
        
        public void SetAsteroidSprite(ref AsteroidEntity entity, AsteroidType asteroidType)
        {
            switch (asteroidType)
            {
                case AsteroidType.Rocky:
                    entity.Sprite.Texture = ResourceLoader.Load<Texture2D>("rocky_asteroid.png");
                    break;
                case AsteroidType.Bronze:
                    entity.Sprite.Texture = ResourceLoader.Load<Texture2D>("bronze_asteroid.png");
                    break;
                case AsteroidType.Silver:
                    entity.Sprite.Texture = ResourceLoader.Load<Texture2D>("silver_asteroid.png");
                    break;
                case AsteroidType.Gold:
                    entity.Sprite.Texture = ResourceLoader.Load<Texture2D>("gold_asteroid.png");
                    break;
            }
        }

        public void SetAsteroidBody(ref AsteroidEntity entity, AsteroidType asteroidType)
        {
            switch (asteroidType)
            {
                case AsteroidType.Rocky:
                    entity.RigidBody.Mass = 30;
                    break;
                case AsteroidType.Bronze:
                    entity.RigidBody.Mass = 70;
                    break;
                case AsteroidType.Silver:
                    entity.RigidBody.Mass = 50;
                    break;
                case AsteroidType.Gold:
                    entity.RigidBody.Mass = 100;
                    break;
            }
        }
    }

    public enum AsteroidType
    {
        Rocky,
        Bronze,
        Silver,
        Gold,
    }
}
