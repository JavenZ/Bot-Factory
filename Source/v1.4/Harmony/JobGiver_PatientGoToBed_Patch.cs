using Verse;
using Verse.AI;
using HarmonyLib;
using RimWorld;

namespace BotFactory
{
    internal class JobGiver_PatientGoToBed_Patch
    {
        // Override job for being a patient based on whether the pawn can charge and the target bed is charge-capable.
        [HarmonyPatch(typeof(JobGiver_PatientGoToBed), "TryGiveJob")]
        public class TryGiveJob_Patch
        {
            [HarmonyPostfix]
            public static void Listener(Pawn pawn, ref Job __result)
            {
                if (__result == null || __result.targetA.Thing.TryGetComp<CompPowerTrader>() == null || !Utils.CanUseBattery(pawn))
                    return;

                __result = JobMaker.MakeJob(BF_JobDefOf.BF_RechargeBattery, __result.targetA.Thing);
            }
        }
    }
}