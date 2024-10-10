using Godot;
using SpaceMiner.src.code.components.processing.data.systems.chunking.chunks;
using System;
namespace SpaceMiner.src.code.components.processing.data.systems.chunking
{
    public static class ChunkHelper
    {
        /// <summary>
        /// Returns chunk position of the provided vector.
        /// </summary>
        /// <param name="position">Normal px position</param>
        public static Vector2 ConvertToChunkVector(Vector2 position)
        {
            position.X = MathF.Floor(position.X / ChunkConstants.CHUNK_SIZE);
            position.Y = MathF.Floor(position.Y / ChunkConstants.CHUNK_SIZE);
            return position;
        }

        /// <summary>
        /// Returns chunk position of the provided vector.<br></br>This method changes the provided vector, which makes it more performant, but changes values
        /// </summary>
        /// <param name="position">Normal px position</param>
        public static void ConvertToChunkVector(ref Vector2 position)
        {
            position.X = MathF.Floor(position.X / ChunkConstants.CHUNK_SIZE);
            position.Y = MathF.Floor(position.Y / ChunkConstants.CHUNK_SIZE);
        }

        /// <summary>
        /// Calculates a position relative to a chunk position
        /// </summary>
        /// <param name="position">normal px position</param>
        /// <returns>position relative to a chunk position</returns>
        public static Vector2 CalculateRelativePosition(Vector2 position)
        {
            Vector2 chunkPosition = ConvertToChunkVector(position);
            position.X -= chunkPosition.X * ChunkConstants.CHUNK_SIZE;
            position.Y -= chunkPosition.Y * ChunkConstants.CHUNK_SIZE;
            return position;
        }

        public static string GetChunkFilename(Vector2 position)
        {
            return $"({position.X}.{position.Y})";
        }
    }
}
