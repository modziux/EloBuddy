using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Events;
using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;
using EloBuddy.SDK.Utils;
using KappAIO_Reborn.Common.Databases.Spells;
using KappAIO_Reborn.Common.SpellDetector.DetectedData;
using KappAIO_Reborn.Common.SpellDetector.Detectors;
using KappAIO_Reborn.Common.SpellDetector.Events;
using KappAIO_Reborn.Common.Utility;
using SharpDX;
using System.Linq;
using Color = System.Drawing.Color;

namespace Fizz
{
    class SpellBlocker
    {
        public static EnabledSpell[] EnabledSpells = { };

        private static float _lastBlock;

        private static float delay => Spells.E.CastDelay + (Game.Ping * 0.75f);

        public static void Init()
        {
            OnSkillShotUpdate.OnUpdate += OnSkillShotUpdate_OnUpdate;
            OnTargetedSpellUpdate.OnUpdate += OnTargetedSpellUpdate_OnUpdate; ;
            OnEmpoweredAttackUpdate.OnUpdate += OnEmpoweredAttackUpdate_OnUpdate;
            OnDangerBuffUpdate.OnUpdate += OnDangerBuffUpdate_OnUpdate;
            OnSpecialSpellUpdate.OnUpdate += OnSpecialSpellUpdate_OnUpdate;
        }

        private static void OnSpecialSpellUpdate_OnUpdate(DetectedSpecialSpellData args)
        {
            if (!args.IsEnemy || !Spells.E.IsReady() || !args.WillHit(Player.Instance))
                return;

            var spellname = args.Data.MenuItemName;
            var spell = EnabledSpells.FirstOrDefault(s => s.SpellName.Equals(spellname));
            if (spell == null)
            {
                Logger.Warn($"{spellname} Not valid Spell");
                return;
            }

            if (!spell.Enabled && (!Meniu.BlockExecute || Player.Instance.PredictHealth(args.TicksLeft) > args.GetSpellDamage(Player.Instance)))
            {
                Logger.Warn($"{spellname} Not Enabled from Menu");
                return;
            }

            Block(args, spell);
        }

        private static void OnDangerBuffUpdate_OnUpdate(DetectedDangerBuffData args)
        {
            if (args.Caster == null || !Spells.E.IsReady() || !args.Caster.IsEnemy || !args.WillHit(Player.Instance))
                return;

            var spellname = args.Data.MenuItemName;
            var spell = EnabledSpells.FirstOrDefault(s => s.SpellName.Equals(spellname));

            if (spell == null)
            {
                Logger.Warn($"{spellname} Not valid Spell");
                return;
            }

            if (!spell.Enabled && (!Meniu.BlockExecute || Player.Instance.PredictHealth(args.TicksLeft) > args.GetSpellDamage(Player.Instance)))
            {
                Logger.Warn($"{spellname} Not Enabled from Menu");
                return;
            }

            Block(args, spell);
        }

        private static void OnEmpoweredAttackUpdate_OnUpdate(DetectedEmpoweredAttackData args)
        {
            if (args.Caster == null || !Spells.E.IsReady() || !args.Caster.IsEnemy || !args.WillHit(Player.Instance))
                return;

            var spellname = args.Data.MenuItemName;
            var spell = EnabledSpells.FirstOrDefault(s => s.SpellName.Equals(spellname));

            if (spell == null)
            {
                Logger.Warn($"{spellname} Not valid Spell");
                return;
            }

            if (!spell.Enabled && (!Meniu.BlockExecute || Player.Instance.PredictHealth(args.TicksLeft) > args.GetSpellDamage(Player.Instance)))
            {
                Logger.Warn($"{spellname} Not Enabled from Menu");
                return;
            }

            Block(args, spell);
        }

        private static void OnTargetedSpellUpdate_OnUpdate(DetectedTargetedSpellData args)
        {
            if (args.Caster == null || !Spells.E.IsReady() || !args.Caster.IsEnemy || !args.Target.IsMe || !args.WillHit(Player.Instance))
                return;

            var spellname = args.Data.MenuItemName;
            var spell = EnabledSpells.FirstOrDefault(s => s.SpellName.Equals(spellname));

            if (spell == null)
            {
                Logger.Warn($"{spellname} Not valid Spell");
                return;
            }

            if (!spell.Enabled && (!Meniu.BlockExecute || Player.Instance.PredictHealth(args.TicksLeft) > args.GetSpellDamage(Player.Instance)))
            {
                Logger.Warn($"{spellname} Not Enabled from Menu");
                return;
            }

            Block(args, spell);
        }

