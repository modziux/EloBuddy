using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Enumerations;

namespace lux.logic
{
    class Qlogic
    {
        public static Prediction.Manager.PredictionInput Qpred = new Prediction.Manager.PredictionInput
        {
            Delay = 0.25f,
            Radius = Spells.Q.Width,
            Range = Spells.Q.Range,
            Speed = Spells.Q.Speed,
            Type = SkillShotType.Linear,
            From = Player.Instance.Position,
            CollisionTypes = { EloBuddy.SDK.Spells.CollisionType.AiHeroClient,
                               EloBuddy.SDK.Spells.CollisionType.ObjAiMinion,
                               EloBuddy.SDK.Spells.CollisionType.YasuoWall }
        };
        public static void simpleQ(Obj_AI_Base target)
        {
            if (target != null)
            {
                Qpred.Target = target;
                var qpredict = Prediction.Manager.GetPrediction(Qpred);
                if (qpredict.HitChancePercent >= Extension.GetSliderValue(Meniu.Prediction, "q.prediction"))
                {
                    Spells.Q.Cast(qpredict.CastPosition);
                }
            }
        }

    }
}
