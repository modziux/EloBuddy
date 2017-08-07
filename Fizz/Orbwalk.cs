using EloBuddy;
using EloBuddy.SDK;
using SharpDX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KappAIO_Reborn.Common.Utility;

namespace Fizz
{
    class Orbwalk
    {
        public static void Combo()
        {
            var target = TargetSelector.GetTarget(Spells.R.Range, DamageType.Magical);
            var use_r = (Spells.R.IsReady() && Meniu.MenuCombo.CheckBoxValue("Combo.Q.Use"));
            var use_e = (Spells.E.IsReady() && Meniu.MenuCombo.CheckBoxValue("Combo.E.Use"));
            var use_w = (Spells.W.IsReady() && Meniu.MenuCombo.CheckBoxValue("Combo.W.Use"));
            var use_q = (Spells.Q.IsReady() && Meniu.MenuCombo.CheckBoxValue("Combo.Q.Use"));
            if (target == null) return;
            if (use_q && Spells.Q.IsInRange(target))
            {
                Spells.Q.Cast(target);
            }
            if (Events.W3x.ContainsKey(target))
            {
                Events.W3x[target]++;
            }
            if (Events.W3x.ContainsKey(target) && Events.W3x[target] >= Game.TicksPerSecond * 2 && Meniu.MenuCombo.CheckBoxValue("Combo.Ww.Use") && use_w)
            {
                Spells.W.Cast();
                Orbwalker.ResetAutoAttack();
            }
            else if (use_w && Spells.W.IsInRange(target) && !Meniu.MenuCombo.CheckBoxValue("Combo.Ww.Use"))
            {
                Spells.W.Cast();
            }
            var prediction = Prediction.Position.PredictUnitPosition(target, 750).Distance(Player.Instance.Position)
                             <= (Spells.E.Range + 200 );
            if (Spells.E.Name == "FizzE" && use_e && !Spells.Q.IsReady() && prediction)
            {
                var castPos = Player.Instance.Distance(Prediction.Position.PredictUnitPosition(target, 750)) > Spells.E.Range
                                  ? Player.Instance.Position.Extend(
                                      Prediction.Position.PredictUnitPosition(target, 750),
                                      Spells.E.Range).To3DWorld()
                                  : target.Position;

                Spells.E.Cast(castPos);

                var pred2 = Prediction.Position.PredictUnitPosition(target, 750).Distance(Player.Instance.Position)
                            <= (200 + target.BoundingRadius);

                if (pred2)
                {
                    Player.IssueOrder(
                            GameObjectOrder.MoveTo,
                            Prediction.Position.PredictUnitPosition(target, 750).To3DWorld());
                }
            }
            switch(Meniu.combomode)
            {
                case 0:
                    if (use_r && Spells.R.IsInRange(target))
                    {
                        var rpred = Spells.R.GetPrediction(target);
                        if (rpred.HitChance == EloBuddy.SDK.Enumerations.HitChance.High)
                        {
                            Spells.R.Cast(rpred.CastPosition);
                        }
                    }
                    break;
                case 1:
                    if (use_r && Spells.R.IsInRange(target))
                    {
                        var rpred = Spells.R.GetPrediction(target);
                        if (rpred.HitChance == EloBuddy.SDK.Enumerations.HitChance.High && Spells.GetDamage(target,SpellSlot.R) >= target.Health)
                        {
                            Spells.R.Cast(rpred.CastPosition);
                        }
                    }
                        break;

            }

        }
        public static void Harass()
        {
            switch(Meniu.harassmode)
            { 
                case 1:
                var Safetarget = TargetSelector.GetTarget(Spells.Q.Range, DamageType.Magical);
            if (Safetarget == null) return;

            if (Spells.W.IsReady() &&  Spells.Q.IsReady() && Spells.E.IsReady())
            {
                Spells.W.Cast();
                Spells.Q.Cast(Safetarget);
            }

                    if (Spells.E.IsReady() && Events.Possbeforeharass != null && !Spells.W.IsReady() && !Spells.Q.IsReady())
                    {
                        Spells.E.Cast(Player.Instance.Position.Extend(Events.Possbeforeharass, Spells.E.Range).To3DWorld());

                        Core.DelayAction(() =>
                        {
                            Spells.E.Cast(Player.Instance.Position.Extend(Events.Possbeforeharass, Spells.E.Range).To3DWorld());
                        }, (365 - Game.Ping));
                    }
                    break;
                case 0:
                    var Agressivetarget = TargetSelector.GetTarget(Spells.Q.Range, DamageType.Magical);
                    if (Agressivetarget == null) return;
                    if (Spells.W.IsReady() && Spells.Q.IsReady() && Spells.E.IsReady())
                    {
                        Spells.Q.Cast(Agressivetarget);
                    }
                    if (Spells.E.IsReady() && !Player.Instance.HasBuff("FizzEIcon"))
                    {
                        Spells.E.Cast(Agressivetarget);
                    }
                    if (Events.W3x.ContainsKey(Agressivetarget))
                    {
                        Events.W3x[Agressivetarget]++;
                    }
                    if (Events.W3x.ContainsKey(Agressivetarget) && Events.W3x[Agressivetarget] >= Game.TicksPerSecond * 2  && Spells.W.IsReady())
                    {
                        Spells.W.Cast();
                    }
                    break;
                case 3:
                    var wtarget = TargetSelector.GetTarget(Spells.W.Range, DamageType.Mixed);
                    if (Events.W3x.ContainsKey(wtarget))
                    {
                        Events.W3x[wtarget]++;
                    }
                    if (Events.W3x.ContainsKey(wtarget) && Events.W3x[wtarget] >= Game.TicksPerSecond * 2 && Spells.W.IsReady())
                    {
                        Spells.W.Cast();
                    }
                    break;




            }
        }
        public static void LaneClear()
        {
            if (Meniu.MenuLane.CheckBoxValue("Lane.W.Use") && Spells.W.IsReady() && Player.Instance.ManaPercent > Meniu.MenuLane.SliderValue("Lane.W.Mana"))
            {
                var minion =
                    EntityManager.MinionsAndMonsters.EnemyMinions.FirstOrDefault(x => Spells.W.IsInRange(x));
                if (minion == null) return;
                if (minion.Health <= Spells.GetDamage(minion,SpellSlot.W)  && Spells.W.IsReady())
                {
                    Spells.W.Cast();
                    Orbwalker.ForcedTarget = minion;
                }
            }
            if (Meniu.MenuLane.CheckBoxValue("Lane.Q.Use") && Spells.Q.IsReady() && Player.Instance.ManaPercent >= Meniu.MenuLane.SliderValue("Lane.Q.Mana"))
            {
                var minion =
                    EntityManager.MinionsAndMonsters.EnemyMinions.FirstOrDefault(m => Spells.Q.IsInRange(m));
                if (minion == null) return;

                if (Spells.Q.IsInRange(minion) && minion.Health <= Spells.GetDamage(minion,SpellSlot.Q)) Spells.Q.Cast(minion);

            }
            if (Meniu.MenuLane.CheckBoxValue("Lane.E.Use") && Spells.E.IsReady() && Player.Instance.ManaPercent >= Meniu.MenuLane.SliderValue("Lane.E.Mana"))
            {
                var minions =
                    EntityManager.MinionsAndMonsters.GetLaneMinions()
                        .Where(m => m.IsValidTarget(Spells.E.Range))
                        .ToArray();
                if (minions.Length == 0) return;

                if (Spells.E.Name == "FizzE")
                {
                    var castPos =
                        Prediction.Position.PredictCircularMissileAoe(
                                minions,
                                Spells.E.Range,
                                Spells.E.Width,
                                Spells.E.CastDelay,
                                Spells.E.Speed)
                            .OrderByDescending(r => r.GetCollisionObjects<Obj_AI_Minion>().Length)
                            .FirstOrDefault();

                    if (castPos != null)
                    {
                        var predictMinion = castPos.GetCollisionObjects<Obj_AI_Minion>();

                        if (predictMinion.Length >= Meniu.MenuLane.SliderValue("lcUseEMinion"))
                        {
                            //var castPos = E.GetPrediction(target).CastPosition;
                            Spells.E.Cast(castPos.CastPosition);

                            Player.IssueOrder(GameObjectOrder.MoveTo, castPos.CastPosition);
                        }
                    }
                }
            }
    }
        public static void JungleClear()
        {
            var mob = EntityManager.MinionsAndMonsters.Monsters.FirstOrDefault(m => Spells.Q.IsInRange(m));

            if (mob == null) return;
            if (Meniu.MenuJungle.CheckBoxValue("Jungle.Q.Use") && Spells.Q.IsReady() && Spells.Q.IsInRange(mob)
                && Player.Instance.ManaPercent >= Meniu.MenuJungle.SliderValue("Jungle.Q.Mana"))
            {
                Spells.Q.Cast(mob);
            }

            if (Meniu.MenuJungle.CheckBoxValue("Jungle.W.Use") && Spells.W.IsReady() && Spells.W.IsInRange(mob)
                && Player.Instance.ManaPercent >= Meniu.MenuJungle.SliderValue("Jungle.W.Mana"))
            {
                if (mob.Health <= Spells.GetDamage(mob,SpellSlot.W))
                {
                    Spells.W.Cast(mob);
                    Orbwalker.ForcedTarget = mob;
                }
            
            }

            if (Meniu.MenuJungle.CheckBoxValue("Jungle.E.Use") && Spells.E.IsReady() && mob.IsValidTarget(Spells.E.Range)
                && Player.Instance.ManaPercent >= Meniu.MenuJungle.SliderValue("Jungle.E.Mana"))
            {
                if (Spells.E.IsInRange(mob) && Spells.E.Name == "FizzE")
                {
                    var castPos = Player.Instance.Distance(Prediction.Position.PredictUnitPosition(mob, 1))
                                  > Spells.E.Range
                                      ? Player.Instance.Position.Extend(
                                          Prediction.Position.PredictUnitPosition(mob, 1),
                                          Spells.E.Range).To3DWorld()
                                      : mob.Position;

                    //var castPos = E.GetPrediction(target).CastPosition;
                    Spells.E.Cast(castPos);

                    var pred2 = Prediction.Position.PredictUnitPosition(mob, 1).Distance(Player.Instance.Position)
                                <= (200 + 330 + mob.BoundingRadius);

                    if (pred2)
                    {
                        Player.IssueOrder(
                            GameObjectOrder.MoveTo,
                            Prediction.Position.PredictUnitPosition(mob, 1).To3DWorld());
                        Orbwalker.DisableMovement = false;
                    }
                    else Spells.E.Cast(Prediction.Position.PredictUnitPosition(mob, 1).To3DWorld());
                }
            }
        }
    }
    }

