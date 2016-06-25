namespace EvAwareness.Utility
{
    using System;

    using Ensage;

    using SharpDX;

    class CommonHelper
    {
        /* 
         * Regards to JumpAttacker
         * for this code.
         * github.com/JumpAttacker
         */

        private const float MapLeft = -8000;
        private const float MapTop = 7350;
        private const float MapRight = 7500;
        private const float MapBottom = -7200;
        private static readonly float MapWidth = Math.Abs(MapLeft - MapRight);
        private static readonly float MapHeight = Math.Abs(MapBottom - MapTop);

        public static Vector2 WorldToMinimap(Vector3 pos)
        {
            var x = pos.X - MapLeft;
            var y = pos.Y - MapBottom;

            float dx, dy, px, py;
            if (Math.Round((float)Drawing.Width / Drawing.Height, 1) >= 1.7)
            {
                dx = 272f / 1920f * Drawing.Width;
                dy = 261f / 1080f * Drawing.Height;
                px = 11f / 1920f * Drawing.Width;
                py = 11f / 1080f * Drawing.Height;
            }
            else if (Math.Round((float)Drawing.Width / Drawing.Height, 1) >= 1.5)
            {
                dx = 267f / 1680f * Drawing.Width;
                dy = 252f / 1050f * Drawing.Height;
                px = 10f / 1680f * Drawing.Width;
                py = 11f / 1050f * Drawing.Height;
            }
            else
            {
                dx = 255f / 1280f * Drawing.Width;
                dy = 229f / 1024f * Drawing.Height;
                px = 6f / 1280f * Drawing.Width;
                py = 9f / 1024f * Drawing.Height;
            }
            var minimapMapScaleX = dx / MapWidth;
            var minimapMapScaleY = dy / MapHeight;

            var scaledX = Math.Min(Math.Max(x * minimapMapScaleX, 0), dx);
            var scaledY = Math.Min(Math.Max(y * minimapMapScaleY, 0), dy);

            var screenX = px + scaledX;
            var screenY = Drawing.Height - scaledY - py;

            return new Vector2((float)Math.Floor(screenX), (float)Math.Floor(screenY));
        }

        public static string GetHeroName(Hero hero)
        {
            return FirstUpper(hero.Name.Split(new string[] { "npc_dota_hero_" }, StringSplitOptions.None)[1]).Replace("_", " ");
        }

        public static string FirstUpper(string str)
        {
            string[] s = str.Split(' ');

            for (int i = 0; i < s.Length; i++)
            {
                if (s[i].Length > 1)
                    s[i] = s[i].Substring(0, 1).ToUpper() + s[i].Substring(1, s[i].Length - 1).ToLower();
                else s[i] = s[i].ToUpper();
            }
            str = string.Join(" ", s);

            string[] s1 = str.Split('_');

            for (int i = 0; i < s1.Length; i++)
            {
                if (s1[i].Length > 1)
                    s1[i] = s1[i].Substring(0, 1).ToUpper() + s1[i].Substring(1, s1[i].Length - 1).ToLower();
                else s1[i] = s1[i].ToUpper();
            }
            return string.Join("_", s1);
        }
    }
}
