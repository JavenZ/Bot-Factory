using Verse;
using RimWorld;

namespace BotFactory
{
    [DefOf]
    public static class BF_LetterDefOf
    {
        static BF_LetterDefOf()
        {
            DefOfHelper.EnsureInitializedInCtor(typeof(BF_LetterDefOf));
        }

        public static LetterDef BF_PersonalityShiftLetter;

        public static LetterDef BF_PersonalityShiftRequestLetter;
    }
}
