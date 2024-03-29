using Verse;
using RimWorld;

namespace BotFactory
{
    [DefOf]
    public static class BF_JobDefOf
    {
        static BF_JobDefOf()
        {
            DefOfHelper.EnsureInitializedInCtor(typeof(BF_JobDefOf));
        }
        public static JobDef BF_RechargeBattery;

        public static JobDef BF_ResurrectMechanical;

        public static JobDef BF_TendMechanical;

        public static JobDef BF_GenerateInsight;

        public static JobDef BF_DoMaintenanceUrgent;

        public static JobDef BF_DoMaintenanceIdle;
    }
}