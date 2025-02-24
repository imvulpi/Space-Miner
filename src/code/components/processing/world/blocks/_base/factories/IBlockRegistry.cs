using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceMiner.src.code.components.user.blocks.core.factories
{
    public interface IBlockRegistry
    {
        /// <summary>
        /// List of all Block Factories, where the key is a factory domain string and value is specific IBlockFactory.
        /// </summary>
        Dictionary<string, IBlockFactory> BlockFactories { get; set; }
        /// <summary>
        /// Registers a factory within the ExtensibleBlockFactory<br></br>The registered factory will be called if the assigned domain is in the processed ID.
        /// </summary>
        /// <param name="domain">The unique domain of your blocks ID</param>
        /// <param name="blockFactory">Your block factory</param>
        /// <returns>Registered factory <br></br>(NOTE: If registration fails due to an already registered factory, it will still return the existing one)</returns>
        public IBlockFactory RegisterBlockFactory(string domain, IBlockFactory blockFactory);
        /// <summary>
        /// Deregisters a factory within the ExtensibleBlockFactory<br></br>
        /// The registered factory is fully deleted from the registry, including the domain
        /// </summary>
        /// <param name="domain">The unique domain of your blocks ID</param>
        /// <returns>Whether the deregistration was succesful</returns>
        public bool DeregisterBlockFactory(string domain);
        /// <summary>
        /// Gets a factory registered at the provided domain
        /// </summary>
        /// <param name="domain">The factory domain</param>
        /// <returns>The registered factory or null if no factory is registered at the provided domain</returns>
        public IBlockFactory GetBlockFactory(string domain);
        /// <summary>
        /// Overrides a factory set at a specific domain with the provied new factory.
        /// </summary>
        /// <param name="domain">The unique factory block ID domain</param>
        /// <param name="blockFactory">Your block factory</param>
        public void SetBlockFactory(string domain, IBlockFactory blockFactory);
    }
}
