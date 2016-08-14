namespace EvAwareness.Modules.TFHelper
{
    using System;

    using Ensage;
    using Ensage.Common;

    using SharpDX;

    using UI;
    using Utility;

    class TFHelperDrawings
    {
        private static double _allyStrength = -1;
        private static double _enemyStrength = -1;
        private static string _fightResult = "uncached";

        public static void OnLoad()
        {
            Game.OnIngameUpdate += Game_OnIngameUpdate;
        }

        private static void Game_OnIngameUpdate(EventArgs args)
        {
            // Щас бы костылями фиксить АахааххахаХ ХАхАХХАхАХХАхАХХАхАХАЪХ 
            // я ебанулся (((нет(да))(нет))(да) * 0)
            if (Utils.SleepCheck("TFHelper_Optimization"))
            {
                _allyStrength = Math.Round(TFHelperCalculator.GetAllyStrength() * 100);
                _enemyStrength = Math.Round(TFHelperCalculator.GetEnemyStrength() * 100);
                _fightResult = TFHelperCalculator.GetText();
                Utils.Sleep(500, "TFHelper_Optimization");
            }
        }

        public static void DrawOnMouse()
        {
            if (!TFHelperVariables.DrawOnMouse || Uncached || !TFHelperVariables.Enabled) return;

            // Я хотел тут что-то дописать, но забыл что забыл что надо было купить сигареты, и тут вспомнил АХАХАХАХА
            var textSize = TFHelperVariables.MouseTextSize;

            var mousePosition = Game.MouseScreenPosition;
            Drawing.DrawText(_allyStrength + " %", new Vector2(mousePosition.X + 28, mousePosition.Y), new Vector2(textSize), Color.LightSkyBlue, FontFlags.AntiAlias | FontFlags.Outline);
            Drawing.DrawText(_enemyStrength + " %", new Vector2(mousePosition.X + 28, mousePosition.Y + textSize + 1), new Vector2(textSize), Color.Red, FontFlags.AntiAlias | FontFlags.Outline);

            Drawing.DrawText(_fightResult, new Vector2(mousePosition.X + 28, mousePosition.Y + textSize * 2 + 1), new Vector2(textSize), Color.LightSkyBlue, FontFlags.AntiAlias | FontFlags.Outline);
        }

        public static void DrawOnHero()
        {
            if (!TFHelperVariables.DrawOnHero || Uncached || !TFHelperVariables.Enabled) return;

            var heroPosition = Drawing.WorldToScreen(Variables.Player.Position);
            var heroSize = UI.Elements.LocalHero.HeroScreenSize;

            var defaultUserSize = TFHelperVariables.HeroTextSize;
            var userSize = defaultUserSize / 4;

            var flags = FontFlags.DropShadow | FontFlags.Outline | FontFlags.AntiAlias;
            var text = _allyStrength + " % / " + _enemyStrength + " %";
            var color = _allyStrength > _enemyStrength ? Color.LightSkyBlue : Color.Red;
            var textPosition = HudHelper.GetTextPosition(heroPosition, text, heroSize + new Vector2(userSize), flags);

            Drawing.DrawText(_fightResult, textPosition, new Vector2(heroSize.Y * 2, heroSize.X) + new Vector2(userSize), Color.White, flags);
            Drawing.DrawText(text, new Vector2(textPosition.X, textPosition.Y + defaultUserSize * 1.1f),
                                new Vector2(heroSize.Y * 2, heroSize.X) + new Vector2(userSize * 2), color, flags);
        }

        private static bool Uncached => _allyStrength == -1 || _enemyStrength == -1 || _fightResult == "uncached";
    }
}