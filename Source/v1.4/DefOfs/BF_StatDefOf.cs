using RimWorld;

namespace BotFactory
{
    [DefOf]
    public static class BF_StatDefOf
    {
        static BF_StatDefOf()
        {
            DefOfHelper.EnsureInitializedInCtor(typeof(BF_StatDefOf));
        }

        public static StatDef BF_MechanicalSurgerySuccessChance;

        public static StatDef BF_MechanicalTendQuality;

        public static StatDef BF_MechanicalTendSpeed;

        public static StatDef BF_MechanicalTendQualityOffset;

        public static StatDef BF_MechanicalSurgerySuccessChanceFactor;

        public static StatDef BF_MaintenanceRetention;

        public static StatDef BF_SurrogateLimitBonus;
    }
}