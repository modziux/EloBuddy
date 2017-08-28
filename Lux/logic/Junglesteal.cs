using System.Linq;

namespace lux.logic
{
    class junglesteal
    {
        public static void ini()
        {
            foreach (var mob in Extension.SupportedJungleMobs.Where(m => Meniu.Junglesteal.GetCheckBoxValue(m.BaseSkinName) && Spells.R.IsReady() && Spells.R.WillKill(m)))
            {
                Spells.R.Cast(mob);
            }
        }
    }
}
