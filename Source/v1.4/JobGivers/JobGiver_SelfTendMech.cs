using RimWorld;
using Verse;
using Verse.AI;

namespace BotFactory
{
    public class JobGiver_SelfTendMech : ThinkNode_JobGiver
    {
        protected override Job TryGiveJob(Pawn pawn)
        {
            if (!pawn.RaceProps.Humanlike || !pawn.health.HasHediffsNeedingTend() || !pawn.health.capacities.CapableOf(PawnCapacityDefOf.Manipulation) || pawn.InAggroMentalState)
            {
                return null;
            }

            if (pawn.IsColonist && pawn.WorkTypeIsDisabled(BF_WorkTypeDefOf.BF_Mechanic))
            {
                return null;
            }

            Job job = JobMaker.MakeJob(BF_JobDefOf.BF_TendMechanical, pawn);
            job.endAfterTendedOnce = true;
            return job;
        }
    }
}
