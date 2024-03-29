using Verse;
using RimWorld;


namespace BotFactory
{
    [DefOf]
    public static class BF_ThingDefOf
    {
        static BF_ThingDefOf()
        {
            DefOfHelper.EnsureInitializedInCtor(typeof(BF_ThingDefOf));
        }

        public static ThingDef BF_BedsideChargerFacility;

        public static ThingDef HospitalBed;

        public static ThingDef BF_FractalPill;

        public static ThingDef BF_MaintenanceSpot;

        public static ThingDef BF_MindOperationAttachedMote;
    }

}