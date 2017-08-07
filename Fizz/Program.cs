using EloBuddy;
using EloBuddy.SDK.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fizz
{
    class Program
    {
        static void Main(string[] args)
        {
            Loading.OnLoadingComplete += Loading_OnLoadingComplete;
        }

        private static void Loading_OnLoadingComplete(EventArgs args)
        {
            if (Player.Instance.ChampionName == "Fizz")
            {
                Chat.Print("Fizz Loaded have Fun :)", System.Drawing.Color.Aqua);
                Spells.Ini();
                Meniu.Ini();
                SpellBlocker.Init();
                Events.Ini();
            }
        }
    }
}
