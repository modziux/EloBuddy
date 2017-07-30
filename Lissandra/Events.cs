using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Events;
using EloBuddy.SDK.Rendering;
using System;
using SharpDX;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lissandra
{
    class Events
    {
        public static MissileClient lissE;
        public static void ini()
        {
            GameObject.OnCreate += GameObject_OnCreate;
            GameObject.OnDelete += GameObject_OnDelete;
            Game.OnTick += Game_OnTick;
            Interrupter.OnInterruptableSpell += Interrupter_OnInterruptableSpell;
            Gapcloser.OnGapcloser += Gapcloser_OnGapcloser;
            Obj_AI_Base.OnBasicAttack += Obj_AI_Base_OnBasicAttack;
            Obj_AI_Base.OnProcessSpellCast += Obj_AI_Base_OnProcessSpellCast;
            Drawing.OnDraw += Drawing_OnDraw;
        }

        private static void Drawing_OnDraw(EventArgs args)
        {
            if (Spells.Q.IsLearned && Extension.GetCheckBoxValue(Meniu.Drawing,"draw.q"))
            {
                Circle.Draw(Color.AntiqueWhite, Spells.Q.Range,ObjectManager.Player.ServerPosition);
            }
            if (Spells.W.IsLearned && Extension.GetCheckBoxValue(Meniu.Drawing, "draw.w"))
            {
                Circle.Draw(Color.Chocolate, Spells.W.Range, ObjectManager.Player.ServerPosition);
            }
            if (Spells.E.IsLearned && Extension.GetCheckBoxValue(Meniu.Drawing, "draw.e"))
            {
                Circle.Draw(Color.OrangeRed, Spells.E.Range, ObjectManager.Player.ServerPosition);
            }
            if (Spells.R.IsLearned && Extension.GetCheckBoxValue(Meniu.Drawing, "draw.r"))
            {
                Circle.Draw(Color.LightCoral, Spells.R.Range, ObjectManager.Player.ServerPosition);
            }
            DamageIndicator.HealthbarEnabled = Extension.GetCheckBoxValue(Meniu.Drawing, "draw.damage");
            DamageIndicator.PercentEnabled = Extension.GetCheckBoxValue(Meniu.Drawing, "draw.percent");
        }

        private static void Obj_AI_Base_OnProcessSpellCast(Obj_AI_Base sender, GameObjectProcessSpellCastEventArgs args)
        {
            if (!(args.Target is AIHeroClient))
            {
                return;
            }

            var caster = sender;
            var target = (AIHeroClient)args.Target;

            if ((!(caster is AIHeroClient) && !(caster is Obj_AI_Turret)) || caster == null || target == null)
            {
                return;
            }
            if (target.IsMe)
            {
                var shp = Extension.GetSliderValue(Meniu.Misc, "misc.r.min");
                var useRS = Extension.GetCheckBoxValue(Meniu.Misc, "misc.r.me") && Spells.R.IsReady();
                if (sender == null || sender.IsAlly || sender.IsMe)
                {
                    return;
                }

                if (sender.IsEnemy || sender is Obj_AI_Turret)
                {
                    if (useRS && Player.Instance.HealthPercent <= shp && !Player.HasBuff("kindredrnodeathbuff") && !Player.HasBuff("JudicatorIntervention")
                        && !Player.HasBuff("ChronoShift") && !Player.HasBuff("UndyingRage"))
                    {
                       Spells.R.Cast(Player.Instance);
                    }
                }
            }
        }

        private static void Obj_AI_Base_OnBasicAttack(Obj_AI_Base sender, GameObjectProcessSpellCastEventArgs args)
        {
            {
                if (!(args.Target is AIHeroClient))
                {
                    return;
                }

                var caster = sender;
                var target = (AIHeroClient)args.Target;

                if ((!(caster is AIHeroClient) && !(caster is Obj_AI_Turret)) || caster == null || target == null)
                {
                    return;
                }

                if (target.IsMe)
                {
                var shp = Extension.GetSliderValue(Meniu.Misc, "misc.r.min");
                var useRS = Extension.GetCheckBoxValue(Meniu.Misc, "misc.r.me") && Spells.R.IsReady();
                    if (sender == null || sender.IsAlly || sender.IsMe)
                    {
                        return;
                    }

                    if (sender.IsEnemy || sender is Obj_AI_Turret)
                    {
                        if (useRS && Player.Instance.HealthPercent <= shp && !Player.HasBuff("kindredrnodeathbuff") && !Player.HasBuff("JudicatorIntervention")
                            && !Player.HasBuff("ChronoShift") && !Player.HasBuff("UndyingRage"))
                        {
                            Spells.R.Cast(Player.Instance);
                        }
                    }
                }
            }
        }

        private static void Gapcloser_OnGapcloser(AIHeroClient sender, Gapcloser.GapcloserEventArgs e)
        {
            if (sender == null || sender.IsAlly || sender.IsMe)
            {
                return;
            }

            if (Spells.W.IsReady() && sender.IsValidTarget(Spells.W.Range - 15) && Extension.GetCheckBoxValue(Meniu.Misc,"gapcloser.w"))
            {
                Spells.W.Cast();
                return;
            }

            if (Spells.R.IsReady() && sender.IsValidTarget(Spells.R.Range) && Extension.GetCheckBoxValue(Meniu.Misc,"gapcloser.r"))
            {
                Spells.R.Cast(sender);
            }
        }

        private static void Interrupter_OnInterruptableSpell(Obj_AI_Base sender, Interrupter.InterruptableSpellEventArgs e)
        {
            if (!Extension.GetCheckBoxValue(Meniu.Misc, "interupter") || sender == null || sender.IsAlly || sender.IsMe || !sender.IsEnemy)
            {
                return;
            }

            if (Spells.R.IsReady() && sender.IsValidTarget(Spells.R.Range))
            {
                Spells.R.Cast(sender);
            }
        }

        private static void Game_OnTick(EventArgs args)
        {
            Logic.Perma.ini();
            if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Combo))
            {
                Orbwalk.combo();
            }
            if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Harass))
            {
                Orbwalk.harass();
            }
            if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.LaneClear))
            {
                Orbwalk.laneclear();
            }
            if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.JungleClear))
            {
                Orbwalk.jungleclear();
            }
            if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Flee))
            {
                Orbwalk.flee();
            }

        }

        private static void GameObject_OnDelete(GameObject sender, EventArgs args)
        {
            var miss = sender as MissileClient;
            if (miss == null || !miss.IsValid || !miss.SpellCaster.IsMe)
            {
                return;
            }
            if (miss.SpellCaster.IsMe && miss.SData.Name == "LissandraEMissile")
            {
                lissE = null;
            }

        }

        private static void GameObject_OnCreate(GameObject sender, EventArgs args)
        {
            
            var miss = sender as MissileClient;
            if (miss != null && miss.IsValid && miss.SpellCaster.IsMe)
            {
                if (miss.SpellCaster.IsMe && miss.SData.Name == "LissandraEMissile")
                {
                    lissE = miss;
                }
            }
        }
    }
    }
