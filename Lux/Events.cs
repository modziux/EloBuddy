using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EloBuddy;
using EloBuddy.SDK;
using SharpDX;
using EloBuddy.SDK.Rendering;

namespace lux
{
    class Events
    {
        public static Vector3 Eposition = Vector3.Zero;
        public static Text Text;
        public static void Drawing_OnDraw(EventArgs args)
        {
            if (Spells.Q.IsLearned && Extension.GetCheckBoxValue(Meniu.Drawing, "draw.q"))
            {
                Circle.Draw(Color.MediumAquamarine, Spells.Q.Range, ObjectManager.Player.ServerPosition);
            }
            if (Spells.E.IsLearned && Extension.GetCheckBoxValue(Meniu.Drawing, "draw.e"))
            {
                Circle.Draw(Color.GreenYellow, Spells.E.Range, ObjectManager.Player.ServerPosition);
            }
            if (Extension.GetCheckBoxValue(Meniu.Drawing, "draw.r.a"))
            {
                foreach (var enemy in EntityManager.Heroes.Enemies.Where(x => x.IsHPBarRendered && Prediction.Health.GetPrediction(x, Spells.R.CastDelay + Game.Ping) <= Spells.GetDamage(x, SpellSlot.R)))
                {
                    Text = new Text("", new System.Drawing.Font("calibri", 16, System.Drawing.FontStyle.Regular));
                    string txt = enemy.ChampionName + " " + "Is Killable with R";
                    Text.X = 950;
                    Text.Y = 650;
                    Text.TextValue = txt;
                    Text.Draw();
                }
            }
            DamageIndicator.HealthbarEnabled = Extension.GetCheckBoxValue(Meniu.Drawing, "indicator");
            DamageIndicator.PercentEnabled = Extension.GetCheckBoxValue(Meniu.Drawing, "percent.indicator");

        }
        public static void Drawing_OnEndScene(EventArgs args)
        {
            if (Spells.R.IsLearned && Extension.GetCheckBoxValue(Meniu.Drawing, "draw.r"))
            {
                Extension.DrawCricleMinimap(System.Drawing.Color.AliceBlue, Spells.R.Range, Player.Instance.ServerPosition, 2, 100);
            }

        }
        public static void OnUpdate(GameObject obj, EventArgs args)
        {
            var missile = obj as MissileClient;
            if (missile != null &&
                missile.SpellCaster != null &&
                missile.SpellCaster.IsEnemy &&
                missile.SpellCaster.Type == GameObjectType.AIHeroClient &&
                logic.Wlogic.ProjectileList.Contains(missile))
            {
                logic.Wlogic.ProjectileList.Remove(missile);
                logic.Wlogic.ProjectileList.Add(missile);
            }
        }

        public static void OnCreate(GameObject obj, EventArgs args)
        {
            var missile = obj as MissileClient;
            if (missile != null &&
                missile.SpellCaster != null &&
                missile.SpellCaster.IsEnemy &&
                missile.SpellCaster.Type == GameObjectType.AIHeroClient)
                logic.Wlogic.ProjectileList.Add(missile);
        }

        public static void OnDelete(GameObject obj, EventArgs args)
        {
            if (obj == null)
                return;

            var missile = obj as MissileClient;
            if (missile != null &&
                missile.SpellCaster != null &&
                missile.SpellCaster.IsEnemy &&
                missile.SpellCaster.Type == GameObjectType.AIHeroClient &&
                logic.Wlogic.ProjectileList.Contains(missile))
            {
                logic.Wlogic.ProjectileList.Remove(missile);
            }
        }
        public static void Game_OnTick(EventArgs args)
        {
            logic.Wlogic.TryToW();
            logic.junglesteal.ini();
            logic.Elogic.LogicE();
            if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Combo))
            {
                Lux.combo();
            }
            if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Harass))
            {
                Lux.harass();
            }
            if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.LaneClear))
            {
                Lux.laneclear();
            }
            if(Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.JungleClear))
            {
                Lux.jungleclear();
            }
            if (Spells.Ignite != null && !ObjectManager.Player.IsDead && Extension.GetCheckBoxValue(Meniu.Misc, "use.ignite") && Spells.Ignite.IsReady())
            {
                var target = TargetSelector.GetTarget(Spells.Ignite.Range, DamageType.True);
                if (target != null && target.IsValid())
                {
                    if (target.Health <= ObjectManager.Player.GetSummonerSpellDamage(target, DamageLibrary.SummonerSpells.Ignite))
                    {
                        Spells.Ignite.Cast(target);
                    }
                }
            }
            if (!ObjectManager.Player.IsDead && Extension.GetCheckBoxValue(Meniu.Misc,"auto.q") && Spells.Q.IsReady())
            {
                var target = TargetSelector.GetTarget(Spells.Q.Range, DamageType.Magical);
                if (target != null && target.IsValid())
                {
                    Spells.Q.CastIfItWillHit(2,Extension.GetSliderValue(Meniu.Prediction, "q.prediction"));
                }
            }
            if (!ObjectManager.Player.IsDead && Extension.GetCheckBoxValue(Meniu.Misc, "auto.q.imo") && Spells.Q.IsReady())
            {
                foreach (var enemy in EntityManager.Enemies.Where(x => x.IsValidTarget(Spells.Q.Range) && !x.IsDead && !x.IsZombie && !x.IsMinion && !x.IsMonster))
                {
                    logic.Permalogic.AutoQIfEnemyImmobile(enemy);
                }

            }
            if (!ObjectManager.Player.IsDead && Spells.E.IsReady() && Extension.ECheck() != 2)
            {
                Spells.E.CastIfItWillHit(Extension.GetSliderValue(Meniu.Misc, "auto.e.min"),Extension.GetSliderValue(Meniu.Prediction, "e.prediction"));
            }
            if (!ObjectManager.Player.IsDead && Extension.GetCheckBoxValue(Meniu.Misc, "auto.e.imo") && Spells.E.IsReady() && Extension.ECheck() != 2)
            {
                foreach (var enemy in EntityManager.Enemies.Where(x => x.IsValidTarget(Spells.E.Range) && !x.IsDead && !x.IsZombie && !x.IsMinion && !x.IsMonster))
                {
                    logic.Permalogic.AutoEIfEnemyImmobile(enemy);
                }
            }
            if (!ObjectManager.Player.IsDead && Extension.GetCheckBoxValue(Meniu.Misc, "auto.r") && Spells.R.IsReady())
            {
                foreach (var enemy in EntityManager.Enemies.Where(x => x.IsValidTarget(Spells.R.Range) && !x.IsDead && !x.IsZombie && !x.IsMinion && !x.IsMonster))
                {
                    logic.Permalogic.AutoRIfEnemyKillable(enemy);
                }
            }
        }
        public static void  Obj_AI_Base_OnProcessSpellCast(Obj_AI_Base sender, GameObjectProcessSpellCastEventArgs args)
        {
                if (sender.IsMe && args.SData.Name == "LuxLightStrikeKugel")
                {
                   Eposition = args.End;
                }

            }
    }
}
