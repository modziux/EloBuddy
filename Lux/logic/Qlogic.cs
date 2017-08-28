using System.Linq;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Rendering;
using Lux;
using SharpDX;
using Color = System.Drawing.Color;

namespace lux.logic
{
    class Qlogic
    {
      
        public static void simpleQ(Obj_AI_Base target)
        {
            if (target != null)
            {
                var delay = 0.25f + Player.Instance.Position.Distance(target) / Spells.Q.Speed;
                var predpos = pred.PredEx(target, delay);
                var rect = new Geometry.Polygon.Rectangle(Player.Instance.Position,predpos,Spells.Q.Width);
                var collcount = ObjectManager.Get<Obj_AI_Base>().Where(x => rect.IsInside(x) && x.IsEnemy && !x.IsDead).Count();

                if (collcount <= 1)
                {
                    Spells.Q.Cast(predpos);
                }
            }
        }

    }
}
