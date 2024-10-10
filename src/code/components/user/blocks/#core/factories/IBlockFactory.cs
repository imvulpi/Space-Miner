namespace SpaceMiner.src.code.components.user.blocks.core.factories
{
    public interface IBlockFactory : IBlockRegistry
    {
        /// <summary>
        /// Creates a block using block factories specified implementations
        /// </summary>
        /// <param name="id">Block ID</param>
        /// <param name="blocksData">Data of the block.<br></br>NOTE: The block specific class and data is usually hidden.</param>
        /// <returns>Block object coupled with all data</returns>
        public Block GetBlock(string id, Block blocksData);

        /// <summary>
        /// Creates a block using block factories specified implementations<br></br>
        /// This method avoids copying/cloning strings by requiring an integer which should skips already processed parts<br></br>
        /// Should be called inside other block factories to avoid overhead from splitting, and copying new arrays.
        /// </summary>
        /// <param name="idParts">Parts of ID (full ID)</param>
        /// <param name="startAt">What part it should start at.</param>
        /// <param name="blocksData">Data of the block.<br></br>NOTE: The block specific class and data is usually hidden.</param>
        /// <returns>Block object coupled with all data</returns>
        public Block GetBlock(string[] idParts, int startAt, Block blocksData);
    }
}
