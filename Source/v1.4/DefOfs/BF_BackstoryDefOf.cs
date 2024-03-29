using RimWorld;

namespace BotFactory
{
    [DefOf]
    public static class BF_BackstoryDefOf
    {
        static BF_BackstoryDefOf()
        {
            DefOfHelper.EnsureInitializedInCtor(typeof(BF_BackstoryDefOf));
        }

        public static BackstoryDef BF_MechChildhoodFreshBlank;
        public static BackstoryDef BF_MechAdulthoodBlank;

        public static BackstoryDef BF_MechChildhoodDrone;
        public static BackstoryDef BF_MechAdulthoodDrone;

        public static BackstoryDef BF_NewbootChildhood;
    }
}