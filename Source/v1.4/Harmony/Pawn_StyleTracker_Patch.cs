using Verse;
using HarmonyLib;
using RimWorld;

namespace BotFactory
{
    internal class Pawn_StyleTracker_Patch
    {
        // Drones don't care about style.
        [HarmonyPatch(typeof(Pawn_StyleTracker), "RequestLookChange")]
        public class RequestLookChange_Patch
        {
            [HarmonyPrefix]
            public static bool Listener(Pawn ___pawn)
            {
                if (Utils.IsConsideredMechanicalDrone(___pawn))
                {
                    return false;
                }
                return true;
            }
        }
    }
}