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

namespace EvAwareness
{
    using Ensage;
    using Ensage.Common.Menu;

    using Utility;
    using Utility.Console;

    using SharpDX;

    using MessageType = Ensage.MessageType;

    public class Bootstrap
    {
        public static void OnLoad()
        {
            Variables.Menu = new Menu("EvAwareness#", "evervolv.aware", true);

            ConsoleHelper.OnLoad();

            if (Variables.IsDevelopment)
                Variables.Menu.AddItem(
                    new MenuItem("evervolv.aware.devalert", Variables.Version + " dev").SetFontColor(new Color(153, 153, 255)));
            Variables.Menu.AddToMainMenu();
            
            if (Variables.IsDevelopment)
                Game.PrintMessage("<font color='#9999ff'>Ev</font>Awareness Loaded <font color='#9999ff'>[dev " + Variables.Version + "]</font>", MessageType.LogMessage);
            else
                Game.PrintMessage("<font color='#9999ff'>Ev</font>Awareness Loaded", MessageType.LogMessage);

            ConsoleHelper.Print(new ConsoleItem("Bootstrap", "Completed!"));
        }
    }
}