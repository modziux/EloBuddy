using EloBuddy;
using EloBuddy.SDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fiddlestick
{
    class Spells
    {
        public static Spell.Targeted Q, E;
        public static Spell.Chargeable W, R;

        public static void Ini()
        {
            Q = new Spell.Targeted(SpellSlot.Q, 575);
            W = new Spell.Chargeable(SpellSlot.W,0,575,5000,0,null,null,DamageType.Magical);
            E = new Spell.Targeted(SpellSlot.E, 750, DamageType.Magical);
            R = new Spell.Chargeable(SpellSlot.R, 0, 800, 1500, 0, int.MaxValue, 600, DamageType.Magical);
        }
    }
}
