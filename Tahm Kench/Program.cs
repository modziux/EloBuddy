using System;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Events;
using EloBuddy.SDK.Menu.Values;
using EloBuddy.SDK.Rendering;
using SharpDX;
using Color = System.Drawing.Color;

namespace Tamh_kench
{
    class Program
    {
        static void Main(string[] args)
        {
            Loading.OnLoadingComplete += Loading_OnLoadingComplete;
        }

        private static void Drawing_OnDraw(EventArgs args)
        {
            int[] Rrange = { 4500, 5500, 6500 };
            if (Extension.GetCheckBoxValue(Tahm_menu.DrawMenu, "Draw.Q"))
            {
                new Circle() {Color = Color.Orange, BorderWidth = 1, Radius = Spells.Q.Range}.Draw(
                    Player.Instance.Position);
            }
            if (Extension.GetCheckBoxValue(Tahm_menu.DrawMenu, "Draw.W"))
            {
                new Circle() {Color = Color.Brown, BorderWidth = 1, Radius = Spells.WSwallow.Range}.Draw(
                    Player.Instance.Position);
                new Circle() {Color = Color.Blue, BorderWidth = 1, Radius = Spells.WSpit.Range}.Draw(
                    Player.Instance.Position);
            }
            if (Extension.GetCheckBoxValue(Tahm_menu.DrawMenu, "Draw.R")&& Spells.R.IsLearned)
            {
                new Circle() { Color = Color.AliceBlue, BorderWidth = 1, Radius = Rrange[Spells.R.Level]}.Draw(
                    Player.Instance.Position);
            }
        }

        private static void Game_OnTick(EventArgs args)
        {
            Perma.Permactive();
            if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Combo))
            {
                combo.Combo();
            }
            if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Harass))
            {
                Harrass.Harras();
            }
        }

        private static void Loading_OnLoadingComplete(EventArgs args)
        {
            if (Player.Instance.ChampionName != "TahmKench")
            {
                return;
            }
            Game.OnTick += Game_OnTick;
            Drawing.OnDraw += Drawing_OnDraw;
            Spells.init();
            Tahm_menu.Ini();

        }

    }

}
