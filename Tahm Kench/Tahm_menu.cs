using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;

namespace Tamh_kench
{
    class Tahm_menu
    {
        public static Menu Menu, ComboMenu, HarassMenu, Rmenu, DrawMenu, SaveMenu;

        public static void Ini()
        {

            Menu = MainMenu.AddMenu("Big Fat Kench", "kbswag");
            Menu.AddGroupLabel("Big Fat Kench");

            ComboMenu = Menu.AddSubMenu("Combo Menu", "combomenuKench");
            ComboMenu.AddGroupLabel("Combo Settings");
            ComboMenu.Add("Combo.Q", new CheckBox("Use Q"));
            ComboMenu.Add("Combo.QOnlyStun", new CheckBox("Use Q Only Stun"));
            ComboMenu.Add("Combo.W.Enemy", new CheckBox("Use W on Enemy"));
            ComboMenu.Add("Combo.W.Minion", new CheckBox("Use W on Minions to Spit"));
            ComboMenu.Add("Combo.E", new CheckBox("Use E"));
            ComboMenu.Add("Combo.E.Hp", new Slider("Use E on hp %", 20, 5, 100));

            HarassMenu = Menu.AddSubMenu("Harass Menu", "harassmenuKench");
            HarassMenu.AddGroupLabel("Harass Settings");
            HarassMenu.Add("Harass.Q", new CheckBox("Use Q"));
            HarassMenu.Add("Harass.W.Minion", new CheckBox("Use W on Minions to Spit"));

            SaveMenu = Menu.AddSubMenu("Save Settings");
            SaveMenu.AddGroupLabel("Saving Allies Settings");
            SaveMenu.Add("enableSaving", new CheckBox("Saving Enabled"));
            SaveMenu.Add("save.allies.hp", new Slider("Save allies lower hp % than", 20, 1, 100));

            Rmenu = Menu.AddSubMenu("R Menu", "rto");
            Rmenu.AddGroupLabel("Cast R on Baron, Dragon");
            Rmenu.Add("r.dragon", new KeyBind("Cast R on Dragon", false, KeyBind.BindTypes.HoldActive,'U'));
            Rmenu.Add("r.baron", new KeyBind("Cast R on Baron", false, KeyBind.BindTypes.HoldActive, 'I'));
            DrawMenu = Menu.AddSubMenu("Draw Menu", "drawMenuKench");
            DrawMenu.AddGroupLabel("Draw Settings");
            DrawMenu.Add("Draw.Q", new CheckBox("Draw Q"));
            DrawMenu.AddSeparator();
            DrawMenu.Add("Draw.W", new CheckBox("Draw W"));
            DrawMenu.AddSeparator();
            DrawMenu.Add("Draw.R", new CheckBox("Draw R"));


        }


    }
}
