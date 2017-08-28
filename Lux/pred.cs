using SharpDX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EloBuddy;
using EloBuddy.SDK;

namespace Lux
{
   static class pred
    {
        public static Vector3 PredictedPos;

        public static Vector3 PredEx(Obj_AI_Base player, float delay) //creds to JustinT
        {
            float va = 0f;
            if (player.IsFacing(Player.Instance))
            {
                va = (50f - player.BoundingRadius);
            }
            else
            {
                va = -(100f - player.BoundingRadius);
            }
            var dis = delay * player.MoveSpeed + va;
            var path = player.GetWaypoints();
            for (var i = 0; i < path.Count - 1; i++)
            {
                var a = path[i];
                var b = path[i + 1];
                var d = a.Distance(b);

                if (d < dis)
                {
                    dis -= d;
                }
                else
                {
                    return (a + dis * (b - a).Normalized()).To3D();
                }
            }
            return (path[path.Count - 1]).To3D();
        }
        public static List<Vector2> GetWaypoints(this Obj_AI_Base unit)
        {
            var result = new List<Vector2>();

            if (!unit.IsVisible)
            {
                return result;
            }

            result.Add(unit.ServerPosition.To2D());
            var path = unit.Path;

            if (path.Length <= 0)
            {
                return result;
            }

            for (var i = 1; i < path.Length; i++)
            {
                result.Add(path[i].To2D());
            }

            return result;
        }
    }
}
