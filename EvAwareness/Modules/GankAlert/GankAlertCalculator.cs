namespace EvAwareness.Modules.GankAlert
{
    using System;
    using System.Linq;

    using Ensage;
    using Ensage.Common.Extensions;

    using MissTracker;

    using Utility.Console;
    using Utility;

    public class GankAlertCalculator
    {
        public static void OnLoad()
        {
            try
            {
                //
            }
            catch (Exception e)
            {
                ConsoleHelper.Print(new ConsoleItem("Gank_Alerter", e, MessageClass.Severe));
            }
        }

        public static Hero GetGankingHero()
        {
            try
            {
                foreach (var hero in MissTrackerModule.Trackers.Values.Where(h => 
                    GankAlertVariables.Whitelist.FirstOrDefault(h2 => h2.Key == h.Hero.Name).Value && h.Hero.IsAlive))
                {
                    var heroDistance = 0f;
                    if (hero.Status == TrackStatus.InFog)
                        heroDistance = hero.LastPosition.Distance(Variables.Player.NetworkPosition);
                    else if (hero.Status == TrackStatus.Visible || hero.Status == TrackStatus.Invisible)
                        heroDistance = hero.Hero.NetworkPosition.Distance(Variables.Player.NetworkPosition);

                    if (heroDistance >= GankAlertVariables.MinDist && heroDistance <= GankAlertVariables.MaxDist && hero.Hero.IsAlive)
                    {
                        return hero.Hero;
                    }
                }
            }
            catch (Exception e)
            {
                ConsoleHelper.Print(new ConsoleItem("GankAlertCalculator::GetGankingHero", e));
            }

            return null;
        }
    }
}