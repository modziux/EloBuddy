using EloBuddy;
using EloBuddy.SDK;
using Lux;

namespace lux.logic
{
    class Permalogic
    {
        public static void AutoQIfEnemyImmobile(Obj_AI_Base target)
        {
            if (Extension.IsEnemyImmobile(target) && Spells.Q.GetPrediction(target).HitChancePercent >= Meniu.Prediction.GetSliderValue("q.prediction") )
            {
                Spells.Q.Cast(target);
            }
        }
        public static void AutoEIfEnemyImmobile(Obj_AI_Base target)
        {
            if (Extension.IsEnemyImmobile(target) && Spells.E.GetPrediction(target).HitChancePercent >= Meniu.Prediction.GetSliderValue("e.prediction") && Extension.ECheck() != 2)
            {
                Spells.E.Cast(target);
            }
        }
        public static void AutoRIfEnemyKillable(Obj_AI_Base target)
        {
            if ( target.GetDamage(SpellSlot.R) >= Prediction.Health.GetPrediction(target, Spells.R.CastDelay + Game.Ping) && Prediction.Health.GetPrediction(target, Spells.R.CastDelay + Game.Ping) >= 0)
            {
                var predpos = pred.PredEx(target, 1f);
                Spells.R.Cast(predpos);
            }
        }
    }
}
