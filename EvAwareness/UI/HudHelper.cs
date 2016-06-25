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
    }
}