namespace EvAwareness.Modules.GankAlert
{
    using System.Collections.Generic;

    using Ensage.Common.Menu;

    using Utility;

    class GankAlertVariables
    {
        public static int TextSize => MenuExtensions.GetItemValue<Slider>("evervolv.aware.gank.textsize").Value;
        public static int MinDist => MenuExtensions.GetItemValue<Slider>("evervolv.aware.gank.mindist").Value;
        public static int MaxDist => MenuExtensions.GetItemValue<Slider>("evervolv.aware.gank.maxdist").Value;
        public static Dictionary<string, bool> Whitelist = new Dictionary<string, bool>(); 
    }
}