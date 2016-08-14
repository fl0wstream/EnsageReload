using System;
using EvAwareness.Utility;

namespace EvAwareness.Modules.TFHelper
{
    using Ensage.Common.Menu;

    using Utility.Console;

    class TFHelperHandler : ModuleHandler
    {
        public override ModuleType GetModuleType() => ModuleType.General;

        public override bool ShouldRun() => false;

        public override void CreateMenu()
        {
            try
            {
                var RootMenu = Variables.Menu;
                var moduleMenu = new Menu("TF Helper", "evervolv.aware.tf");
                {
                    moduleMenu.AddBool("evervolv.aware.tf.enabled", "TF Helper");
                    moduleMenu.AddSlider("evervolv.aware.tf.range", "TF Range", 1200, 500, 1800);
                    moduleMenu.AddBool("evervolv.aware.tf.draw.mouse", "Draw on mouse");
                    moduleMenu.AddSlider("evervolv.aware.tf.mousetextsize", "Mouse text size", 14, 10, 30);
                    moduleMenu.AddBool("evervolv.aware.tf.draw.hero", "Draw on hero");
                    moduleMenu.AddSlider("evervolv.aware.tf.herotextsize", "Hero text size", 14, 10, 30);
                    RootMenu.AddSubMenu(moduleMenu);
                }

            }
            catch (Exception e)
            {
                ConsoleHelper.Print(new ConsoleItem("TFHelperHandler::CreateMenu", e));
            }
        }

        public override void InitEvents()
        {
            TFHelperDrawings.OnLoad();
        }

        public override void OnTick() { }
    }
}