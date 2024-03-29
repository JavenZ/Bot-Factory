using RimWorld;
using Verse;

namespace BotFactory
{
    public class ThoughtWorker_RustedPart : ThoughtWorker
    {
        protected override ThoughtState CurrentStateInternal(Pawn p)
        {
            if (ThoughtUtility.ThoughtNullified(p, def) || !Utils.IsConsideredMechanicalAndroid(p))
            {
                return ThoughtState.Inactive;
            }

            for (int i = p.health.hediffSet.hediffs.Count - 1; i >= 0; i--)
            {
                if (p.health.hediffSet.hediffs[i].def == BF_HediffDefOf.BF_RustedPart)
                    return true;
            }
            return ThoughtState.Inactive;
        }
    }
}
