using EloBuddy;
using EloBuddy.SDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lissandra.Logic
{
    class Elogic
    {
        public static void ComboE (Obj_AI_Base target)
        {
            if (Spells.E.IsReady() && !Player.Instance.HasBuff("LissandraE") && Extension.GetCheckBoxValue(Meniu.Combo,"combo.e") && Spells.E.IsInRange(target))
            {
                // Naudoja E Viena karta
                var pred = Spells.E.GetPrediction(target);
                Spells.E.Cast(pred.CastPosition);
            }
            if (Events.lissE != null && Events.lissE.Position.CountEnemyChampionsInRange(Spells.W.Range - 50) >= Extension.GetSliderValue(Meniu.Combo, "combo.ew") && Spells.W.IsReady())
            {
                // Naudoja E tik kai W ir hit x zmoniu
                Spells.E.Cast(Game.CursorPos);
                Spells.W.Cast();
            }
            if (Extension.GetCheckBoxValue(Meniu.Combo,"combo.e2") && Events.lissE != null && Events.lissE.Position.IsInRange(Events.lissE.EndPosition, 50))
            {
                // Full combo
                Spells.E.Cast(Game.CursorPos);
            }
        }
        public static void flee()
        {

            if (Events.lissE == null)
            {
                Spells.E.Cast(Game.CursorPos);
            }

            if (Events.lissE != null && Events.lissE.Position.IsInRange(Events.lissE.EndPosition, 50))
            {
                Spells.E.Cast(Game.CursorPos);
            }
        }
        public static void harass(Obj_AI_Base target)
        {
            if (Spells.E.IsReady() && !Player.Instance.HasBuff("LissandraE") && Extension.GetCheckBoxValue(Meniu.Harass, "harass.e") && Spells.E.IsInRange(target))
            {
                // Naudoja E Viena karta
                var pred = Spells.E.GetPrediction(target);
                Spells.E.Cast(pred.CastPosition);
            }
        }
    }
}
