namespace EvAwareness.Modules.TFHelper
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Ensage;
    using Ensage.Common.Extensions;
    using Ensage.Common.Menu;

    using Utility;

    public class TFHelperVariables
    {
        public static IEnumerable<Hero> EnemiesClose
        {
            get
            {
                return
                    Variables.Heroes.Enemies.Where(
                        m =>
                            m.Distance2D(Variables.Player) <= Math.Pow(TFRange, 2) && m.IsValidTarget(TFRange, false) &&
                            m.CountEnemiesInRange(m.IsMelee ? m.AttackRange * 1.5f : m.AttackRange + 20 * 1.5f) > 0);
            }
        }

        public static int TFRange => MenuExtensions.GetItemValue<Slider>("evervolv.aware.tf.range").Value;

        public static IEnumerable<Hero> AlliesClose
        {
            get
            {
                return
                    Variables.Heroes.Allies.Where(
                        m => m.Distance2D(Variables.Player) <= Math.Pow(TFRange, 2) && m.IsValidTarget(TFRange, false));
            }
        }

        public static string AllyStrengthText { get; set; }

        public static string EnemyStrengthText { get; set; }

        public static string TeamsVSText { get; set; }
    }
}