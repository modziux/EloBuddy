using System;
using System.Collections.Generic;
using System.Linq;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Enumerations;
using EloBuddy.SDK.Events;
using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;
using EloBuddy.SDK.Rendering;
using SharpDX;

namespace lux
{
    class Program
    {
        public static void Main(string[] args)
        {
            Loading.OnLoadingComplete += Loading_OnLoadingComplete;
        }

        private static void Loading_OnLoadingComplete(EventArgs args)
        {
            if (ObjectManager.Player.ChampionName == "Lux")
            {
                Meniu.ini();
                Chat.Print("Candy Crush Lux By Modziux loaded Good Luck !", System.Drawing.Color.HotPink);
                Spells.Ini();
                Game.OnTick += Events.Game_OnTick;
                GameObject.OnCreate += Events.OnCreate;
                GameObject.OnDelete += Events.OnDelete;
                Obj_AI_Base.OnUpdatePosition += Events.OnUpdate;
                Obj_AI_Base.OnProcessSpellCast += Events.Obj_AI_Base_OnProcessSpellCast;
                Drawing.OnEndScene += Events.Drawing_OnEndScene;
                Drawing.OnDraw += Events.Drawing_OnDraw;
            }

        }
    }
}