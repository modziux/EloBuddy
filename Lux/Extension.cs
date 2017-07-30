using System.Linq;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;
using SharpDX;
using EloBuddy.SDK.Rendering;
using System.Collections.Generic;
using System;

namespace lux
{
    static class Extension
    {
        public static readonly string[] exclusive = { "SRU_Baron", "SRU_RiftHerald", "SRU_Dragon_Fire", "SRU_Dragon_Water", "SRU_Dragon_Earth", "SRU_Dragon_Air", "SRU_Dragon_Elder" };
        public static bool IsEnemyImmobile(Obj_AI_Base target)
        {
            if (target.HasBuffOfType(BuffType.Stun) || target.HasBuffOfType(BuffType.Snare) ||
                target.HasBuffOfType(BuffType.Knockup) ||
                target.HasBuffOfType(BuffType.Charm) || target.HasBuffOfType(BuffType.Fear) ||
                target.HasBuffOfType(BuffType.Knockback) ||
                target.HasBuffOfType(BuffType.Taunt) || target.HasBuffOfType(BuffType.Suppression) ||
                target.IsStunned || !target.CanMove)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        public static int ECheck()
        {
            if (Spells.E.Name == "LuxLightStrikeKugel") 
            {
                return 1;
            }
            else
            {
                return 2;
            }
        }
        public static bool GetCheckBoxValue(this Menu m, string uniqueId)
        {
            return m.Get<CheckBox>(uniqueId).CurrentValue;
        }
        public static int GetSliderValue(this Menu m, string uniqueId)
        {
            return m.Get<Slider>(uniqueId).CurrentValue;
        }
        public static int GetComboboxValue(this Menu m, string uniqueId)
        {
            return m.Get<ComboBox>(uniqueId).CurrentValue;
        }
        public static bool WillKill(this Spell.Skillshot spell, Obj_AI_Base target)
        {
            return Spells.GetDamage(target, spell.Slot) >= Prediction.Health.GetPrediction(target,spell.CastDelay + Game.Ping);
        }
        public static IEnumerable<Obj_AI_Minion> SupportedJungleMobs
        {
            get
            {
                return EntityManager.MinionsAndMonsters.GetJungleMonsters().Where(m => exclusive.Any(j => j.Equals(m.BaseSkinName)));
            }
        }
        public static void DrawCricleMinimap(System.Drawing.Color color, float radius, Vector3 center, int thickness = 5, int quality = 30)
        {
            var pointList = new List<Vector3>();
            for (var i = 0; i < quality; i++)
            {
                var angle = i * Math.PI * 2 / quality;
                pointList.Add(new Vector3(center.X + radius * (float)Math.Cos(angle), center.Y + radius * (float)Math.Sin(angle), center.Z));
            }

            for (var i = 0; i < pointList.Count; i++)
            {
                var a = pointList[i];
                var b = pointList[i == pointList.Count - 1 ? 0 : i + 1];

                var aonScreen = Drawing.WorldToMinimap(a);
                var bonScreen = Drawing.WorldToMinimap(b);

                //Fixed drawing bug
                if (aonScreen.X == 0 || bonScreen.X == 0)
                    continue;

                Drawing.DrawLine(aonScreen.X, aonScreen.Y, bonScreen.X, bonScreen.Y, thickness, color);
            }
        }
    }
}
