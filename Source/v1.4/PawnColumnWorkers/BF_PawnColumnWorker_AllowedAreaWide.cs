using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Verse;

namespace BotFactory
{
    [StaticConstructorOnStartup]
    public class BF_PawnColumnWorker_AllowedAreaWide : PawnColumnWorker_AllowedAreaWide
    {
        public override void DoCell(Rect rect, Pawn pawn, PawnTable table)
        {
            if (pawn.Faction == Faction.OfPlayer)
            {
                AreaAllowedGUI.DoAllowedAreaSelectors(rect, pawn);
            }
        }
    }
}
