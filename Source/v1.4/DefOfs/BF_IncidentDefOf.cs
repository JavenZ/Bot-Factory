using RimWorld;

namespace BotFactory
{
    [DefOf]
    public static class BF_IncidentDefOf
    {
        static BF_IncidentDefOf()
        {
            DefOfHelper.EnsureInitializedInCtor(typeof(BF_IncidentDefOf));
        }
        public static IncidentDef BF_HackingIncident;

        public static IncidentDef ResourcePodCrash;

        public static IncidentDef RefugeePodCrash;

        public static IncidentDef PsychicEmanatorShipPartCrash;

        public static IncidentDef DefoliatorShipPartCrash;
    }
}
