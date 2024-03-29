using Verse;
using RimWorld;

namespace BotFactory
{
    [DefOf]
    public static class BF_HediffDefOf
    {
        static BF_HediffDefOf()
        {
            DefOfHelper.EnsureInitializedInCtor(typeof(BF_HediffDefOf));
        }
        public static HediffDef BF_FractalPillOrganic;

        public static HediffDef BF_StasisPill;

        public static HediffDef BF_RemainingCharge;

        public static HediffDef BF_RecoveringFromDDOS;

        public static HediffDef BF_ShortReboot;

        public static HediffDef BF_LongReboot;

        public static HediffDef BF_MemoryCorruption;

        public static HediffDef BF_AutomodulatedVoiceSynthesizer;

        public static HediffDef BF_CoerciveVoiceSynthesizer;

        // SkyMind related Hediffs

        public static HediffDef BF_SplitConsciousness;

        public static HediffDef BF_ForeignConsciousness;

        public static HediffDef BF_MindOperation;

        public static HediffDef BF_IsolatedCore;

        public static HediffDef BF_AutonomousCore;

        public static HediffDef BF_ReceiverCore;

        public static HediffDef BF_SkyMindReceiver;

        public static HediffDef BF_SkyMindTransceiver;

        public static HediffDef BF_NoController;

        // Maintenance Part failures

        public static HediffDef BF_PartDecay;

        public static HediffDef BF_RustedPart;

        public static HediffDef BF_PowerLoss;

        public static HediffDef BF_DamagedCore;

        public static HediffDef BF_FailingCoolantValves;

        public static HediffDef BF_RogueMechanites;

        // Maintenance effects

        public static HediffDef BF_MaintenanceCritical;

        public static HediffDef BF_MaintenancePoor;

        public static HediffDef BF_MaintenanceSatisfactory;

    }
}
