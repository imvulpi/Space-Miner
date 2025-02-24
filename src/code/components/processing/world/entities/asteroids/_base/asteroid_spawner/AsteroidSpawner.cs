using Godot;
using SpaceMiner.src.code.components.processing.data.game.chunks.chunk;
using SpaceMiner.src.code.components.processing.data.systems.chunking;
using SpaceMiner.src.code.components.processing.data.systems.chunking.chunks;
using SpaceMiner.src.code.components.user.entities.entities;
using SpaceMiner.src.code.components.user.entities.entities.implementations.asteroid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SpaceMiner.src.code.components.user.entities.asteroids
{
    public class AsteroidSpawner : IAsteroidSpawner
    {
        public AsteroidSpawner(Node2D playerNode, Node connectNode)
        {
            PlayerNode = playerNode;
            ConnectNode = connectNode;
        }

        public Node2D PlayerNode { get; set; }
        public Node ConnectNode { get; set; }
        public int MaxAsteroids { get; set; } = 128;
        public int SafeRadiusBlocks { get; set; } = 16;
        public int MaxSpawnRadiusBlocks { get; set; } = 64;
        public int DespawnRadiusBlocks { get; set; } = 96;
        public Action<List<ChunkNode>> GetChunks;
        private Godot.Timer godotSpawnTimer;
        private Random random = new Random();
        private Node asteroidHolder;
        private List<AsteroidEntity> Asteroids = new();
        public void Initialize()
        {
            godotSpawnTimer ??= new()
            {
                Name = "Asteroid Timer",
                WaitTime = 0.1,
            };
            ConnectNode.AddChild(godotSpawnTimer);
            godotSpawnTimer.Start();

            godotSpawnTimer.Timeout += () =>
            {
                Despawn();
                if (Asteroids.Count < MaxAsteroids)
                {
                    AsteroidEntity asteroidEntity = new();
                    int shouldSpawn = random.Next(1, 3);
                    if (shouldSpawn == 1)
                    {
                        int randomNumber = random.Next(1, 10001);
                        float randomPercentage = (float)randomNumber / 10000;
                        AsteroidEntity asteroid = null;
                        if (randomPercentage >= 0.0001 && randomPercentage <= 0.01) // 1%
                        {
                            asteroid = new GoldAsteroid().CreateScene();
                        }
                        else if (randomPercentage > 0.01 && randomPercentage <= 0.101) // 10%
                        {
                            asteroid = new SilverAsteroid().CreateScene();
                        }
                        else if (randomPercentage > 0.101 && randomPercentage <= 0.301) // 20%
                        {
                            asteroid = new BronzeAsteroid().CreateScene();
                        }
                        else if (randomPercentage > 0.301 && randomPercentage <= 1) // 69%
                        {
                            asteroid = new RockyAsteroid().CreateScene();
                        }
                        AddAsteroid(asteroid);
                    }
                }
            };
        }

        private void Despawn()
        {
            Vector2 safeRadiusMin = new Vector2(PlayerNode.Position.X - SafeRadiusBlocks * ChunkConstants.BLOCK_SIZE,
                       PlayerNode.Position.Y - SafeRadiusBlocks * ChunkConstants.BLOCK_SIZE);
            Vector2 safeRadiusMax = new Vector2(PlayerNode.Position.X + SafeRadiusBlocks * ChunkConstants.BLOCK_SIZE,
                       PlayerNode.Position.Y + SafeRadiusBlocks * ChunkConstants.BLOCK_SIZE);

            Vector2 min = new Vector2(safeRadiusMin.X - DespawnRadiusBlocks * ChunkConstants.BLOCK_SIZE,
                          safeRadiusMin.Y - DespawnRadiusBlocks * ChunkConstants.BLOCK_SIZE);

            Vector2 max = new Vector2(safeRadiusMax.X + DespawnRadiusBlocks * ChunkConstants.BLOCK_SIZE,
                          safeRadiusMax.Y + DespawnRadiusBlocks * ChunkConstants.BLOCK_SIZE);
            for (int i = 0; i < Asteroids.Count; i++)
            {
                AsteroidEntity asteroid = Asteroids[i];
                if (!GodotObject.IsInstanceValid(asteroid))
                {
                    Asteroids.Remove(asteroid);
                    i--;
                    continue;
                }
                
                if (asteroid.Position.X > max.X || asteroid.Position.Y > max.Y || asteroid.Position.X < min.X || asteroid.Position.Y < min.Y)
                {
                    asteroidHolder.RemoveChild(asteroid);
                    Asteroids.Remove(asteroid);
                    i--;
                }
            }
        }

        private void AddAsteroid(AsteroidEntity asteroidEntity)
        {
            if(asteroidHolder == null)
            {
                asteroidHolder = new();
                asteroidHolder.Name = "Asteroid Holder";
                ConnectNode.AddChild(asteroidHolder);
            }

            Vector2 safeRadiusMin = new Vector2(PlayerNode.Position.X - SafeRadiusBlocks * ChunkConstants.BLOCK_SIZE,
                       PlayerNode.Position.Y - SafeRadiusBlocks * ChunkConstants.BLOCK_SIZE);
            Vector2 safeRadiusMax = new Vector2(PlayerNode.Position.X + SafeRadiusBlocks * ChunkConstants.BLOCK_SIZE,
                       PlayerNode.Position.Y + SafeRadiusBlocks * ChunkConstants.BLOCK_SIZE);

            Vector2 min = new Vector2(safeRadiusMin.X - MaxSpawnRadiusBlocks * ChunkConstants.BLOCK_SIZE,
                          safeRadiusMin.Y - MaxSpawnRadiusBlocks * ChunkConstants.BLOCK_SIZE);

            Vector2 max = new Vector2(safeRadiusMax.X + MaxSpawnRadiusBlocks * ChunkConstants.BLOCK_SIZE,
                          safeRadiusMax.Y + MaxSpawnRadiusBlocks * ChunkConstants.BLOCK_SIZE);

            double randomX = random.NextDouble() * (max.X - min.X) + min.X;
            double randomY = random.NextDouble() * (max.Y - min.Y) + min.Y;

            asteroidEntity.Position = new Vector2((float)randomX, (float)randomY);
            asteroidHolder.AddChild(asteroidEntity);
            Asteroids.Add(asteroidEntity);
        }
    }
}
