using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lissandra
{
    static class Spells
    {
        public static Spell.Skillshot Q, Q2, E;
        public static Spell.Active W;
        public static Spell.Targeted R;

    public static void ini()
        {

            Q = new Spell.Skillshot(SpellSlot.Q, 715, SkillShotType.Linear, 250, 2200, 75);
            Q2 = new Spell.Skillshot(SpellSlot.Q, 825, SkillShotType.Linear, 250, 2200, 90);
            W = new Spell.Active(SpellSlot.W, 425);
            E = new Spell.Skillshot(SpellSlot.E, 1000, SkillShotType.Linear, 250, 850, 125);
            R = new Spell.Targeted(SpellSlot.R, 400);
            DamageIndicator.Initialize(GetTotalDamage);
        }
        public static float GetDamage(this Obj_AI_Base target, SpellSlot slot)
        {
            const DamageType damageType = DamageType.Magical;
            var AD = Player.Instance.FlatPhysicalDamageMod;
            var AP = Player.Instance.FlatMagicDamageMod;
            var sLevel = Player.GetSpell(slot).Level - 1;

            var dmg = 0f;

            switch (slot)
            {
                case SpellSlot.Q:
                    if (Q.IsReady())
                    {
                        dmg += new float[] { 70, 100, 130, 160, 190 }[sLevel] + 0.65f * AP;
                    }
                    break;
                case SpellSlot.W:
                    if (W.IsReady())
                    {
                        dmg += new float[] { 70, 110, 150, 190, 230 }[sLevel] + 0.4f * AP;
                    }
                    break;
                case SpellSlot.E:
                    if (E.IsReady())
                    {
                        dmg += new float[] { 70, 115, 160, 205, 250 }[sLevel] + 0.6f * AP;
                    }
                    break;
                case SpellSlot.R:
                    if (R.IsReady())
                    {
                        dmg += new float[] { 150, 250, 350 }[sLevel] + 0.7f * AP;
                    }
                    break;
            }
            return Player.Instance.CalculateDamageOnUnit(target, damageType, dmg - 10);
        }
        public static float GetTotalDamage(this Obj_AI_Base target)
        {
            var dmg =
                Player.Spells.Where(
                    s => (s.Slot == SpellSlot.Q) || (s.Slot == SpellSlot.W) || (s.Slot == SpellSlot.E) || (s.Slot == SpellSlot.R) && s.IsReady)
                    .Sum(s => target.GetDamage(s.Slot));

            return dmg + Player.Instance.GetAutoAttackDamage(target);
        }
    }
    
}
