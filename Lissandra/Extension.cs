using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;
using EloBuddy;

namespace Lissandra
{
    static class Extension
    {
        public static void createcheckbox(EloBuddy.SDK.Menu.Menu name, string pavadinimas, string kamatys)
        {
            name.Add(pavadinimas, new CheckBox(kamatys));
        }
        public static void createslider(Menu name, string pavadinimas, string kamatys, int df, int min, int max)
        {
            name.Add(pavadinimas, new Slider(kamatys, df, min, max));
        }
        public static bool GetCheckBoxValue(this Menu m, string uniqueId)
        {
            return m.Get<CheckBox>(uniqueId).CurrentValue;
        }
        public static int GetSliderValue(this Menu m, string uniqueId)
        {
            return m.Get<Slider>(uniqueId).CurrentValue;
        }
        public static int GetComboboxValue(this Menu m, string uniqueId)
        {
            return m.Get<ComboBox>(uniqueId).CurrentValue;
        }
        public static bool IsEnemyImmobile(Obj_AI_Base target)
        {
            if (target.HasBuffOfType(BuffType.Stun) || target.HasBuffOfType(BuffType.Snare) ||
                target.HasBuffOfType(BuffType.Knockup) ||
                target.HasBuffOfType(BuffType.Charm) || target.HasBuffOfType(BuffType.Fear) ||
                target.HasBuffOfType(BuffType.Knockback) ||
                target.HasBuffOfType(BuffType.Taunt) || target.HasBuffOfType(BuffType.Suppression) ||
                target.IsStunned || !target.CanMove)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
