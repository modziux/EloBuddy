using EloBuddy;
using EloBuddy.SDK.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fiddlestick
{
    class Program
    {
        static void Main(string[] args)
        {
            Loading.OnLoadingComplete += Loading_OnLoadingComplete;
        }

        private static void Loading_OnLoadingComplete(EventArgs args)
        {
            Chat.Print(ObjectManager.Player.ChampionName);
            if(ObjectManager.Player.ChampionName == "FiddleSticks")
            {
                Meniu.Ini();
                Spells.Ini();
                Events.Ini();
            }
        }
    }
}
