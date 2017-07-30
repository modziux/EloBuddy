using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EloBuddy;
using EloBuddy.SDK;

namespace Lissandra.Logic
{
    class Perma
    {
        public static void ini()
        {
            if (Spells.R.IsReady() && Extension.GetCheckBoxValue(Meniu.Combo,"combo.r.ene"))
            {

                foreach (var enemy in EntityManager.Enemies.Where(x => x.IsValidTarget(Spells.R.Range) && !x.IsDead && !x.IsZombie && !x.IsMinion && !x.IsMonster))
                {
                    if (Spells.GetDamage(enemy,SpellSlot.R) >= enemy.Health)
                    {
                        Rlogic.CastR(enemy);                     
                    }
                    else if (enemy.CountEnemyChampionsInRange(Spells.R.Range) >= Extension.GetSliderValue(Meniu.Combo, "combo.r.min")  )
                    {
                        Rlogic.CastR(enemy);
                    }
                }
            }
        }
    }
}