        private static void OnSkillShotUpdate_OnUpdate(DetectedSkillshotData args)
        {
            if (args.Caster == null || !Spells.E.IsReady() || !args.IsEnemy || !args.WillHit(Player.Instance))
                return;

            var spellname = args.Data.MenuItemName;
            var spell = EnabledSpells.FirstOrDefault(s => s.SpellName.Equals(spellname));

            if (spell == null)
            {
                Logger.Warn($"{spellname} Not valid Spell");
                return;
            }

            if (!spell.Enabled && (!Meniu.BlockExecute || Player.Instance.PredictHealth(args.TravelTime(Player.Instance)) > args.GetSpellDamage(Player.Instance)))
            {
                Logger.Warn($"{spellname} Not Enabled from Menu");
                return;
            }

            Block(args, spell);
        }

        public static bool Block(object BlockSpell, EnabledSpell menuItem)
        {
            if (!Spells.E.IsReady())
                return false;

            var skillshot = BlockSpell as DetectedSkillshotData;
            if (skillshot != null && Meniu.BlockSkillshots && (menuItem.FastEvade || skillshot.TravelTime(Player.Instance) <= delay))
            {
                var dashInfo = Player.Instance.GetDashInfo();
                if (dashInfo != null)
                {
                    var travelTime = (Player.Instance.ServerPosition.Distance(dashInfo.EndPos) / 20) * 1000f;
                    var predPos = Player.Instance.PrediectPosition(travelTime);
                    var skillshotpred = Player.Instance.PrediectPosition(skillshot.TravelTime(predPos.To2D()));
                    if (Player.Instance.BoundingRadius > predPos.Distance(skillshotpred))
                    {
                        return CastW(skillshot.Caster, skillshot.Data.MenuItemName, skillshot.Start.To3D());
                    }
                }
                else
                {
                    return CastW(skillshot.Caster, skillshot.Data.MenuItemName, skillshot.Start.To3D());
                }
            }

            var buff = BlockSpell as DetectedDangerBuffData;
            if (buff != null && Meniu.BlockBuff && buff.TicksLeft <= delay)
                return CastW(buff.Caster, buff.Data.MenuItemName);

            var targeted = BlockSpell as DetectedTargetedSpellData;
            if (targeted != null && Meniu.BlockTargeted && (menuItem.FastEvade || targeted.TicksLeft <= delay))
                return CastW(targeted.Caster, targeted.Data.MenuItemName, targeted.Start);

            var autoAttack = BlockSpell as DetectedEmpoweredAttackData;
            if (autoAttack != null && Meniu.BlockAA && autoAttack.TicksLeft <= delay)
                return CastW(autoAttack.Caster, autoAttack.Data.MenuItemName, autoAttack.Start);

            var specialSpell = BlockSpell as DetectedSpecialSpellData;
            if (specialSpell != null && Meniu.BlockSpecial && (menuItem.FastEvade || specialSpell.TicksLeft <= delay))
                return CastW(specialSpell.Caster, specialSpell.Data.MenuItemName);

            return false;
        }

        private static bool CastW(Obj_AI_Base caster, string spellname = "", Vector3 startPos = new Vector3())
        {

            if (!Spells.E.IsReady())
                return false;

            var wtarget =
                TargetSelector.SelectedTarget != null && TargetSelector.SelectedTarget.IsKillable(-1, true) && Spells.E.IsInRange(Spells.E.GetPrediction(TargetSelector.SelectedTarget).CastPosition)
                ? TargetSelector.SelectedTarget
                : Spells.E.GetTarget().IsKillable(-1, true) && Spells.E.IsInRange(Spells.E.GetPrediction(Spells.E.GetTarget()).CastPosition)
                ? Spells.E.GetTarget()
                : caster;

            var castpos = wtarget.IsKillable(-1, true) && Spells.E.IsInRange(Spells.E.GetPrediction(wtarget).CastPosition)
                ? Spells.E.GetPrediction(wtarget).CastPosition : startPos != new Vector3() && startPos.IsValid() && Spells.E.IsInRange(startPos) ? startPos : Game.CursorPos;

            _lastBlock = Core.GameTickCount;
            Logger.Info($"BLOCK {spellname}");
            return Spells.E.Cast(castpos);
        }

        public class EnabledSpell
        {
            public EnabledSpell(string spellname)
            {
                this.SpellName = spellname;
            }

            public string SpellName;
            public bool Enabled { get { return Meniu.MenuE.CheckBoxValue($"enable{SpellName}"); } }
            public bool FastEvade { get { return Meniu.MenuE.CheckBoxValue($"fast{this.SpellName}"); } }
            public int DangerLevel { get { return Meniu.MenuE.SliderValue($"danger{this.SpellName}"); } }
        }
    }

}

