using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Events;
using EloBuddy.SDK.Enumerations;
using System;
using System.Linq;

namespace lux.logic
{
    class Permalogic
    {
        public static void AutoQIfEnemyImmobile(Obj_AI_Base target)
        {
            if (Extension.IsEnemyImmobile(target) && Spells.Q.GetPrediction(target).HitChancePercent >= Extension.GetSliderValue(Meniu.Prediction,"q.prediction") )
            {
                Spells.Q.Cast(target);
            }
        }
        public static void AutoEIfEnemyImmobile(Obj_AI_Base target)
        {
            if (Extension.IsEnemyImmobile(target) && Spells.E.GetPrediction(target).HitChancePercent >= Extension.GetSliderValue(Meniu.Prediction, "e.prediction") && Extension.ECheck() != 2)
            {
                Spells.E.Cast(target);
            }
        }
        public static void AutoRIfEnemyKillable(Obj_AI_Base target)
        {
            if (Spells.R.GetPrediction(target).HitChancePercent >= Extension.GetSliderValue(Meniu.Prediction, "r.prediction") && Spells.GetDamage(target, SpellSlot.R) >= Prediction.Health.GetPrediction(target, Spells.R.CastDelay + Game.Ping))
            {
                Spells.R.Cast(target);
            }
        }
    }
}
