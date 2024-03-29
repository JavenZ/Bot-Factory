using Verse;
using RimWorld;

namespace BotFactory
{
    [DefOf]
    public static class BF_WorkTypeDefOf
    {
        static BF_WorkTypeDefOf()
        {
            DefOfHelper.EnsureInitializedInCtor(typeof(BF_WorkTypeDefOf));
        }

        public static WorkTypeDef BF_Mechanic;
    }
}