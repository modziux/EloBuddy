using EloBuddy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lissandra.Logic
{
    class Rlogic
    {
        public static void CastR(Obj_AI_Base Target)
        {
            if (Target.IsMe && Spells.R.IsReady())
            {
                Spells.R.Cast();
            }
            else if (Spells.R.IsInRange(Target) && Spells.R.IsReady())
            {
                Spells.R.Cast(Target);
            }
        }
    }
}
