using EloBuddy;
using EloBuddy.SDK;
using Lux;

namespace lux.logic
{
    class Rlogic
    {
        public static void FinisherR(Obj_AI_Base target)
        {
            if (target.GetDamage(SpellSlot.R) >= Prediction.Health.GetPrediction(target,Spells.R.CastDelay + Game.Ping) && Prediction.Health.GetPrediction(target,Spells.R.CastDelay + Game.Ping) >= 0)
            {;
                var predpos = pred.PredEx(target, 1f);
                Spells.R.Cast(predpos);
            }
        }
        public static void MultiR(Obj_AI_Base target)
        {
            Spells.R.CastIfItWillHit(Meniu.Combo.GetSliderValue("combo.r.min"),Meniu.Prediction.GetSliderValue("r.prediction"));
        }
        public static void Rchoise(Obj_AI_Base target)
        {
            switch (Meniu.Combo.GetComboboxValue("combo.r.logic"))
            {
                case 0:
                    FinisherR(target);
                    break;
                case 1:
                    MultiR(target);
                    break;
                case 2:
                    FinisherR(target);
                    MultiR(target);
                    break;
            }
        }
    }
}
