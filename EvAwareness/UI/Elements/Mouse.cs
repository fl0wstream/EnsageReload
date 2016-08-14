namespace EvAwareness.UI.Elements
{
    using System;
    using System.Linq;
    using System.Runtime.Remoting.Messaging;

    using Ensage;
    using Ensage.Common;
    using Ensage.Common.Menu;

    using Modules.TFHelper;

    using SharpDX;

    using Utility;
    using Utility.Console;

    class Mouse : ElementHandler
    {
        public override void OnLoad()
        {
            try
            {
                var rootMenu = HudVariables.HudMenu;
                var moduleMenu = new Menu("Mouse", "evervolv.aware.hud.mouse");
                {
                    moduleMenu.AddBool("evervolv.aware.hud.mouse.draw", "Draw on mouse", true);
                    rootMenu.AddSubMenu(moduleMenu);
                }
            }
            catch (Exception e)
            {
                ConsoleHelper.Print(new ConsoleItem("Mouse", e, MessageClass.Warning));
            }
        }

        public override bool ShouldDraw() => HudVariables.ShouldBeVisible &&
                                             MenuExtensions.GetItemValue<bool>("evervolv.aware.hud.mouse.draw");

        public override void OnDraw()
        {
            if (TFHelperVariables.Enabled)
                TFHelperDrawings.DrawOnMouse();
        }
    }
}