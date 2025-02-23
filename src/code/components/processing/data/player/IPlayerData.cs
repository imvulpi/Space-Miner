using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceMiner.src.code.components.user.special.player
{
    public interface IPlayerData : IBalance
    {
        public string Nickname { get; set; }
        public string UUID { get; set; }
    }
}
