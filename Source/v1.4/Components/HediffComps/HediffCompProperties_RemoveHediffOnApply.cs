using Verse;

namespace BotFactory
{
    public class HediffCompProperties_RemoveHediffOnApply : HediffCompProperties
    {
        public HediffCompProperties_RemoveHediffOnApply()
        {
            compClass = typeof(HediffComp_RemoveHediffOnApply);
        }

        public HediffDef hediffToRemove;
    }
}
