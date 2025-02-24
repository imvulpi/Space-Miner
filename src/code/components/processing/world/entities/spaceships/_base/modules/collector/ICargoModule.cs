using SpaceMiner.src.code.components.user.entities.asteroids;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceMiner.src.code.components.user.entities.spaceships
{
    public interface ICargoModule
    {
        public int MaxCapacity { get; set; }
        public int CurrentCapacity { get; set; }
        /// <summary>
        /// Cargo type and amount of it.
        /// </summary>
        public List<Cargo> CargoList { get; set; }
        public EventHandler CargoChanged { get; set; }

        public void AddCargo(CargoType type, int amount);
        public void RemoveCargo(CargoType type, int amount);
        public Cargo GetCargo(CargoType type);
        public int CalculateCargo();
    }
}
