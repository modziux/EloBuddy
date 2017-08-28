using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EloBuddy;
using EloBuddy.SDK;
using SharpDX;

namespace lux
{
    class Lux
    {
        public static void combo()
        {
            if (Spells.Q.IsReady() && Extension.GetCheckBoxValue(Meniu.Combo, "combo.q"))
            {
                foreach (var enemy in EntityManager.Heroes.Enemies.Where(x=> Spells.Q.IsInRange(x) && !x.IsDead))
                {
                    logic.Qlogic.simpleQ(enemy);
                }
            }
            if (!ObjectManager.Player.IsDead && Spells.E.IsReady() && Extension.GetCheckBoxValue(Meniu.Combo,"combo.e") && Extension.ECheck() != 2)
            {
                Spells.E.CastIfItWillHit(Extension.GetSliderValue(Meniu.Combo, "combo.e.enemies"), 85);
            }
            if (Spells.R.IsReady() && Extension.GetCheckBoxValue(Meniu.Combo, "combo.r"))
            {
                foreach (var enemy in EntityManager.Heroes.Enemies.Where(x => Spells.R.IsInRange(x) && !x.IsDead))
                {
                    logic.Rlogic.Rchoise(enemy);
                }
            }
        }
        public static void harass()
        {
            if (Spells.Q.IsReady() && Extension.GetCheckBoxValue(Meniu.Harras, "harass.q"))
            {
                foreach (var enemy in EntityManager.Heroes.Enemies.Where(x => Spells.R.IsInRange(x) && !x.IsDead))
                {
                    logic.Qlogic.simpleQ(enemy);
                }
            }
            if (!ObjectManager.Player.IsDead && Spells.E.IsReady() && Extension.GetCheckBoxValue(Meniu.Harras, "harass.e") && Extension.ECheck() != 2)
            {
                Spells.E.CastIfItWillHit(Extension.GetSliderValue(Meniu.Harras, "harass.e.enemies"),Extension.GetSliderValue(Meniu.Prediction, "e.prediction"));
            }
        }
        public static void laneclear()
        {
            if (Spells.E.IsReady() && Extension.GetCheckBoxValue(Meniu.Laneclear,"laneclear.e"))
            {
                Spells.E.CastOnBestFarmPosition(Extension.GetSliderValue(Meniu.Laneclear, "laneclear.e.min"),Extension.GetSliderValue(Meniu.Prediction, "q.prediction"));
            }
            if (Spells.Q.IsReady() && Extension.GetCheckBoxValue(Meniu.Laneclear, "laneclear.q"))
            {
                Spells.Q.CastOnBestFarmPosition(2,Extension.GetSliderValue(Meniu.Prediction, "q.prediction"));
            }
        }
        public static void jungleclear()
        {
            var monster = EntityManager.MinionsAndMonsters.GetJungleMonsters().OrderByDescending(j => j.Health).FirstOrDefault(j => j.IsValidTarget(Spells.Q.Range));
            if (monster != null)
            {
                if (Spells.E.IsReady() && Extension.GetCheckBoxValue(Meniu.Jungleclear, "jungleclear.e"))
                {
                    Spells.E.Cast(monster.Position);
                }
                if (Spells.Q.IsReady() && Extension.GetCheckBoxValue(Meniu.Jungleclear, "jungleclear.q"))
                {
                    Spells.Q.Cast(monster.Position);
                }
            }
        }
    }
}
