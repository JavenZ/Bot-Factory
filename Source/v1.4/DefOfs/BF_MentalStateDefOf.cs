using RimWorld;
using Verse;

namespace BotFactory
{
    [DefOf]
    public static class BF_MentalStateDefOf
    {
        static BF_MentalStateDefOf()
        {
            DefOfHelper.EnsureInitializedInCtor(typeof(BF_MentalStateDefOf));
        }
        public static MentalStateDef BF_MentalState_Exterminator;
    }
}