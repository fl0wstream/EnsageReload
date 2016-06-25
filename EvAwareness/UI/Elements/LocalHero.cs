using System;

namespace EvAwareness.UI.Elements
{
    using System.Linq;

    using Ensage;
    using Ensage.Common;
    using Ensage.Common.Menu;

    using global::EvAwareness.Modules.GankAlert;

    using SharpDX;

    using Utility.Console;
    using Utility;

    class LocalHero : ElementHandler
    {
        public static Vector2 HeroScreenPosition { get; private set; }

        public static Vector2 HeroScreenSize { get; private set; }

        public override void OnLoad()
        {
            try
            {
                var rootMenu = HudVariables.HudMenu;
                var moduleMenu = new Menu("Hero", "evervolv.aware.hud.hero");
                {
                    moduleMenu.AddBool("evervolv.aware.hud.hero.draw", "Draw on hero", true);
                    rootMenu.AddSubMenu(moduleMenu);
                }
            }
            catch (Exception e)
            {
                ConsoleHelper.Print(new ConsoleItem("StatusPanel", e, MessageClass.Warning));
            }
        }

        public override bool ShouldDraw() =>    Variables.Player.IsValid &&
                                                Variables.Player.IsAlive && 
                                                HudVariables.ShouldBeVisible &&
                                                MenuExtensions.GetItemValue<bool>("evervolv.aware.hud.hero.draw");

        public override void OnDraw()
        {
            HeroScreenPosition = HUDInfo.GetHPbarPosition(Variables.Player) + new Vector2(-HUDInfo.GetHPBarSizeX(Variables.Player) / 2, -HUDInfo.GetHpBarSizeY(Variables.Player) * 2.5f);
            HeroScreenSize = new Vector2(HUDInfo.GetHPBarSizeX(Variables.Player), HUDInfo.GetHpBarSizeY(Variables.Player) / 2) * 2;

            // Gank drawing
            if (MenuExtensions.GetItemValue<bool>("evervolv.aware.gank.enable"))
            {
                if (GankAlertCalculator.GetGankingHero() != null)
                {
                    var gankingHero = GankAlertCalculator.GetGankingHero();
                    var userSize = MenuExtensions.GetItemValue<Slider>("evervolv.aware.gank.textsize").Value / 4;

                    // Line drawing
                    if (MenuExtensions.GetItemValue<bool>("evervolv.aware.gank.drawline"))
                    {
                        var gankingHeroLine = HUDInfo.GetHPbarPosition(gankingHero)
                                              + new Vector2(
                                                    +HUDInfo.GetHPBarSizeX(gankingHero) / 2,
                                                    +HUDInfo.GetHpBarSizeY(gankingHero) * 5);
                        var localHeroLine = HUDInfo.GetHPbarPosition(Variables.Player)
                                            + new Vector2(
                                                  +HUDInfo.GetHPBarSizeX(Variables.Player) / 2,
                                                  +HUDInfo.GetHpBarSizeY(Variables.Player) * 5);

                        Drawing.DrawLine(localHeroLine, gankingHeroLine, Color.Cyan);
                    }

                    // Text drawing
                    var text = CommonHelper.GetHeroName(gankingHero) + " is ganking!";
                    var flags = FontFlags.None;
                    Drawing.DrawText(text, HudHelper.GetTextPosition(HeroScreenPosition, text, HeroScreenSize + new Vector2(userSize), flags),
                        new Vector2(HeroScreenSize.Y * 2, HeroScreenSize.X) + new Vector2(userSize * 2, userSize), Color.White, flags);
                }
            }
        }
    }
}