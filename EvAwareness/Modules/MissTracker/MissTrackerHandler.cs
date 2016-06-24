using System;
using EvAwareness.Utility;

namespace EvAwareness.Modules.MissTracker
{
    using Ensage;
    using Ensage.Common.Menu;

    using Utility.Console;

    class MissTrackerHandler : ModuleHandler
    {
        public override void CreateMenu()
        {
            try
            {
                var rootMenu = Variables.Menu;
                var moduleMenu = new Menu("SS Tracker", "evervolv.aware.misstracker");
                {
                    moduleMenu.AddBool("evervolv.aware.misstracker.hud", "Track in HUD", true);
                    moduleMenu.AddSlider("evervolv.aware.misstracker.mintime", "Minimum SS time", 5, 1, 10);
                    rootMenu.AddSubMenu(moduleMenu);
                }
            }
            catch (Exception e)
            {
                ConsoleHelper.Print(new ConsoleItem("MissTracker_Handler", e, MessageClass.Warning));
            }
        }

        public override ModuleType GetModuleType() => ModuleType.General;

        public override void InitEvents()
        {
            MissTrackerModule.OnLoad();
        }

        public override void OnTick() { }

        public override bool ShouldRun() => Game.IsInGame;
    }
}