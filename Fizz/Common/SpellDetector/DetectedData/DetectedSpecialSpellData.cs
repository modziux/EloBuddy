﻿using EloBuddy;
using EloBuddy.SDK;
using KappAIO_Reborn.Common.Databases.SpellData;
using SharpDX;

namespace KappAIO_Reborn.Common.SpellDetector.DetectedData
{
    public class DetectedSpecialSpellData
    {
        public AIHeroClient Caster;
        public Obj_AI_Base Target;
        public GameObject Object;
        public Vector3 Position;
        public SpecialSpellData Data;
        public float StartTick;
        public float EndTick => this.StartTick + this.Data.CastDelay;
        public float TicksLeft => this.EndTick - Core.GameTickCount;
        public float TicksPassed => this.StartTick - Core.GameTickCount;
        public bool Ended => Core.GameTickCount - this.EndTick > 0;
        public bool IsEnemy => this.Caster != null && this.Caster.IsEnemy;

        public float GetSpellDamage(Obj_AI_Base target)
        {
            return !target.IsValidTarget() ? 0 : this.Caster.GetSpellDamage(target, this.Data.Slot);
        }

        public bool WillHit(Obj_AI_Base target)
        {
            return target.Distance(this.Position) <= this.Data.Range || this.Target != null && this.Target.IdEquals(target);
        }
    }
}
