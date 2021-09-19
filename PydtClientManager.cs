using Mohawk.SystemCore;
using Mohawk.UIInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TenCrowns.ClientCore;
using TenCrowns.GameCore;

namespace PydtOldWorld
{
    public class PydtClientManager : ClientManager
    {
        private bool forceSaveExit = false;

        public PydtClientManager(ModSettings modSettings, Game gameClient, GameInterfaces gameInterfaces, IClientNetwork clientNetwork) : base(modSettings, gameClient, gameInterfaces, clientNetwork)
        {
        }

        public override void startGame()
        {
            forceSaveExit = false;
            base.startGame();
        }

        public override void blockScreenForHotseat()
        {
            // First time through it's the current player's turn
            if (forceSaveExit)
            {
                Player pActivePlayer = activePlayer();
                if (pActivePlayer != null)
                {
                    Interfaces.UserInterface.SetUIAttribute("HotseatPopup-IsActive", true.ToStringCached());
                    Interfaces.UserInterface.CreatePopup("Turn Complete", "Click OK To Save and Exit to Menu", new List<PopupOption>() { new PopupOption("OK") }, true,
                        PopupOverlayType.DARK, -1, "POPUP_DEFAULT", (bool result) =>
                        {
                            App.SaveGame(new SaveParameters { path = $"{App.MPSavesFolder}/PYDT_{DateTime.Now.ToString("yyyyMMdd_HHmmss")}.zip" }, GameClient, (success) => App.ExitToMenu());
                        });
                }
            } else
            {
                forceSaveExit = true;
                base.blockScreenForHotseat();
            }
        }
    }
}
