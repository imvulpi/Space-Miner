using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceMiner.src.code.components.processing.entities.implementations.player.spaceship
{
    public interface ISpaceshipController<T> where T : class
    {
        public void Move(T entity, double delta);
        public void Rotate(T entity, double delta);
        public void Addtional(T entity, double delta);
    }
}
