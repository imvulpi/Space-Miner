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
            position.X = MathF.Round(position.X / ChunkConstants.CHUNK_SIZE);
            position.Y = MathF.Round(position.Y / ChunkConstants.CHUNK_SIZE);
            return position;
        }

        /// <summary>
        /// Returns chunk position of the provided vector.<br></br>This method changes the provided vector, which makes it more performant, but changes values
        /// </summary>
        /// <param name="position">Normal px position</param>
        public static void ConvertToChunkVector(ref Vector2 position)
        {
            position.X = MathF.Round(position.X / ChunkConstants.CHUNK_SIZE);
            position.Y = MathF.Round(position.Y / ChunkConstants.CHUNK_SIZE);
        }

        public static string GetChunkFilename(Vector2 position)
        {
            return $"({position.X}{position.Y})";
        }
    }
}
