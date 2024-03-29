using RimWorld;

namespace BotFactory
{
    [DefOf]
    public static class BF_ThoughtDefOf
    {
        static BF_ThoughtDefOf()
        {
            DefOfHelper.EnsureInitializedInCtor(typeof(BF_ThoughtDefOf));
        }

        public static ThoughtDef BF_ConnectedSkyMindAttacked;

        public static ThoughtDef BF_AttackedViaSkyMind;

        public static ThoughtDef BF_TrolledViaSkyMind;

        public static ThoughtDef BF_SurrogateMentalBreak;


        public static ThoughtDef BF_PersonalityShiftAllowed;

        public static ThoughtDef BF_PersonalityShiftDenied;
    }
}
