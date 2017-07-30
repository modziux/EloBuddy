using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Enumerations;

namespace Tamh_kench
{
    class Spells
    {
        public static Spell.Skillshot Q;
        public static Spell.Targeted WSwallow;
        public static Spell.Skillshot WSpit;
        public static Spell.Active E;
        public static Spell.Active R;



        public static void init()
        {
            Q = new Spell.Skillshot(SpellSlot.Q, 800, SkillShotType.Linear, 100, 2000, 75);
            {
                Q.AllowedCollisionCount = 0;
            }

            WSwallow = new Spell.Targeted(SpellSlot.W, 250);
            WSpit = new Spell.Skillshot(SpellSlot.W, 900, SkillShotType.Linear, 100, 900, 75);
            {
                WSpit.AllowedCollisionCount = 0;
            }
            E = new Spell.Active(SpellSlot.E);
            R = new Spell.Active(SpellSlot.R,6500,DamageType.Physical);


        }
    }
}
