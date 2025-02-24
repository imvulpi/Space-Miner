using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace SpaceMiner.src.code.components.processing.data.game.spaceships
{
    public interface ISpaceship : ISpaceshipData
    {
        public void Init();
        public void Update(double delta);
        public void UpdateParameters();
    }
}
