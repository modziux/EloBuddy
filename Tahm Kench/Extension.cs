using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;

namespace Tamh_kench
{
   static class Extension
    {
        public static int QBuffcount(AIHeroClient target)
        {
            return target.GetBuffCount("tahmkenchpdebuffcounter");
        }

        public static bool Canswallow(AIHeroClient target)
        {
            return QBuffcount(target) >= 3;
        }

        public static bool Swallowed
        {
            get { return Player.Instance.HasBuff("tahmkenchwhasdevouredtarget"); }
        }

        public static bool GetCheckBoxValue(this Menu m, string uniqueId)
        {
            return m.Get<CheckBox>(uniqueId).CurrentValue;
        }
        public static int GetSliderValue(this Menu m, string uniqueId)
        {
            return m.Get<Slider>(uniqueId).CurrentValue;
        }
        public static bool IsCC(Obj_AI_Base target)
        {
            return target.GetMovementBlockedDebuffDuration() > 0;
        }
        public static int Distancetraveltime(Obj_AI_Base ally)
        {
            return (int)Player.Instance.Distance(ally) / (int)Player.Instance.MoveSpeed;
        }
    }
}