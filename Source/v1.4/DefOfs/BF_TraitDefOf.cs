using RimWorld;

namespace BotFactory
{
    [DefOf]
        public static class BF_TraitDefOf
        {
            static BF_TraitDefOf()
            {
                DefOfHelper.EnsureInitializedInCtor(typeof(BF_TraitDefOf));
            }

            public static TraitDef BF_FeelingsTowardOrganics;

            public static TraitDef Transhumanist;
        }
}
