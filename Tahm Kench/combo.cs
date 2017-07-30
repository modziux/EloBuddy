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
    class combo : Tahm_menu
    {
        public static void Combo()
        {
            var qtarget = TargetSelector.GetTarget(Spells.Q.Range, DamageType.Magical);
            var wswallowt = TargetSelector.GetTarget(Spells.WSwallow.Range, DamageType.Magical);
            var wsplitt = TargetSelector.GetTarget(Spells.WSpit.Range, DamageType.Magical);
            var closestminion = EntityManager.MinionsAndMonsters.EnemyMinions.OrderBy(x => x.Distance(Player.Instance)).FirstOrDefault();
            if (qtarget != null)
            {
                var qpred = Spells.Q.GetPrediction(qtarget);
                if (Spells.Q.IsInRange(qtarget) && qpred.HitChance >= HitChance.Medium &&
                    Spells.Q.IsReady() && !Extension.GetCheckBoxValue(ComboMenu, "Combo.QOnlyStun") &&
                    Extension.GetCheckBoxValue(ComboMenu, "Combo.Q"))
                {
                    Spells.Q.Cast(qtarget);
                }
                else if (Spells.Q.IsInRange(qtarget) && qpred.HitChance >= HitChance.Medium &&
                         Spells.Q.IsReady() && Extension.GetCheckBoxValue(ComboMenu, "Combo.QOnlyStun") &&
                         Extension.GetCheckBoxValue(ComboMenu, "Combo.Q") && Extension.QBuffcount(qtarget) >= 3)
                {
                    Spells.Q.Cast(qtarget);
                }
            }
            if (wswallowt != null)
            {
                if (Extension.GetCheckBoxValue(ComboMenu, "Combo.W.Enemy") && Extension.Canswallow(wswallowt) &&
                    Spells.WSwallow.IsReady() && !Extension.Swallowed && !Extension.IsCC(wswallowt))
                {
                    Spells.WSwallow.Cast(wswallowt);
                }
            }
            if (wsplitt != null)
            {
                if (Spells.WSpit.IsReady() && Extension.GetCheckBoxValue(ComboMenu,"Combo.W.Minion"))
                {
                    if (!Extension.Swallowed && closestminion != null)
                    {
                        Spells.WSwallow.Cast(closestminion);
                    }
                    if(Extension.Swallowed)
                    {
                        Spells.WSpit.Cast(wsplitt);
                    }
                }
            }

        }
    }
}
