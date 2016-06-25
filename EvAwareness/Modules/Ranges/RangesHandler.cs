using System;
using EvAwareness.Utility;

namespace EvAwareness.Modules.Ranges
{
    using System.Collections.Generic;
    using System.Linq;

    using ClipperLib;

    using Ensage;
    using Ensage.Common.Extensions.SharpDX;
    using Ensage.Common.Menu;

    using global::EvAwareness.UI;

    using SharpDX;

    using Utility.Console;

    class RangesHandler : ModuleHandler
    {
        public override void OnTick() { }

        public override ModuleType GetModuleType() => ModuleType.General;

        public override bool ShouldRun() => MenuExtensions.GetItemValue<bool>("evervolv.aware.ranges.enemy") ||
                                            MenuExtensions.GetItemValue<bool>("evervolv.aware.ranges.ally");

        public override void CreateMenu()
        {
            try
            {
                var RootMenu = Variables.Menu;
                var moduleMenu = new Menu("AA Range Tracking", "evervolv.aware.ranges");
                {
                    moduleMenu.AddBool("evervolv.aware.ranges.ally", "Ally Ranges", true);
                    moduleMenu.AddBool("evervolv.aware.ranges.enemy", "Enemy Ranges", true);
                    RootMenu.AddSubMenu(moduleMenu);
                }
            }
            catch (Exception e)
            {
                ConsoleHelper.Print(new ConsoleItem("RangesHandler::CreateMenu", e));
            }
        }

        public override void InitEvents()
        {
            try
            {
                Drawing.OnDraw += Drawing_OnDraw;
            }
            catch (Exception e)
            {
                ConsoleHelper.Print(new ConsoleItem("RangesHandler::InitEvents", e));
            }
        }

        private void Drawing_OnDraw(EventArgs args)
        {
            if (Drawing.Direct3DDevice9 == null || Drawing.Direct3DDevice9.IsDisposed || !Game.IsInGame
                || ObjectManager.LocalHero == null) return;

            try
            {
                if (ShouldRun() && HudVariables.ShouldBeVisible)
                {
                    if (MenuExtensions.GetItemValue<bool>("evervolv.aware.ranges.enemy"))
                    {
                        this.DrawEnemyZone();
                    }

                    if (MenuExtensions.GetItemValue<bool>("evervolv.aware.ranges.ally"))
                    {
                        this.DrawAllyZone();
                    }
                }
            }
            catch (Exception e)
            {
                ConsoleHelper.Print(new ConsoleItem("RangesHandler::OnDraw", e));
            }
        }

        public void DrawAllyZone()
        {
            var currentPath = this.GetAllyPoints().Select(v2 => new IntPoint(v2.X, v2.Y)).ToList();
            var currentPoly = currentPath.ToPolygon();
            currentPoly.Draw(Color.Green);
        }

        private List<Vector2> GetAllyPoints(bool dynamic = true)
        {
            var staticRange = 360f;
            var polygonsList = Variables.Heroes.AlliesClose.Select(enemy => new Geometry.Circle(enemy.NetworkPosition.To2D(), (dynamic ? (enemy.IsMelee ? enemy.AttackRange * 1.5f : enemy.AttackRange) : staticRange) + enemy.HullRadius + 20).ToPolygon()).ToList();
            var pathList = Geometry.ClipPolygons(polygonsList);
            var pointList = pathList.SelectMany(path => path, (path, point) => new Vector2(point.X, point.Y)).Where(currentPoint => !currentPoint.IsWall()).ToList();
            return pointList;
        }

        public void DrawEnemyZone()
        {
            var currentPath = GetEnemyPoints().Select(v2 => new IntPoint(v2.X, v2.Y)).ToList();
            var currentPoly = currentPath.ToPolygon();
            currentPoly.Draw(Color.Red);
        }

        private List<Vector2> GetEnemyPoints(bool dynamic = true)
        {
            var staticRange = 360f;
            var polygonsList = Variables.Heroes.EnemiesClose.Select(enemy => new Geometry.Circle(enemy.NetworkPosition.To2D(), (dynamic ? (enemy.IsMelee ? enemy.AttackRange * 1.5f : enemy.AttackRange) : staticRange) + enemy.HullRadius + 20).ToPolygon()).ToList();
            var pathList = Geometry.ClipPolygons(polygonsList);
            var pointList = pathList.SelectMany(path => path, (path, point) => new Vector2(point.X, point.Y)).Where(currentPoint => !currentPoint.IsWall()).ToList();
            return pointList;
        }
    }
}