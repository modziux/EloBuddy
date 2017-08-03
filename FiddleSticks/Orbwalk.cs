using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EloBuddy;
using EloBuddy.SDK;

namespace Fiddlestick
{
    class Orbwalk
    {
        public static void Combo()
        {
            if (Spells.Q.IsReady())
            {
                foreach (var enemy in EntityManager.Heroes.Enemies.Where(x => Spells.Q.IsInRange(x) && !x.IsDead))
                {
                    if (Extension.GetCheckBoxValue(Meniu.Q,enemy.ChampionName))
                    {
                        Spells.Q.Cast(enemy);
                    }
                }
            }
            var wtarget = TargetSelector.GetTarget(350, DamageType.Magical);
            if (wtarget != null)
            {
                if (Spells.W.IsReady() && Extension.GetCheckBoxValue(Meniu.W, wtarget.ChampionName))
                {
                    Player.CastSpell(SpellSlot.W, wtarget);
                }
            }
            if (Spells.E.IsReady())
            {
                foreach (var enemy in EntityManager.Heroes.Enemies.Where(x => Spells.E.IsInRange(x) && !x.IsDead))
                {
                    if (Extension.GetCheckBoxValue(Meniu.E, enemy.ChampionName) && enemy.CountEnemyChampionsInRange(450) >= Extension.GetSliderValue(Meniu.E,"e.min"))
                    {
                        Spells.E.Cast(enemy);
                    }
                }
            }
            if (Spells.R.IsReady())
            {
                var range = (float)Extension.GetSliderValue(Meniu.R, "r.range");
                foreach (var enemy in EntityManager.Heroes.Enemies.Where(x => x.IsInRange(Player.Instance,800) && !x.IsDead))
                {
                    if (Extension.GetCheckBoxValue(Meniu.R,"use.r") && enemy.CountEnemyChampionsInRange(range) >= Extension.GetSliderValue(Meniu.R, "r.min"))
                    {
                        Player.CastSpell(SpellSlot.R, enemy.Position);
                    }
                }
            }
        }
        public static void Harass()
        {
            if (Spells.E.IsReady())
            {
                foreach (var enemy in EntityManager.Heroes.Enemies.Where(x => Spells.E.IsInRange(x) && !x.IsDead))
                {
                    if (Extension.GetCheckBoxValue(Meniu.E, enemy.ChampionName) && enemy.CountEnemyChampionsInRange(450) >= Extension.GetSliderValue(Meniu.E, "e.min"))
                    {
                        Spells.E.Cast(enemy);
                    }
                }
            }
        }
        public static void LaneClear()
        {
            var count = EntityManager.MinionsAndMonsters.GetLaneMinions(EntityManager.UnitTeam.Enemy, Player.Instance.Position, Spells.E.Range);
            var minnions = count.OrderByDescending(j => j.Health).FirstOrDefault(j => j.IsValidTarget(Spells.E.Range));
            if (minnions != null)
            {
                if (count.Count() >= Extension.GetSliderValue(Meniu.Laneclear, "lane.e.min"))
                {
                    if (Spells.E.IsReady() && Extension.GetCheckBoxValue(Meniu.Laneclear, "lane.e") && !Extension.IsWActive)
                    {
                        Spells.E.Cast(minnions);
                    }
                }
                if (Spells.W.IsReady() && Extension.GetCheckBoxValue(Meniu.Laneclear, "lane.w") && !Extension.IsWActive)
                {
                    Player.CastSpell(SpellSlot.W, minnions);
                }
            }
        }
        public static void JungleClear()
        {
            var monster = EntityManager.MinionsAndMonsters.GetJungleMonsters().OrderByDescending(j => j.Health).FirstOrDefault(j => j.IsValidTarget(Spells.E.Range));
            var count = EntityManager.MinionsAndMonsters.GetJungleMonsters();
            if (monster != null)
            {
                if (count.Count() > 1)
                {
                    if (Spells.E.IsReady() && Extension.GetCheckBoxValue(Meniu.JungleClear, "jungle.e") && !Extension.IsWActive)
                    {
                        Spells.E.Cast(monster);
                    }
                }
                if (Spells.Q.IsReady() && Extension.GetCheckBoxValue(Meniu.JungleClear, "jungle.q") && !Extension.IsWActive)
                {
                    Spells.Q.Cast(monster);
                }
                if (Spells.W.IsReady() && Extension.GetCheckBoxValue(Meniu.JungleClear, "jungle.w") && !Extension.IsWActive)
                {
                    Player.CastSpell(SpellSlot.W, monster);
                }
            }
        }
    }
}
