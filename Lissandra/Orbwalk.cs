using EloBuddy;
using EloBuddy.SDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lissandra
{
    class Orbwalk
    {
        public static void combo()
        {
            var target = TargetSelector.GetTarget(Spells.E.Range, EloBuddy.DamageType.Magical);
            if (target != null)
            {
                Logic.Elogic.ComboE(target);
                Logic.Qlogic.Qcast();
                Logic.Wlogic.Castw();
            }
        }
        public static void harass()
        {
            var target = TargetSelector.GetTarget(Spells.E.Range, EloBuddy.DamageType.Magical);
            if (target != null)
            {
                Logic.Elogic.harass(target);
                if (Extension.GetCheckBoxValue(Meniu.Harass, "harass.q"))
                {
                    Logic.Qlogic.Qcast();
                }
            }
        }
        public static void laneclear()
        {
            if (Spells.Q.IsReady() && Extension.GetCheckBoxValue(Meniu.Laneclear, "lane.q"))
            {
                Spells.Q.CastOnBestFarmPosition(Extension.GetSliderValue(Meniu.Laneclear,"min.q"),60);
            }
            if (Spells.E.IsReady() && Extension.GetCheckBoxValue(Meniu.Laneclear, "lane.e") && Events.lissE == null)
            {
                Spells.E.CastOnBestFarmPosition(Extension.GetSliderValue(Meniu.Laneclear, "min.E"), 60);
            }
            if (Spells.W.IsReady() && Extension.GetCheckBoxValue(Meniu.Laneclear, "lane.w"))
            {
                var wcount = Player.Instance.ServerPosition.CountEnemyMinionsInRange(Spells.W.Range);
                if (wcount >= Extension.GetSliderValue(Meniu.Laneclear, "min.w"))
                {
                    Spells.W.Cast();
                }
            }
        }
        public static void jungleclear()
        {
            var monster = EntityManager.MinionsAndMonsters.GetJungleMonsters().OrderByDescending(j => j.Health).FirstOrDefault(j => j.IsValidTarget(Spells.Q.Range));
            if (monster != null)
            {
                if (Spells.E.IsReady() && Extension.GetCheckBoxValue(Meniu.JungleClear, "jungle.e") && Events.lissE == null)
                {
                    Spells.E.Cast(monster.Position);
                }
                if (Spells.Q.IsReady() && Extension.GetCheckBoxValue(Meniu.JungleClear, "jungle.q"))
                {
                    Spells.Q.Cast(monster.Position);
                }
                if (Spells.W.IsReady() && Extension.GetCheckBoxValue(Meniu.JungleClear, "jungle.w") && Spells.W.IsInRange(monster.ServerPosition))
                {
                    Spells.W.Cast(monster.Position);
                }
            }
        }
        public static void flee()
        {
            Logic.Elogic.flee();
        }
    }
}
