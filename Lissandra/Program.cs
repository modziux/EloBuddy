using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Events;

namespace Lissandra
{
    class Program
    {
        static void Main(string[] args)
        {
            Loading.OnLoadingComplete += Loading_OnLoadingComplete;
        }

        private static void Loading_OnLoadingComplete(EventArgs args)
        {
            if (Player.Instance.ChampionName == "Lissandra")
            {
                Chat.Print("Lissandra by modziux Loaded Good luck",System.Drawing.Color.AliceBlue);
                Spells.ini();
                Meniu.Ini();
                Events.ini();
            }
        }
    }
}
