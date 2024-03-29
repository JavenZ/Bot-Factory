using Verse;
using RimWorld;

namespace BotFactory
{
    [DefOf]
    public static class BF_PawnKindDefOf
    {
        static BF_PawnKindDefOf()
        {
            DefOfHelper.EnsureInitializedInCtor(typeof(BF_PawnKindDefOf));
        }

        public static PawnKindDef BF_FractalAbomination;

        public static PawnKindDef BF_MicroScyther;

        public static PawnKindDef BF_FractalWitness;
    }
}