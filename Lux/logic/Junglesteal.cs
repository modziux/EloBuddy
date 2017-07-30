using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lux.logic
{
    class junglesteal
    {
        public static void ini()
        {
            foreach (var mob in Extension.SupportedJungleMobs.Where(m => Extension.GetCheckBoxValue(Meniu.Junglesteal,m.BaseSkinName) && Spells.R.IsReady() && Extension.WillKill(Spells.R,m)))
            {
                Spells.R.Cast(mob);
            }
        }
    }
}
