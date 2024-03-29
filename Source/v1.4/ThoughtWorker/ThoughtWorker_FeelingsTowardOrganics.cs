using RimWorld;
using Verse;

namespace BotFactory
{
    public class ThoughtWorker_FeelingsTowardOrganics : ThoughtWorker
    {
        protected override ThoughtState CurrentSocialStateInternal(Pawn p, Pawn other)
        {
            int feelingDegree = p.story.traits.DegreeOfTrait(BF_TraitDefOf.BF_FeelingsTowardOrganics);
            if (!RelationsUtility.PawnsKnowEachOther(p, other) || Utils.IsConsideredMechanical(other) || other.health.hediffSet.CountAddedAndImplantedParts() >= 5)
            {
                return false;
            }
            else
            {
                return ThoughtState.ActiveAtStage(feelingDegree - 1);
            }
        }
    }
}
