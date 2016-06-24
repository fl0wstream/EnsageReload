namespace EvAwareness.Modules.MissTracker
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Ensage;

    using Utility;
    using Utility.Console;

    using SharpDX;

    class MissTrackerModule
    {
        public static Dictionary<string, HeroTracker> Trackers = new Dictionary<string, HeroTracker>();

        public static void OnLoad()
        {
            try
            {
                foreach (var enemy in Variables.Heroes.Enemies.Where(x => x.IsAlive))
                {
                    Trackers.Add(enemy.Name, new HeroTracker() { Hero = enemy, LastSeen = -1 });
                }

                Game.OnUpdate += OnUpdate;
            }
            catch (Exception e)
            {
                ConsoleHelper.Print(new ConsoleItem("SSTracker", e, MessageClass.Severe));
            }
        }

        private static void OnUpdate(EventArgs args)
        {
            foreach (var h in Variables.Heroes.Enemies)
            {
                var hero = Trackers.Values.FirstOrDefault(h2 => h2.Hero.HeroID == h.HeroID);
                if (hero != null)
                {
                    if (hero.LastSeen < 0 && !h.IsVisible)
                    {
                        hero.LastSeen = Environment.TickCount;
                        hero.LastPosition = hero.Hero.NetworkPosition;
                    }
                    if (hero.SSTimeFloat > 1 && (h.IsVisible))
                    {
                        hero.LastPosition = Vector3.Zero;
                        hero.LastSeen = -1;
                    }
                }
            }
        }
    }

    public class HeroTracker
    {
        public Hero Hero { get; set; }

        public float LastSeen { get; set; }

        public float SSTimeFloat => LastSeen > -1 ? (float)Math.Ceiling((Environment.TickCount - LastSeen) / 1000 + 0.5) : 0;

        public string SSTime => LastSeen > -1 ? Math.Ceiling((Environment.TickCount - LastSeen) / 1000 + 0.5).ToString() : "";

        public Vector3 LastPosition { get; set; } = Vector3.Zero;
    }
}