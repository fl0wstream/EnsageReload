using System;

namespace EvAwareness.UI.Elements
{
    using System.Linq;

    using Ensage;
    using Ensage.Common.Menu;

    using global::EvAwareness.Modules.MissTracker;

    using SharpDX;

    using Utility.Console;
    using Utility;

    class Minimap : ElementHandler
    {
        public override void OnLoad()
        {
            try
            {
                var rootMenu = HudVariables.HudMenu;
                var moduleMenu = new Menu("Minimap", "evervolv.aware.hud.minimap");
                {
                    moduleMenu.AddBool("evervolv.aware.hud.minimap.draw", "Draw on minimap", true);
                    rootMenu.AddSubMenu(moduleMenu);
                }
            }
            catch (Exception e)
            {
                ConsoleHelper.Print(new ConsoleItem("StatusPanel", e, MessageClass.Warning));
            }
        }

        public override void OnDraw()
        {
            if (MenuExtensions.GetItemValue<bool>("evervolv.aware.misstracker.minimap"))
            {
                foreach (var tracker in MissTrackerModule.Trackers.Values.Where(x =>
                    x.SSTimeInt >= MenuExtensions.GetItemValue<Slider>("evervolv.aware.misstracker.mintime").Value &&
                    x.Hero.IsAlive &&
                    x.Status != TrackStatus.Visible))
                {
                    var drawPosition = CommonHelper.WorldToMinimap(tracker.LastPosition);
                    var size = new Vector2(MenuExtensions.GetItemValue<Slider>("evervolv.aware.misstracker.minimap.size").Value);
                    Drawing.DrawRect(drawPosition + new Vector2(-size.X / 2, -size.Y / 2), size, HudHelper.GetHeroTextureMinimap(tracker.Hero.Name));

                    /**if (MenuExtensions.GetItemValue<bool>("evervolv.aware.misstracker.minimap.sstime"))
                    {
                        var textSize = new Vector2(MenuExtensions.GetItemValue<Slider>("evervolv.aware.misstracker.minimap.size").Value - 2);
                        var textColor = Color.White; textColor.A = 100;
                        Drawing.DrawText(tracker.SSTimeInt.ToString(), drawPosition + new Vector2(-textSize.X / 2, -textSize.Y / 2), textSize, textColor, FontFlags.AntiAlias);
                    }*/
                }
            }
        }

        public override bool ShouldDraw() => HudVariables.ShouldBeVisible &&
                                             MenuExtensions.GetItemValue<bool>("evervolv.aware.hud.minimap.draw");
    }
}