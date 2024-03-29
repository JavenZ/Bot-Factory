using Verse;
using RimWorld;

namespace BotFactory
{
    public class CompSpawnerMicroScyther : ThingComp
    {

        public override void PostSpawnSetup(bool respawningAfterLoad)
        {
            SpawnPawn();
            parent.Destroy();
        }

        public void SpawnPawn()
        {
            PawnGenerationRequest request = new PawnGenerationRequest(BF_PawnKindDefOf.BF_MicroScyther, Faction.OfAncientsHostile, PawnGenerationContext.NonPlayer);
            Pawn pawn = PawnGenerator.GeneratePawn(request);
            GenSpawn.Spawn(pawn, parent.Position, parent.Map);
            pawn.mindState.mentalStateHandler.TryStartMentalState(BF_MentalStateDefOf.BF_MentalState_Exterminator, transitionSilently: true);
            
            Hediff hediff = HediffMaker.MakeHediff(BF_HediffDefOf.BF_RemainingCharge, pawn, null);
            hediff.Severity = 0.5f;
            pawn.health.AddHediff(hediff, null, null);
        }
    }
}