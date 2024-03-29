using System.Collections.Generic;
using RimWorld;
using Verse;

namespace BotFactory
{
    // Override AffectQuality to use BF_MechanicalSurgerySuccessChance instead of SurgerySuccessChance
    public class SurgeryOutcomeComp_MechanicSuccessChance : SurgeryOutcomeComp_SurgeonSuccessChance
    {
        public override void AffectQuality(RecipeDef recipe, Pawn surgeon, Pawn patient, List<Thing> ingredients, BodyPartRecord part, Bill bill, ref float quality)
        {
            quality *= surgeon.GetStatValue(BF_StatDefOf.BF_MechanicalSurgerySuccessChance);
        }
    }
}