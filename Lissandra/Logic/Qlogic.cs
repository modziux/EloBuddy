using EloBuddy;
using EloBuddy.SDK;
using SharpDX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lissandra.Logic
{
    class Qlogic
    {
        public static void Qcast()
        {
            if (!Spells.Q.IsReady())
            {
                return;
            }

            var target = TargetSelector.GetTarget(Spells.Q.Range, DamageType.Magical);
            if (target == null)
            {
                return;
            }

            if (target.IsValidTarget(Spells.Q.Range))
            {
                var predq = Spells.Q.GetPrediction(target);
                Spells.Q.Cast(predq.CastPosition);
            }

            var target2 = TargetSelector.GetTarget(Spells.Q2.Range, DamageType.Magical);

            if (target2 == null)
            {
                return;
            }

            var pred = Spells.Q2.GetPrediction(target2);
            var collisions = EntityManager.MinionsAndMonsters.EnemyMinions.Where(it => it.IsValidTarget(Spells.Q.Range)).ToList();

            if (!collisions.Any())
            {
                return;
            }

            foreach (var minion in collisions)
            {
                var poly = new Geometry.Polygon.Rectangle(
                    (Vector2)Player.Instance.ServerPosition,
                    Player.Instance.ServerPosition.Extend(minion.ServerPosition, Spells.Q2.Range),
                    Spells.Q2.Width);

                if (poly.IsInside(pred.UnitPosition))
                {
                    Spells.Q.Cast(minion.Position);
                }
            }
        }
    }
}
