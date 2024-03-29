using Verse;

namespace BotFactory
{
    public class HediffComp_Warper : HediffComp
    {
        // Organics hit by a fractal lance suffer the fractal warping hediff. If they already have the hediff, then do nothing.
        public override void CompPostPostAdd(DamageInfo? dinfo)
        {
            base.CompPostPostAdd(dinfo);
            if (Pawn.RaceProps.intelligence == Intelligence.Humanlike && !Utils.IsConsideredMechanical(Pawn) && Pawn.health.hediffSet.GetFirstHediffOfDef(BF_HediffDefOf.BF_FractalPillOrganic) == null)
            {
                Hediff fractal = HediffMaker.MakeHediff(BF_HediffDefOf.BF_FractalPillOrganic, Pawn);
                fractal.Severity = 0.25f;
                Pawn.health.AddHediff(fractal);
            }
        }
    }
}
