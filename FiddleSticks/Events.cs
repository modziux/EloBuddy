using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Events;
using EloBuddy.SDK.Rendering;
using SharpDX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fiddlestick
{
    class Events
    {
        public static void Ini()
        {
            Game.OnTick += Game_OnTick;
            Obj_AI_Base.OnProcessSpellCast += Obj_AI_Base_OnProcessSpellCast;
            Obj_AI_Base.OnBuffLose += Obj_AI_Base_OnBuffLose;
            Interrupter.OnInterruptableSpell += Interrupter_OnInterruptableSpell;
            Drawing.OnDraw += Drawing_OnDraw;

        }

        private static void Drawing_OnDraw(EventArgs args)
        {
            if (Spells.Q.IsLearned && Extension.GetCheckBoxValue(Meniu.Drawings, "draw.q"))
            {
                Circle.Draw(Color.MediumAquamarine, Spells.Q.Range, ObjectManager.Player.ServerPosition);
            }
            if (Spells.W.IsLearned && Extension.GetCheckBoxValue(Meniu.Drawings, "draw.w"))
            {
                Circle.Draw(Color.GreenYellow, 575, ObjectManager.Player.ServerPosition);
            }
            if (Spells.E.IsLearned && Extension.GetCheckBoxValue(Meniu.Drawings, "draw.e"))
            {
                Circle.Draw(Color.MediumAquamarine, Spells.E.Range, ObjectManager.Player.ServerPosition);
            }
            if (Spells.R.IsLearned && Extension.GetCheckBoxValue(Meniu.Drawings, "draw.r"))
            {
                Circle.Draw(Color.GreenYellow, 800, ObjectManager.Player.ServerPosition);
            }
        }

        private static void Interrupter_OnInterruptableSpell(Obj_AI_Base sender, Interrupter.InterruptableSpellEventArgs e)
        {
            if (Spells.Q.IsReady() && Extension.GetCheckBoxValue(Meniu.Q,"q.channel") && sender.IsEnemy && Spells.Q.IsInRange(sender))
            {
                Spells.Q.Cast(sender);
            }
            if (Spells.E.IsReady() && Extension.GetCheckBoxValue(Meniu.E, "e.channel") && sender.IsEnemy && Spells.E.IsInRange(sender))
            {
                Spells.E.Cast(sender);
            }
        }

        private static void Obj_AI_Base_OnProcessSpellCast(Obj_AI_Base sender, GameObjectProcessSpellCastEventArgs args)
        {
            if (sender.IsMe && args.SData.Name == "Drain")
            {
                Orbwalker.DisableAttacking = true;
                Orbwalker.DisableMovement = true;
            }
        }

        private static void Obj_AI_Base_OnBuffLose(Obj_AI_Base sender, Obj_AI_BaseBuffLoseEventArgs args)
        {
            if (args.Buff.Name == "DrainChannel")
            {
                Orbwalker.DisableAttacking = false;
                Orbwalker.DisableMovement = false;
            }
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
    }
}
