using Godot;
using ProtoBuf;
using SpaceMiner.src.code.components.commons.errors.exceptions;
using SpaceMiner.src.code.components.user.entities.asteroids;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceMiner.src.code.components.user.entities.spaceships
{
    [ProtoContract]
    public class CargoModule : ICargoModule
    {
        /// <summary>
        /// Max WEIGHT capacity
        /// </summary>
        public int MaxCapacity { get; set; }
        /// <summary>
        /// Current WEIGHT capacity
        /// </summary>

        [ProtoMember(1)]
        public int CurrentCapacity { get; set; } = 0;
        [ProtoMember(2)]
        public List<Cargo> CargoList { get; set; } = new List<Cargo>();
        public EventHandler CargoChanged { get; set; }

        public int CalculateCargo()
        {
            int totalCargo = 0;
            foreach(Cargo cargo in CargoList)
            {
                int cargoTypeWeight = GetCargoWeight(cargo.CargoType);
                int totalWeight = cargo.Amount * cargoTypeWeight;
                totalCargo += totalWeight;
            }

            if(totalCargo != CurrentCapacity)
            {
                GD.Print("Something went wrong in previous cargo weight calculations");
            }
            return totalCargo;
        }

        public void AddCargo(CargoType type, int amount)
        {
            int cargoTypeWeight = GetCargoWeight(type);
            int totalCargoWeight = cargoTypeWeight * amount;
            if (totalCargoWeight + CurrentCapacity > MaxCapacity)
            {
                int overflowedCapacity = totalCargoWeight + CurrentCapacity;
                int spaceLeft = MaxCapacity - CurrentCapacity;
                if(spaceLeft > cargoTypeWeight)
                {
                    int unitsThatWillFit = (int)Mathf.Floor(spaceLeft/cargoTypeWeight);
                    AddCargoDirect(type, unitsThatWillFit, unitsThatWillFit * cargoTypeWeight);
                    // Return not enough space
                }
            }
            else
            {
                AddCargoDirect(type, amount, totalCargoWeight);
            }
        }
        public void RemoveCargo(CargoType type, int amount)
        {
            Cargo cargo = CargoList.Where((cargo) => cargo.CargoType == type).FirstOrDefault();
            if (cargo == null) return;
            if(amount > cargo.Amount)
            {
                cargo.Amount = 0;
                CurrentCapacity -= cargo.Weight;
                cargo.Weight = 0;
                // Return not enough cargo?
            }
            else
            {
                cargo.Amount -= amount;
                int cargoTypeWeight = GetCargoWeight(type);
                int weightLoss = amount * cargoTypeWeight;
                CurrentCapacity -= weightLoss;
                cargo.Weight -= weightLoss;
            }
            CargoChanged?.Invoke(this, EventArgs.Empty);
        }

        public Cargo GetCargo(CargoType type)
        {
            Cargo cargo = CargoList.Where((cargo) => cargo.CargoType == type).FirstOrDefault();
            return cargo;
        }

        private void AddCargoDirect(CargoType type, int amount, int totalCargoWeight)
        {
            Cargo cargo = CargoList.Where((cargo) => cargo.CargoType == type).FirstOrDefault();
            if (cargo == null)
            {
                Cargo newCargo = new Cargo()
                {
                    CargoType = type,
                    Amount = amount,
                    Weight = totalCargoWeight
                };
                CargoList.Add(newCargo);
                CurrentCapacity += totalCargoWeight;
            }
            else
            {
                cargo.Amount += amount;
                cargo.Weight += totalCargoWeight;
                CurrentCapacity += totalCargoWeight;
            }
            CargoChanged?.Invoke(this, EventArgs.Empty);
        }

        public int GetCargoWeight(CargoType cargoType)
        {
            return cargoType switch
            {
                CargoType.Rock => 2,
                CargoType.Bronze => 8,
                CargoType.Silver => 10,
                CargoType.Gold => 20,
                _ => throw new GameException(commons.errors.PrettyErrorType.Invalid, "Asteroid type not implemented", "This asteroid type was not implemented in conversion to cargo"),
            };
        }

        public CargoType AsteroidToCargo(AsteroidType asteroidType)
        {
            return asteroidType switch
            {
                AsteroidType.Rocky => CargoType.Rock,
                AsteroidType.Bronze => CargoType.Bronze,
                AsteroidType.Silver => CargoType.Silver,
                AsteroidType.Gold => CargoType.Gold,
                _ => throw new GameException(commons.errors.PrettyErrorType.Invalid, "Asteroid type not implemented", "This asteroid type was not implemented in conversion to cargo"),
            };
        }
    }
}
