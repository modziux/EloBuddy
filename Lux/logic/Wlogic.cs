using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EloBuddy.SDK.Constants;
using EloBuddy.SDK.Rendering;
using SharpDX;
using EloBuddy;
using EloBuddy.SDK;
using UnsignedEvade;

namespace lux.logic
{
    class Wlogic
    {
        public static List<MissileClient> ProjectileList = new List<MissileClient>();
        public static List<SpellInfo> EnemyProjectileInformation = new List<SpellInfo>();
        public static void TryToW()
        //credit to Chaos for this logic if about to be hit!
        {
            if (Spells.W.IsReady() && Spells.W.IsLearned)
                foreach (MissileClient missile in ProjectileList)
                    foreach (SpellInfo info in EnemyProjectileInformation)
                        foreach (var client in EntityManager.Heroes.Allies)
                        {
                            if (ShouldShield(missile, info, client) && CollisionCheck(missile, info, client))
                            {
                                if (info.ChannelType == SpellDatabase.ChannelType.None && Spells.W.IsReady()
                                    && Extension.GetCheckBoxValue(Meniu.Shield,client.ChampionName))

                                {
                                    Spells.W.Cast(client.ServerPosition);
                                }
                                else if (info.ChannelType != SpellDatabase.ChannelType.None && Spells.W.IsReady()
                                    && Extension.GetCheckBoxValue(Meniu.Shield, client.ChampionName))
                                    Spells.W.Cast(client.ServerPosition);
                            }
                        }
        }





        public static bool ShouldShield(MissileClient missile, SpellInfo info, AIHeroClient client)
        {


            if (missile.SpellCaster.Name != "Diana")
                if (missile.SData.Name != info.MissileName ||
                    !missile.IsInRange(client, 800))
                    return false;


            if (info.ProjectileType == SpellDatabase.ProjectileType.LockOnProjectile
                && missile.Target != client)
                return false;



            return true;
        }



        public static bool CollisionCheck(MissileClient missile, SpellInfo info, AIHeroClient client)
        {
            bool variable = Prediction.Position.Collision.LinearMissileCollision(
                client, missile.StartPosition.To2D(), missile.StartPosition.To2D().Extend(missile.EndPosition, info.Range),
                info.MissileSpeed, info.Width, info.Delay);
            return variable;
        }
    }
}

