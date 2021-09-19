using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TenCrowns.AppCore;
using TenCrowns.GameCore;

namespace PydtOldWorld
{
    public class PydtOldWorldMod : ModEntryPointAdapter
    {
        public override void Initialize(ModSettings modSettings)
        {
            base.Initialize(modSettings);
            modSettings.Factory = new PydtGameFactory();
        }
    }
}
