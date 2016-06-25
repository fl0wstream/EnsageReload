namespace EvAwareness.Modules.MissTracker
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.AccessControl;

    using Ensage;
    using Ensage.Common;
    using Ensage.Common.Extensions;

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
                    Trackers.Add(enemy.Name, new HeroTracker() { Hero = enemy });
                    ConsoleHelper.Print(new ConsoleItem("MissTrackerModule::OnLoad", "Added " + enemy.Name));
                }

                Game.OnUpdate += OnUpdate;
            }
            catch (Exception e)
            {
                ConsoleHelper.Print(new ConsoleItem("MissTrackerModule::OnLoad", e, MessageClass.Severe));
            }
        }

        private static void OnUpdate(EventArgs args)
        {
            if (!Utils.SleepCheck("aware.heroupdate")) return;

            // Adding new heroes
            foreach (var h in Variables.Heroes.Enemies.Where(x => x.IsAlive))
            {
                var hero = Trackers.Values.FirstOrDefault(h2 => h2.Hero.Index == h.Index);
                if (hero == null)
                {
                    Trackers.Add(h.Name, new HeroTracker() { Hero = h });
                    ConsoleHelper.Print(new ConsoleItem("MissTrackerModule::OnUpdate", "Added " + h.Name));
                }
            }

            // Updating heroes
            foreach (var hero in Trackers.Values)
            {
                var curStat = hero.GetStatus();
                if (hero.Status != hero.GetStatus())
                {
                    hero.LastSeen = Game.GameTime;
                    if (hero.Hero != null) hero.LastPosition = hero.Hero.NetworkPosition;
                    hero.Status = curStat;
                }
            }

            Utils.Sleep(200, "aware.heroupdate");
        }
    }

    public class HeroTracker
    {
        public Hero Hero { get; set; }

        public float LastSeen { get; set; }

        public string SSTime => Status != TrackStatus.Visible ? ((int)(Game.GameTime - LastSeen)).ToString() : String.Empty;

        public int SSTimeInt => (int)(Game.GameTime - LastSeen);

        public Vector3 LastPosition { get; set; }

        public TrackStatus Status { get; set; }

        public Color GetColor()
        {
            switch (Status)
            {
                case TrackStatus.Visible:
                    return Color.Green;
                case TrackStatus.InFog:
                    return Color.YellowGreen;
                case TrackStatus.Invalid:
                    return Color.Gray;
                case TrackStatus.Invisible:
                    return Color.LightYellow;
                default:
                    return Color.Gray;
            }
        }

        public TrackStatus GetStatus()
        {
            return !Hero.IsValid ? TrackStatus.Invalid : 
                    Hero.IsInvisible() ? TrackStatus.Invisible : 
                    Hero.IsVisible ? TrackStatus.Visible : 
                    TrackStatus.InFog;
        }
    }

    public enum TrackStatus
    {
        Invalid,
        Invisible,
        Visible,
        InFog
    }
}