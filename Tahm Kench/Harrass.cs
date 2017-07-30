using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Enumerations;

namespace Tamh_kench
{
    class Harrass : Tahm_menu
    {
        public static void Harras()
        {
            var qtarget = TargetSelector.GetTarget(Spells.Q.Range, DamageType.Magical);
            var wsplitt = TargetSelector.GetTarget(Spells.WSpit.Range, DamageType.Magical);
            var closestminion =
                EntityManager.MinionsAndMonsters.EnemyMinions.OrderBy(x => x.Distance(Player.Instance)).FirstOrDefault();
            if (qtarget != null)
            {
                var qpred = Spells.Q.GetPrediction(qtarget);
                if (Spells.Q.IsInRange(qtarget) && qpred.HitChance >= HitChance.Medium &&
                    Spells.Q.IsReady() &&
                    Extension.GetCheckBoxValue(HarassMenu, "Harass.Q"))
                {
                    Spells.Q.Cast(qtarget);
                }
            }
            if (wsplitt != null)
            {
                if (Spells.WSpit.IsReady() && Extension.GetCheckBoxValue(HarassMenu, "Harass.W.Minion"))
                {
                    if (!Extension.Swallowed && closestminion != null)
                    {
                        Spells.WSwallow.Cast(closestminion);
                    }
                    else if (Extension.Swallowed)
                    {
                        Spells.WSpit.Cast(wsplitt);
                    }
                }
            }
        }
    }
}
