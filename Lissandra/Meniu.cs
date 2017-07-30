using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lissandra
{
    class Meniu
    {
        public static Menu Menu, Combo, Harass, Laneclear, JungleClear, Misc, Drawing;
        public static void Ini()
        {
            Menu = MainMenu.AddMenu("Lissandra", "liss");
            Menu.AddGroupLabel("Lissandra By modziux");
            Combo = Menu.AddSubMenu("Combo", "Kombo");
            Extension.createcheckbox(Combo, "combo.q", "Use Q");
            Combo.AddSeparator();
            Extension.createcheckbox(Combo, "combo.w", "Use W");
            Extension.createslider(Combo, "combo.min.w", "Minimum Enemies to use W", 2, 1, 5);
            Combo.AddSeparator();
            Extension.createcheckbox(Combo, "combo.e", "Use E");
            Extension.createslider(Combo, "combo.ew", "Minimum enemies To use E W combo", 3, 1, 5);
            Extension.createcheckbox(Combo, "combo.e2", "Use E2");
            Combo.AddSeparator();
            Extension.createcheckbox(Combo, "combo.r.ene", "Use R on Enemies");
            Extension.createslider(Combo, "combo.r.min", "Use R on X Enemies", 2, 1, 5);
            Harass = Menu.AddSubMenu("Harass", "harr");
            Extension.createcheckbox(Harass, "harass.q", "Use Q");
            Extension.createcheckbox(Harass, "harass.e", "Use E");
            Laneclear = Menu.AddSubMenu("LaneClear", "lane");
            Extension.createcheckbox(Laneclear, "lane.q", "Use Q");
            Extension.createslider(Laneclear, "min.q", "Min minnions hit with Q", 3, 1, 10);
            Laneclear.AddSeparator();
            Extension.createcheckbox(Laneclear, "lane.w", "Use W");
            Extension.createslider(Laneclear, "min.w", "Min minnions hit with W", 3, 1, 10);
            Laneclear.AddSeparator();
            Extension.createcheckbox(Laneclear, "lane.e", "Use E");
            Extension.createslider(Laneclear, "min.e", "Min minnions hit with E", 3, 1, 10);
            JungleClear = Menu.AddSubMenu("JungleClear", "jungle");
            Extension.createcheckbox(JungleClear, "jungle.q", "Use Q");
            Extension.createcheckbox(JungleClear, "jungle.w", "Use W");
            Extension.createcheckbox(JungleClear, "jungle.e", "Use E");
            Misc = Menu.AddSubMenu("Misc", "kita");
            Extension.createcheckbox(Misc, "misc.r.me", "Use R on Lissandra");
            Extension.createslider(Misc, "misc.r.min", "Use R on X Percent of Health", 30, 1, 100);
            Extension.createcheckbox(Misc, "interupter", "Use R to Interupt Spells");
            Extension.createcheckbox(Misc, "gapcloser.r", "Use R to Gapclose");
            Extension.createcheckbox(Misc, "gapcloser.w", "Use W to Gapclose");
            Drawing = Menu.AddSubMenu("Drawings", "draw");
            Extension.createcheckbox(Drawing, "draw.q", "Draw Q");
            Extension.createcheckbox(Drawing, "draw.w", "Draw W");
            Extension.createcheckbox(Drawing, "draw.e", "Draw E");
            Extension.createcheckbox(Drawing, "draw.r", "Draw R");
            Extension.createcheckbox(Drawing, "draw.damage", "Draw Damage");
            Extension.createcheckbox(Drawing, "draw.percent", "Draw Damage Percent");
        }
    }
}
