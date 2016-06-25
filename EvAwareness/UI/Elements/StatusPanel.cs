namespace EvAwareness.UI.Elements
{
    using System;
    using System.Linq;
    using System.Runtime.Remoting.Messaging;

    using Ensage;
    using Ensage.Common;
    using Ensage.Common.Menu;

    using Modules.MissTracker;

    using SharpDX;

    using Utility;
    using Utility.Console;

    class StatusPanel : ElementHandler
    {
        public override void OnLoad()
        {
            try
            {
                var rootMenu = HudVariables.HudMenu;
                var moduleMenu = new Menu("Status panel", "evervolv.aware.hud.statuspanel");
                {
                    moduleMenu.AddBool("evervolv.aware.hud.statuspanel.draw", "Draw on status panel", true);
                    rootMenu.AddSubMenu(moduleMenu);
                }
            }
            catch (Exception e)
            {
                ConsoleHelper.Print(new ConsoleItem("StatusPanel", e, MessageClass.Warning));
            }
        }

        public override bool ShouldDraw() => HudVariables.ShouldBeVisible && 
                                             MenuExtensions.GetItemValue<bool>("evervolv.aware.hud.statuspanel.draw");

        public override void OnDraw()
        {
            // Development hud
            if (Variables.IsDevelopment)
            {
                Drawing.DrawText("Heroes: ", new Vector2(100, 100), Color.Aqua, FontFlags.Outline);

                var i = 0;

                foreach (var tracker in MissTrackerModule.Trackers.Values)
                {
                    i++;
                    Drawing.DrawText(
                        tracker.Hero.Name + " | " + tracker.SSTime + " | " + tracker.LastPosition,
                        new Vector2(100, 100 + 18 * i),
                        Color.Black,
                        FontFlags.Outline);
                }

                i = 0;
            }

            // Misstracker top panel hud
            if (MenuExtensions.GetItemValue<bool>("evervolv.aware.misstracker.hud"))
            {
                foreach (var tracker in MissTrackerModule.Trackers.Values)
                {
                    var hudInfo = HudHelper.GetTopPanelPosition(tracker.Hero);
                    var hudInfoSize = HudHelper.GetTopPanelSize(tracker.Hero);

                    var drawColor = tracker.GetColor();
                    drawColor.A = 200;

                    Drawing.DrawRect(hudInfo, hudInfoSize, drawColor);
                    Drawing.DrawRect(hudInfo, hudInfoSize, Color.Black, true);
                    var timePosition = new Vector2(hudInfo.X + 5, hudInfo.Y + 10);

                    Drawing.DrawText(tracker.SSTime, timePosition, new Vector2(14), Color.Black, FontFlags.AntiAlias);

                    var statusPosition = new Vector2(hudInfo.X + 5, hudInfo.Y + 20);

                    Drawing.DrawText(
                        tracker.Status.ToString(),
                        statusPosition,
                        new Vector2(14),
                        Color.Black,
                        FontFlags.AntiAlias);
                }
            }
        }
    }
}