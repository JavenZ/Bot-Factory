using RimWorld;
using Verse;

namespace BotFactory
{
    public class ThoughtWorker_CoerciveVoiceSynthesizer : ThoughtWorker
    {
        protected override ThoughtState CurrentSocialStateInternal(Pawn p, Pawn other)
        {
            return RelationsUtility.PawnsKnowEachOther(p, other) && other.health.hediffSet.HasHediff(BF_HediffDefOf.BF_CoerciveVoiceSynthesizer);
        }
    }
}
