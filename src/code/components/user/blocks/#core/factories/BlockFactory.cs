using SpaceMiner.src.code.components.commons.errors;
using SpaceMiner.src.code.components.commons.errors.exceptions;
using SpaceMiner.src.code.components.user.blocks.core.constants;
using System.Collections.Generic;

namespace SpaceMiner.src.code.components.user.blocks.core.factories
{
    public class BlockFactory : IBlockFactory
    {
        public BlockFactory() { 
            BlockFactories = new Dictionary<string, IBlockFactory>();
        }
        public Dictionary<string, IBlockFactory> BlockFactories { get; set; }
        public virtual Block GetBlock(string id, Block blocksData)
        {
            string[] idParts = id.Split(BlocksConstants.ID_FACTORY_DELIMITER);
            string key = idParts[0];
            return GetFactoryBlock(key, idParts, 1, blocksData);
        }

        public virtual Block GetBlock(string[] idParts, int startAt, Block blocksData)
        {
            string key = idParts[startAt];
            return GetFactoryBlock(key, idParts, startAt+1, blocksData);
        }

        private Block GetFactoryBlock(string key, string[] idParts, int startAt, Block blocksData)
        {
            if (BlockFactories.TryGetValue(key, out IBlockFactory factory))
            {
                return factory.GetBlock(idParts, startAt, blocksData);
            }
            else
            {
                throw new GameException(PrettyErrorType.ResourceNotFound, "Missing Block Factory", $"No block factory found for domain: {key}");
            }
        }

        public IBlockFactory RegisterBlockFactory(string domain, IBlockFactory blockFactory)
        {
            if (BlockFactories.ContainsKey(domain))
            {
                return BlockFactories[domain];
            }

            BlockFactories.Add(domain, blockFactory);
            return blockFactory;
        }

        public bool DeregisterBlockFactory(string domain)
        {
            BlockFactories.Remove(domain);
            return true;
        }

        public IBlockFactory GetBlockFactory(string domain)
        {
            if (BlockFactories.TryGetValue(domain, out IBlockFactory factory))
            {
                return factory;
            }
            else
            {
                return null;
            }
        }

        public void SetBlockFactory(string domain, IBlockFactory blockFactory)
        {
            if (BlockFactories.ContainsKey(domain))
            {
                BlockFactories[domain] = blockFactory;
            }
            else
            {
                BlockFactories.Add(domain, blockFactory);
            }
        }
    }
}
