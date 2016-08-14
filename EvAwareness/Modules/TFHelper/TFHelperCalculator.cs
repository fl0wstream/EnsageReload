namespace EvAwareness.Modules.TFHelper
{
    using System.Collections.Generic;
    using System.Linq;

    using Ensage;
    using Ensage.Common;
    using Ensage.Common.AbilityInfo;
    using Ensage.Common.Extensions;

    using Utility;

    class TFHelperCalculator
    {
        /// <summary>
        /// Gets the ally team strength.
        /// </summary>
        /// <returns></returns>
        public static float GetAllyStrength()
        {
            if (!Variables.Player.IsAlive || (TFHelperVariables.AlliesClose.Any() && !TFHelperVariables.EnemiesClose.ToList().Any()))
            {
                return 0;
            }

            return 1 - GetEnemyStrength();
        }

        public static float GetEnemyStrength()
        {
            var allyDamage = TFHelperVariables.AlliesClose.ToList().Sum(s => GetHeroAvgDamage(s, TFHelperVariables.EnemiesClose.ToList()));
            var enemyDamage = TFHelperVariables.EnemiesClose.ToList().Sum(s => GetHeroAvgDamage(s, TFHelperVariables.AlliesClose.ToList()));

            if (enemyDamage <= 0 && allyDamage >= 0)
            {
                return 0;
            }

            if (!Variables.Player.IsAlive || (TFHelperVariables.AlliesClose.Any() && !TFHelperVariables.EnemiesClose.ToList().Any()))
            {
                return 0;
            }

            return (enemyDamage / allyDamage) <= 1 ? (enemyDamage / allyDamage) : (allyDamage / enemyDamage);
        }

        public static string GetText()
        {
            if ((TFHelperVariables.AlliesClose.Any() && !TFHelperVariables.EnemiesClose.ToList().Any()))
            {
                if (!Variables.Player.IsAlive)
                {
                    return "You kinda suck!";
                }

                return "No enemy around";
            }

            return Variables.Player.IsAlive ?
                $"{TFHelperVariables.AlliesClose.Count()}v{TFHelperVariables.EnemiesClose.Count()}: {(GetAllyStrength() > GetEnemyStrength() ? "Ally" : "Enemy")} will win"
                : string.Format("You kinda suck!");
        }

        public static float GetHeroAvgDamage(Hero player, List<Hero> Enemies)
        {
            var totalEnemies = Enemies.Count();
            if (totalEnemies == 0)
            {
                return -1;
            }

            /**
            var AADamage = Enemies.Aggregate(0, (current, s) => (int)(current + player.GetAutoAttackDamage(s) * 2));
            var QDamage = Enemies.Aggregate(0, (current, s) => (int)(current + (player.GetSpell(SpellSlot.Q).IsReady() ? player.GetSpellDamage(s, SpellSlot.Q) : 0f)));
            var WDamage = Enemies.Aggregate(0, (current, s) => (int)(current + (player.GetSpell(SpellSlot.W).IsReady() ? player.GetSpellDamage(s, SpellSlot.W) : 0f)));
            var EDamage = Enemies.Aggregate(0, (current, s) => (int)(current + (player.GetSpell(SpellSlot.E).IsReady() ? player.GetSpellDamage(s, SpellSlot.E) : 0f)));
            var RDamage = Enemies.Aggregate(0, (current, s) => (int)(current + (player.GetSpell(SpellSlot.R).IsReady() ? player.GetSpellDamage(s, SpellSlot.R) : 0f)));
            */
            var AADamage = Enemies.Aggregate(
                0,
                (current, s) =>
                (int)
                (current
                 + s.DamageTaken(player.DamageAverage + player.BonusDamage, DamageType.Physical, player, true) * 2));
            var QDamage = Enemies.Aggregate(
                0,
                (current, s) =>
                (int)
                (current
                 + ((player.Spellbook.SpellQ.CanBeCasted() || player.Spellbook.SpellQ.AbilityBehavior == AbilityBehavior.Passive)
                        ? AbilityDamage.CalculateDamage(player.Spellbook.SpellQ, player, s)
                        : 0f)));
            var WDamage = Enemies.Aggregate(
                0,
                (current, s) =>
                (int)
                (current
                 + ((player.Spellbook.SpellW.CanBeCasted() || player.Spellbook.SpellW.AbilityBehavior == AbilityBehavior.Passive)
                        ? AbilityDamage.CalculateDamage(player.Spellbook.SpellW, player, s)
                        : 0f)));
            var EDamage = Enemies.Aggregate(
                0,
                (current, s) =>
                (int)
                (current
                 + ((player.Spellbook.SpellE.CanBeCasted() || player.Spellbook.SpellE.AbilityBehavior == AbilityBehavior.Passive)
                        ? AbilityDamage.CalculateDamage(player.Spellbook.SpellE, player, s)
                        : 0f)));
            var RDamage = Enemies.Aggregate(
                0,
                (current, s) =>
                (int)
                (current
                 + ((player.Spellbook.SpellR.CanBeCasted() || player.Spellbook.SpellR.AbilityBehavior == AbilityBehavior.Passive)
                        ? AbilityDamage.CalculateDamage(player.Spellbook.SpellR, player, s)
                        : 0f)));

            var itemsDamage = 0f;

            foreach (var item in player.Inventory.Items)
            {
                foreach (var hero in Enemies)
                {
                    if (item.CanBeCasted())
                        itemsDamage += AbilityDamage.CalculateDamage(item, player, hero);
                }

            }

            var totalDamage = AADamage + QDamage + WDamage + EDamage + RDamage + itemsDamage;

            return (float)totalDamage / totalEnemies;
        }
    }
}