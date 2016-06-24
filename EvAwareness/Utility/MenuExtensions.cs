namespace EvAwareness.Utility
{
    using System;

    using Ensage.Common.Menu;

    static class MenuExtensions
    {
        public static MenuItem AddBool(this Menu menu, string name, string displayName, bool defaultValue = false)
        {
            return menu.AddItem(new MenuItem(name, displayName).SetValue(defaultValue));
        }

        public static MenuItem AddSlider(this Menu menu, string name, string displayName, Tuple<int, int, int> values)
        {
            return menu.AddItem(new MenuItem(name, displayName).SetValue(new Slider(values.Item1, values.Item2, values.Item3)));
        }

        public static MenuItem AddSlider(this Menu menu, string name, string displayName, int value, int min, int max)
        {
            return menu.AddItem(new MenuItem(name, displayName).SetValue(new Slider(value, min, max)));
        }

        public static MenuItem AddKeybind(this Menu menu, string name, string displayName, Tuple<uint, KeyBindType> value)
        {
            return menu.AddItem(new MenuItem(name, displayName).SetValue(new KeyBind(value.Item1, value.Item2)));
        }

        public static MenuItem AddText(this Menu menu, string name, string displayName)
        {
            return menu.AddItem(new MenuItem(name, displayName));
        }

        public static MenuItem AddStringList(this Menu menu, string name, string displayName, string[] value, int index = 0)
        {
            return menu.AddItem(new MenuItem(name, displayName).SetValue(new StringList(value, index)));
        }

        public static T GetItemValue<T>(string item)
        {
            return Variables.Menu.Item(item).GetValue<T>();
        }
    }
}