using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceMiner.src.code.components.user.special.player
{
    public interface IBalance
    {
        public float Balance { get; set; }
        public void AddBalance(float amount);
        public void RemoveBalance(float amount);
        public event EventHandler<float> BalanceChanged;
    }
}
