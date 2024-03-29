using RimWorld;
using Verse;

namespace BotFactory
{
    [DefOf]
    public static class BF_BodyPartDefOf
    {
        static BF_BodyPartDefOf()
        {
            DefOfHelper.EnsureInitializedInCtor(typeof(BF_BodyPartDefOf));
        }

        public static BodyPartDef BF_InternalCorePump;

        public static BodyPartDef BF_MechaniteStorage;
    }
}