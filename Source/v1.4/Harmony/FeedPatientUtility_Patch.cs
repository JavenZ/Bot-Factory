using Verse;
using HarmonyLib;
using RimWorld;

namespace BotFactory
{
    // There is no reason to try and feed a mechanical unit that is charging.
    internal class FeedPatientUtility_Patch
    {
        [HarmonyPatch(typeof(FeedPatientUtility), "ShouldBeFed")]
        public class ShouldBeFed_Patch
        {
            [HarmonyPostfix]
            public static void Listener(Pawn p, ref bool __result)
            {
                if (__result && Utils.CanUseBattery(p) && p.CurJob.def == BF_JobDefOf.BF_RechargeBattery)
                    __result = false;
            }
        }
    }
}