using System;
using EvAwareness.Utility;

namespace EvAwareness.Modules.MissTracker
{
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
                    moduleMenu.AddItem(new MenuItem("evervolv.aware.misstracker.hud", "Track in HUD").SetValue(true));
                    rootMenu.AddSubMenu(moduleMenu);
                }
            }
            catch (Exception e)
            {
                ConsoleHelper.Print(new ConsoleItem("MissTracker_Handler", e, MessageType.Warning));
            }
        }

        public override ModuleType GetModuleType()
        {
            throw new NotImplementedException();
        }

        public override void InitEvents()
        {
            throw new NotImplementedException();
        }

        public override void OnTick()
        {
            throw new NotImplementedException();
        }

        public override bool ShouldRun()
        {
            throw new NotImplementedException();
        }
    }
}