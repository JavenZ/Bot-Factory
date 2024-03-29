using Verse;
using RimWorld;

namespace BotFactory
{
    [DefOf]
    public static class BF_RulePackDefOf
    {
        static BF_RulePackDefOf()
        {
            DefOfHelper.EnsureInitializedInCtor(typeof(BF_RulePackDefOf));
        }

        public static RulePackDef BF_AndroidNoneNames;

        public static RulePackDef BF_DroneNoneNames;
    }
}
