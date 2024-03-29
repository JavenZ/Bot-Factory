﻿using System.Collections.Generic;
using RimWorld;
using Verse;

namespace BotFactory
{
    // Override AffectQuality to use BF_MechanicalSurgerySuccessChanceFactor instead of SurgerySuccessChanceFactor
    public class SurgeryOutcomeComp_BedAndRoomMechQuality : SurgeryOutcomeComp_BedAndRoomQuality
    {
        public override void AffectQuality(RecipeDef recipe, Pawn surgeon, Pawn patient, List<Thing> ingredients, BodyPartRecord part, Bill bill, ref float quality)
        {
            quality *= patient.CurrentBed().GetStatValue(BF_StatDefOf.BF_MechanicalSurgerySuccessChanceFactor);
        }
    }
}