using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TenCrowns.ClientCore;
using TenCrowns.GameCore;

namespace PydtOldWorld
{
    public class PydtGameFactory : GameFactory
    {
        public override ClientManager CreateClientManager(GameInterfaces gameInterfaces)
        {
            return new PydtClientManager(gameInterfaces);
        }
    }
}
