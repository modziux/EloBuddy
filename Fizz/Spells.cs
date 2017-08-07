using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fizz
{
    public static class Spells
    {
        public static Spell.Targeted Q;
        public static Spell.Active W;
        public static Spell.Skillshot E, R;

        public static void Ini()
        {
            Q = new Spell.Targeted(SpellSlot.Q, 550);
            W = new Spell.Active(SpellSlot.W, (uint)Player.Instance.GetAutoAttackRange());
            E = new Spell.Skillshot(SpellSlot.E, 400, SkillShotType.Circular, 250, int.MaxValue, 330);
            R = new Spell.Skillshot(SpellSlot.R, 1300, SkillShotType.Linear, 250, 1200, 80)
            {
                AllowedCollisionCount = int.MaxValue,
            };
            E.AllowedCollisionCount = int.MaxValue;
            DamageIndicator.Initialize(GetTotalDamage);
        }
        public static float GetDamage(this Obj_AI_Base target, SpellSlot slot)
        {
            const DamageType damageType = DamageType.Magical;
            var AD = Player.Instance.TotalAttackDamage;
            var AP = Player.Instance.TotalMagicalDamage;
            var sLevel = Player.GetSpell(slot).Level - 1;

            var dmg = 0f;

            switch (slot)
            {
                case SpellSlot.Q:
                    if (Q.IsReady())
                    {
                        dmg += new float[] { 10, 25, 40, 55, 70 }[sLevel] + 0.55f * AP + AD;
                    }
                    break;
                case SpellSlot.W:
                    if (W.IsReady())
                    {
                        dmg += new float[] { 20, 30, 40, 50, 60 }[sLevel] + 0.4f * AP + AD;
                    }
                    break;
                case SpellSlot.E:
                    if (E.IsReady())
                    {
                        dmg += new float[] { 70, 120, 170, 220, 270 }[sLevel] + 0.75f * AP;
                    }
                    break;
                case SpellSlot.R:
                    if (R.IsReady())
                    {
                        if (target.Distance(Player.Instance.Position) < 455)
                        {
                            dmg += new float[] { 150, 250, 350 }[sLevel] + 0.6f * AP;
                        }
                        else if (target.Distance(Player.Instance.Position) > 455 && target.Distance(Player.Instance.Position) < 910)
                        {
                            dmg += new float[] { 225, 325, 425 }[sLevel] + 0.8f * AP;
                        }
                        else if ( target.Distance(Player.Instance.Position) > 910)
                        {
                            dmg += new float[] { 300, 400, 500 }[sLevel] + 1.2f * AP;
                        }
                    }
                    break;
            }
            return Player.Instance.CalculateDamageOnUnit(target, damageType, dmg);
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
