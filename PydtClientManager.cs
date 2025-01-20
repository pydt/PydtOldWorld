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

        public PydtClientManager(GameInterfaces gameInterfaces) : base(gameInterfaces)
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
                    Interfaces.Application.UserInterface.SetUIAttribute("HotseatPopup-IsActive", true.ToStringCached());
                    Interfaces.Application.UserInterface.CreatePopup("Turn Complete", "Click OK To Save and Exit to Menu", new List<PopupOption>() { new PopupOption("OK") }, true,
                        PopupOverlayType.DARK, -1, "POPUP_DEFAULT", (bool result) =>
                        {
                            App.SaveGame(new SaveParameters { path = $"{App.MPSavesFolder}/PYDT_{DateTime.Now.ToString("yyyyMMdd_HHmmss")}.zip" }, GameClient, GameClient.getCurrentTurnPlayer(), (success) => App.ExitToMenu());
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
