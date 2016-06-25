namespace EvAwareness.UI
{
    using Ensage;
    using Ensage.Common;

    using SharpDX;

    public class HudHelper
    {
        public static Vector2 GetTopPanelPosition(Hero hero)
        {
            var hudInfo = HUDInfo.GetTopPanelPosition(hero);
            var hudInfoSize = HUDInfo.GetTopPanelSize(hero);

            hudInfo.Y += (float)hudInfoSize[1] - 13;
            hudInfo.X += 1;

            return hudInfo;
        }

        public static Vector2 GetTopPanelSize(Hero hero)
        {
            var hudInfoSize = HUDInfo.GetTopPanelSize(hero);
            
            return new Vector2((float)hudInfoSize[0], (float)hudInfoSize[1]);
        }

        public static DotaTexture GetHeroTextureMinimap(string heroName)
        {
            var name = "materials/ensage_ui/miniheroes/" + heroName.Substring("npc_dota_hero_".Length) + ".vmat";

            return Drawing.GetTexture(name);
        }

        public static Vector2 MeasureText(string text, Vector2 size, FontFlags flags)
        {
            return Drawing.MeasureText(text, "Arial", new Vector2(size.Y * 2, size.X), flags);
        }

        public static Vector2 GetTextPosition(Vector2 start, string text, Vector2 size, FontFlags flags)
        {
            var textSize = MeasureText(text, size, flags);
            return start + new Vector2(size.X / 2 - textSize.X / 2, -textSize.Y / 2 + 2);
        }
    }
}