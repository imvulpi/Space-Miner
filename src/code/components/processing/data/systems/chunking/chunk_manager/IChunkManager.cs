using Godot;

namespace SpaceMiner.src.code.components.processing.data.systems.chunking.chunk_manager
{
    public interface IChunkManager
    {
        /// <summary>
        /// Loads chunks in an implemented way.
        /// </summary>
        public void Load(Vector2 position);
        /// <summary>
        /// Unloads chunks in an implemented way.
        /// </summary>
        public void Unload(Vector2 position);
        /// <summary>
        /// Manages the loading and unloading of chunks from position in an implemented way.<br></br>Should be called periodically to function correctly.
        /// </summary>
        public void Manage(Vector2 position);
    }
}
