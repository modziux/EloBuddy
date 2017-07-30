using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EloBuddy.SDK;
using EloBuddy;

namespace Lissandra.Logic
{
    class Wlogic
    {
        public static void Castw()
        {
            if (!Spells.W.IsReady())
            {
                return;
            }
            var Winrange = Player.Instance.CountEnemyChampionsInRange(Spells.W.Range);
            if (Winrange >= Extension.GetSliderValue(Meniu.Combo, "combo.min.w") && Extension.GetCheckBoxValue(Meniu.Combo,"combo.w"))
            {
                Spells.W.Cast();
            }
        }
    }
}
