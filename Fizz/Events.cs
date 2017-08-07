using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EloBuddy;
using EloBuddy.SDK;
using KappAIO_Reborn.Common.Utility;
using SharpDX;
using EloBuddy.SDK.Rendering;

namespace Fizz
{
    class Events
    {
        public static Dictionary<AIHeroClient, int> W3x = new Dictionary<AIHeroClient, int>();
        public static Vector3 Possbeforeharass;
        public static void Ini()
        {
            Obj_AI_Base.OnBuffGain += Obj_AI_Base_OnBuffGain;
            Obj_AI_Base.OnBuffLose += Obj_AI_Base_OnBuffLose;
            Obj_AI_Base.OnBasicAttack += Obj_AI_Base_OnBasicAttack;
            Obj_AI_Base.OnProcessSpellCast += Obj_AI_Base_OnProcessSpellCast;
            Drawing.OnDraw += Drawing_OnDraw;
            Game.OnTick += Game_OnTick;
        }

        private static void Game_OnTick(EventArgs args)
        {
            if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Combo))
            {
                Orbwalk.Combo();
            }
            if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Harass))
            {
                Orbwalk.Harass();
            }
            if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.LaneClear))
            {
                Orbwalk.LaneClear();
            }
            if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.JungleClear))
            {
                Orbwalk.JungleClear();
            }
        }

        private static void Drawing_OnDraw(EventArgs args)
        {
            if (Meniu.MenuDraw.CheckBoxValue("drawq"))
                Circle.Draw(Spells.Q.IsReady() ? Color.Blue : Color.Red,
                    Spells.Q.Range, 3F, Player.Instance.Position);

            if (Meniu.MenuDraw.CheckBoxValue("draww"))
                Circle.Draw(Spells.W.IsReady() ? Color.Purple : Color.Red,
                    Spells.W.Range, 3F, Player.Instance.Position);

            if (Meniu.MenuDraw.CheckBoxValue("drawe"))
                Circle.Draw(Spells.E.IsReady() ? Color.SeaGreen : Color.Red,
                    Spells.E.Range, 3F, Player.Instance.Position);

            if (Meniu.MenuDraw.CheckBoxValue("drawr"))
                Circle.Draw(Spells.R.IsReady() ? Color.Yellow : Color.Red,
                    Spells.R.Range, 3F, Player.Instance.Position);

            DamageIndicator.HealthbarEnabled = Meniu.MenuMisc.CheckBoxValue("damage.hp");
            DamageIndicator.PercentEnabled = Meniu.MenuMisc.CheckBoxValue("damage.percent");
        }

        private static void Obj_AI_Base_OnProcessSpellCast(Obj_AI_Base sender, GameObjectProcessSpellCastEventArgs args)
        {
            if (sender.IsMe && args.SData.Name == "FizzQ")
            {
                Possbeforeharass = Player.Instance.Position;
            }
        }

        private static void Obj_AI_Base_OnBasicAttack(Obj_AI_Base sender, GameObjectProcessSpellCastEventArgs args)
        {
            if (sender is Obj_AI_Turret && args.Target.IsMe && Spells.E.IsReady())
            {
                if (KappAIO_Reborn.Common.Utility.Extensions.CheckBoxValue(Meniu.MenuMisc,"e.turret"))
                {
                    Spells.E.Cast(Player.Instance.Position.Extend(Game.CursorPos, Spells.E.Range - 1).To3DWorld());
                    Core.DelayAction(() =>
                    {
                        Spells.E.Cast(Player.Instance.Position.Extend(Game.CursorPos, Spells.E.Range - 1).To3DWorld());
                    }, (365 - Game.Ping));
                }
            }
        }

        private static void Obj_AI_Base_OnBuffLose(Obj_AI_Base sender, Obj_AI_BaseBuffLoseEventArgs args)
        {
            var hero = sender as AIHeroClient;
            if (hero == null || !args.Buff.Name.Equals("fizzwdot")) return;
            if (W3x.ContainsKey(hero))
            {
                W3x.Remove(hero);
            }
        }

        private static void Obj_AI_Base_OnBuffGain(Obj_AI_Base sender, Obj_AI_BaseBuffGainEventArgs args)
        {
            if (args.Buff.Name == "FizzW")
            {
                Orbwalker.ResetAutoAttack();
            }
            var hero = sender as AIHeroClient;
            if (hero == null || !args.Buff.Name.Equals("fizzwdot")) return;
            if (!W3x.ContainsKey(hero))
            {
                W3x.Add(hero, 0);
            }
        }
    }
}
