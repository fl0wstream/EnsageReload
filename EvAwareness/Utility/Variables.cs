// --------------------------------------------------------------------------------------------------------------------
// <copyright company="EnsageSharp" file="Program.cs">
//   Copyright (c) 2015 EnsageSharp.
//           This program is free software: you can redistribute it and/or modify
//           it under the terms of the GNU General Public License as published by
//           the Free Software Foundation, either version 3 of the License, or
//           (at your option) any later version.
//           
//           This program is distributed in the hope that it will be useful,
//           but WITHOUT ANY WARRANTY; without even the implied warranty of
//           MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//           GNU General Public License for more details.
//           
//           You should have received a copy of the GNU General Public License
//           along with this program.  
//           If not, see http://www.gnu.org/licenses
// </copyright>
// 
// --------------------------------------------------------------------------------------------------------------------

namespace EvAwareness.Utility
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    using Ensage;
    using Ensage.Common.Extensions;
    using Ensage.Common.Menu;

    using Modules;
    using Modules.MissTracker;
    using Modules.GankAlert;
    using Modules.Ranges;
    using Modules.TFHelper;

    class Variables
    {
        public static List<ModuleHandler> ModulesList = new List<ModuleHandler>()
                                                            {
                                                                new MissTrackerHandler(),
                                                                new GankAlertHandler(),
                                                                new RangesHandler(),
                                                                new TFHelperHandler()
                                                            };

        public static Menu Menu { get; set; }

        public static Hero Player => ObjectManager.LocalHero;

        public static bool IsDevelopment => Program.IsDevelopment;

        public static string Version
            => Assembly.GetExecutingAssembly().GetName().Version.ToString().TrimEnd(".0".ToCharArray());

        public class Heroes
        {
            public static List<Hero> Enemies
                => ObjectManager.GetEntities<Hero>().Where(x => x.Team != Player.Team).ToList();

            public static List<Hero> Allies
                => ObjectManager.GetEntities<Hero>().Where(x => x.Team == Player.Team).ToList();

            public static List<Hero> AlliesClose
                => Allies.Where(
                        m => m.Index != Player.Index &&
                            m.Distance2D(Player) <= Math.Pow(1000, 2) && m.IsValidTarget(1000, false)).ToList();

            public static List<Hero> EnemiesClose
                => Enemies.Where(
                        m => m.Distance2D(Player) <= Math.Pow(1000, 2) && m.IsValidTarget(1500, false) &&
                            m.CountEnemiesInRange(m.IsMelee ? m.AttackRange * 1.5f : m.AttackRange + 20 * 1.5f) > 0).ToList();
        }
    }
}
