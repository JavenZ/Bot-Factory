using RimWorld;

namespace BotFactory
{
    [DefOf]
    public static class BF_HistoryEventDefOf
    {
        static BF_HistoryEventDefOf()
        {
            DefOfHelper.EnsureInitializedInCtor(typeof(BF_HistoryEventDefOf));
        }

        public static HistoryEventDef BF_PossessesOrganicColonist;

        public static HistoryEventDef BF_PossessesMechanicalColonist;
    }
}