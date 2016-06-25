namespace EvAwareness.Modules.TFHelper
{
    using System;

    using Ensage;

    using SharpDX;

    using UI;
    using Utility;

    class TFHelperDrawings
    {
        public static void OnLoad()
        {
            Drawing.OnDraw += Drawing_OnDraw;
        }

        private static void Drawing_OnDraw(EventArgs args)
        {
            if (Drawing.Direct3DDevice9 == null || Drawing.Direct3DDevice9.IsDisposed || !Game.IsInGame
                || ObjectManager.LocalHero == null) return;

            if (!HudVariables.ShouldBeVisible || !MenuExtensions.GetItemValue<bool>("evervolv.aware.tf.enabled")) return;

            if (Variables.IsDevelopment)
            {
                Drawing.DrawRect(new Vector2(100, 200), new Vector2(250, 200), Drawing.GetTexture("materials/ensage_ui/menu/itembg1.vmat"));
                Drawing.DrawText("TFHelper devpanel", new Vector2(108, 208), new Vector2(20), Color.White, FontFlags.AntiAlias);
                Drawing.DrawLine(new Vector2(108, 228), new Vector2(350 - 8, 228), Color.White);

                Drawing.DrawText(Math.Round(TFHelperCalculator.GetAllyStrength() * 100) + " %", new Vector2(108, 238), new Vector2(20), Color.LightSkyBlue, FontFlags.AntiAlias | FontFlags.Outline);
                Drawing.DrawText(Math.Round(TFHelperCalculator.GetEnemyStrength() * 100) + " %", new Vector2(108, 258), new Vector2(20), Color.Red, FontFlags.AntiAlias | FontFlags.Outline);

                Drawing.DrawText(TFHelperCalculator.GetText(), new Vector2(108, 278), new Vector2(20), Color.LightSkyBlue, FontFlags.AntiAlias | FontFlags.Outline);
            }
        }
    }
}