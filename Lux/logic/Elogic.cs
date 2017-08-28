using EloBuddy;
using EloBuddy.SDK;

namespace lux.logic
{
    class Elogic
    {

        public static void LogicE()
        {
            if (Player.HasBuff("LuxLightStrikeKugel"))
            {
                int eBig = Events.Eposition.CountEnemyChampionsInRange(350);
                if (Meniu.Combo.GetCheckBoxValue("e.slow"))
                {
                    int detonate = eBig - Events.Eposition.CountEnemyChampionsInRange(160);
                    if (detonate > 0 || eBig > 1)
                        Spells.E.Cast(Game.CursorPos);
                }
                else if (Meniu.Combo.GetCheckBoxValue("e.detonate"))
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