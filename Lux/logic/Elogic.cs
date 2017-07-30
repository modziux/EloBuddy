using EloBuddy;
using EloBuddy.SDK;
using SharpDX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lux.logic
{
    class Elogic
    {

        public static void LogicE()
        {
            if (Player.HasBuff("LuxLightStrikeKugel"))
            {
                int eBig = Events.Eposition.CountEnemyChampionsInRange(350);
                if (Extension.GetCheckBoxValue(Meniu.Combo, "e.slow"))
                {
                    int detonate = eBig - Events.Eposition.CountEnemyChampionsInRange(160);
                    if (detonate > 0 || eBig > 1)
                        Spells.E.Cast(Game.CursorPos);
                }
                else if (Extension.GetCheckBoxValue(Meniu.Combo, "e.detonate"))
                {
                    if (eBig > 0)
                        Spells.E.Cast(Game.CursorPos);
                }
                else
                {
                    Spells.E.Cast(Game.CursorPos);
                }
            }
        }
    }
}