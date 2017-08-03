using EloBuddy.SDK.Menu;
using EloBuddy.SDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fiddlestick
{
    class Meniu
    {
        public static Menu Main, Q, W, E, R, Laneclear, JungleClear, Drawings;
        public static void Ini()
        {
            Main = MainMenu.AddMenu("Fiddlestick", "Fiddle");
            Q = Main.AddSubMenu("Q Settings", "Q");
            foreach(var enemy in EntityManager.Heroes.Enemies)
            {
                Extension.createcheckbox(Q,enemy.ChampionName, "Use Q on " + enemy.ChampionName);
            }
            Q.AddSeparator();
            Extension.createcheckbox(Q, "q.channel", "Auto (Q) To Interupt Spell");
            W = Main.AddSubMenu("W Settings", "W");
            foreach (var enemy in EntityManager.Heroes.Enemies)
            {
                Extension.createcheckbox(W, enemy.ChampionName, "Use W on " + enemy.ChampionName);
            }
            E = Main.AddSubMenu("E Settings", "E");
            foreach (var enemy in EntityManager.Heroes.Enemies)
            {
                Extension.createcheckbox(E,enemy.ChampionName, "Use E on " + enemy.ChampionName);
            }
            E.AddSeparator();
            Extension.createslider(E, "e.min", "Min enemies to use E", 2, 1, 5);
            Extension.createcheckbox(E, "e.channel", "Auto (E) To Interupt Spell");
            R = Main.AddSubMenu("R Settings", "R");
            Extension.createcheckbox(R, "use.r", "Use R");
            Extension.createslider(R, "r.min", "Cast R on {0} Enemies", 3, 1, 5);
            Extension.createslider(R,"r.range","R Range",300,100,600 );
            Laneclear = Main.AddSubMenu("LaneClear", "lane");
            Extension.createcheckbox(Laneclear, "lane.W", "Use W ");
            Extension.createcheckbox(Laneclear, "lane.E", "Use E ");
            Extension.createslider(Laneclear, "lane.e.min", "Use E on {0} Minnions", 3, 1, 8);
            JungleClear = Main.AddSubMenu("JungleClear", "jungle");
            Extension.createcheckbox(JungleClear, "jungle.q", "Use Q");
            Extension.createcheckbox(JungleClear, "jungle.W", "Use W");
            Extension.createcheckbox(JungleClear, "jungle.e", "Use E");
            Drawings = Main.AddSubMenu("Drawings", "draw");
            Extension.createcheckbox(Drawings, "draw.q", "Draw Q");
            Extension.createcheckbox(Drawings, "draw.w", "Draw W");
            Extension.createcheckbox(Drawings, "draw.e", "Draw E");
            Extension.createcheckbox(Drawings, "draw.r", "Draw R");
        }
    }

}
