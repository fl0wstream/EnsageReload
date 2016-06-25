using System;
using EvAwareness.Utility;

namespace EvAwareness.Modules.GankAlert
{
    using Ensage.Common.Menu;
    using Ensage.Common.Menu.MenuItems;

    using Utility.Console;

    class GankAlertHandler : ModuleHandler
    {
        public override void CreateMenu()
        {
            try
            {
                var RootMenu = Variables.Menu;
                var moduleMenu = new Menu("Gank Alerter", "evervolv.aware.gank");
                {
                    moduleMenu.AddBool("evervolv.aware.gank.enable", "Gank Alerter", true);
                    moduleMenu.AddSlider("evervolv.aware.gank.textsize", "Text Size", 2, 2, 80).SetTooltip("The text size of the Gank alert text");
                    moduleMenu.AddSlider("evervolv.aware.gank.mindist", "Min. Distance", 1100, 300, 1800).SetTooltip("The minimum detection distance");
                    moduleMenu.AddSlider("evervolv.aware.gank.maxdist", "Max. Distance", 3500, 500, 6000).SetTooltip("The maximum detection distance");

                    moduleMenu.AddItem(
                        new EnemyHeroesToggler(
                            "evervolv.aware.gank.whitelist",
                            "Work on",
                            GankAlertVariables.Whitelist));

                    moduleMenu.AddBool("evervolv.aware.gank.drawline", "Draw line to ganking hero", true);
                    moduleMenu.AddBool(
                        "evervolv.aware.gank.drawline.minimap",
                        "Draw line to ganking hero on minimap",
                        true);

                    RootMenu.AddSubMenu(moduleMenu);
                }
            }
            catch (Exception e)
            {
                ConsoleHelper.Print(new ConsoleItem("GankAlerterHandler::CreateMenu", e));
            }
        }

        public override ModuleType GetModuleType() => ModuleType.OnUpdate;

        public override bool ShouldRun() => MenuExtensions.GetItemValue<bool>("evervolv.aware.gank.enable");

        public override void InitEvents()
        {
            try
            {
                GankAlertCalculator.OnLoad();
            }
            catch (Exception e)
            {
                ConsoleHelper.Print(new ConsoleItem("GankAlerterHandler::InitEvents", e));
            }
        }

        public override void OnTick()
        {
            GankAlertCalculator.GetGankingHero();
        }
    }
}